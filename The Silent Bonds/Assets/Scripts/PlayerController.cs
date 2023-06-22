using UnityEngine;

public class PlayerController : MonoBehaviour {

    //  [SerializeField] private float gravityModifier = 1.5f;
    [Header("Player Movement Section")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float velocityReducingFactor = 1f;
    [SerializeField] private float maxReducingValue = .3f;
    [SerializeField] private float groundRaycastDistance = 0.5f;
    [SerializeField] private Transform playerChild;
    [SerializeField] 
    public Vector3 moveDirection = Vector3.zero;
    private Vector3 previousMoveDirection = Vector3.zero;
    private bool isOnGround = true;
    private bool hasJumped = false;
    public Rigidbody rb;
   

    [Header("Jumping Section")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float startFallingVelocity = 1.0f;
    [SerializeField] private float fallingRate = 1.5f;

    [Header("Combat Section")]
    public Transform AttackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int damage = 20;
    public LayerMask enemyLayer;
    private float startedAttacking;

    [Header("Animation Section")]
    public Animator animator;
    private float lastRightClickTime = 0f;
    private float doubleClickDelay = 0.5f;


    [Header("Dashing Particle Section")]
    [SerializeField] private Transform particlesParent;
    [SerializeField] private ParticleSystem jumpParticles;
    [Header("Player Sound Effects")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip DashSound;
    [SerializeField] private AudioClip AttackSound;
    [SerializeField] private AudioSource audioSource;
    


    [SerializeField] private TrailRenderer swordTrail;



    private void Awake() {

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        particlesParent = transform.Find("Particles").Find("JumpParticles");
        jumpParticles = particlesParent.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update() {
        CheckGround();
        Inputs();
        Walking();
        Jump();
        Attacking();
        Attacking2();
        Defending();
        ApplyDownForce();
        soundEffectsManage();
    }

    private void Inputs() {
        //Taking input through axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //Getting main camera transform, to use it for getting camera forward direction
        //This will then be used to rotate the player towards the camera face, Making the forward of the player= forward of camera
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();    // Normalizing to make it unit vector

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        
        Vector3 newMoveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        // to maintain the previous move direction in case it is currently zero
        if (newMoveDirection == Vector3.zero)
        {
            previousMoveDirection = moveDirection;
        }

        moveDirection = newMoveDirection;
            

        // Rotate the player towards the movement direction
        if (moveDirection != Vector3.zero)
            playerChild.forward = Vector3.Slerp(playerChild.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }

    private void Walking() {
        Vector3 moveVelocity;

        if (Input.GetKey(KeyCode.LeftShift)) {
            moveVelocity = moveDirection * runSpeed;
            animator.SetBool("isRunning", true);
        }
        else {
            animator.SetBool("isWalking", moveDirection != Vector3.zero);
            animator.SetBool("isRunning", false);
            animator.SetBool("isDashing", false);

            moveVelocity = moveDirection * moveSpeed;
        }
        if (moveVelocity != Vector3.zero)
            velocityReducingFactor += acceleration * Time.deltaTime;

        else
            velocityReducingFactor -= acceleration * Time.deltaTime;


        velocityReducingFactor = Mathf.Clamp(velocityReducingFactor, maxReducingValue, 1);
        moveVelocity *= velocityReducingFactor;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = Vector3.MoveTowards(rb.velocity, moveVelocity, 100 * Time.deltaTime);

    }
    private void Attacking() {
        if (Input.GetMouseButtonDown(0)) {
            attackatPoint();
            animator.SetTrigger("Attack");
            swordTrail.enabled = true;
            startedAttacking = Time.time;
        }

        // this part makes sure that the trail is only rendered while attacking
        if (swordTrail.enabled && Time.time > startedAttacking + 0.5)
            swordTrail.enabled = false;
    }

    private void CheckGround() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRaycastDistance)) {
            if (hit.collider.CompareTag("ground")) {
                isOnGround = true;
                hasJumped = false;
            }
        }
        else {
            isOnGround = false;
        }

    }
  



    private void Jump() {

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !hasJumped  /*jumpCount < 2*/) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
            hasJumped = true;
            PlayJumpParticle();
        }
        //ApplyDownForce();

    }

    private void ApplyDownForce() {

            // Check if the player is above a certain height or velocity threshold
            if ((rb.velocity.y < startFallingVelocity) && !isOnGround)
            {
                Debug.Log("Applying Down Force");
                rb.velocity += Physics.gravity * fallingRate * Time.deltaTime;
            }
                

    }
    private void Attacking2() {
        if (Input.GetMouseButtonDown(0)) {
            // Check for double right-click
            
            if (Time.time - lastRightClickTime <= doubleClickDelay) {
                // Double right-click detected, trigger the animation
                animator.SetTrigger("Attack2");
                attackatPoint();
                swordTrail.enabled = true ;
                startedAttacking = Time.time;
            }

            lastRightClickTime = Time.time;

            // this part makes sure that the trail is only rendered while attacking
            if (swordTrail.enabled && Time.time > startedAttacking + 0.5)
                swordTrail.enabled = false;
        }
    }

    private void Defending() {
        if (Input.GetMouseButtonDown(1)) {
            animator.SetTrigger("Defend");

        }
    }
    private void attackatPoint() {
        Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies) {
            enemy.gameObject.GetComponent<EnemyHealthCombat>().TakeDamage(damage);
            Debug.Log(enemy.name);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    private void PlayJumpParticle()
    {
        particlesParent.forward = - playerChild.transform.forward;
        particlesParent.Rotate(60, 0, 0);
        jumpParticles.Play();
    }

    private void SetSwordTrail(bool status)
    {
        swordTrail.enabled = status;

        
    }
    private void soundEffectsManage() {
        if (Input.GetMouseButtonDown(0)) {
            audioSource.PlayOneShot(AttackSound, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !hasJumped) {
            audioSource.PlayOneShot(jumpSound, 1f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            // Play dash sound effect
            // Add code here to play dash sound effect
        }
    }
}
 