using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //  [SerializeField] private float gravityModifier = 1.5f;
    [Header("Player Movement Section")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float velocityReducingFactor = 1f;
    [SerializeField] private float maxReducingValue = .3f;
    [SerializeField] private float groundRaycastDistance = 0.5f;
    [SerializeField] private Transform playerChild;
    //[SerializeField] private GameObject secondCamera;

    [Header("Jumping Section")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float startFallingPosition = 1.0f;
    [SerializeField] private float fallingRate = 1.5f;
    private Vector3 moveDirection = Vector3.zero;
    private int jumpCount = 0;
    private bool isOnGround = true;
    private Rigidbody rb;

    [Header("Animation Section")]
    public Animator animator;





    private void Awake() {
        rb = GetComponent<Rigidbody>();
        //  animator = GetComponent<Animator>();

    }


    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space) && isOnGround  /*jumpCount < 2*/ ) 

        //    Jump();

        Inputs();
        //  CheckGround();
         Jump();
        
        

    }


    
    private void FixedUpdate() {
        Walking();
        // ApplyGravity();

    }

    

        private void Inputs() {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            moveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

            // Rotate the player towards the movement direction
            if (moveDirection != Vector3.zero)
                playerChild.forward = Vector3.Slerp(playerChild.forward, moveDirection, Time.deltaTime * rotationSpeed);

        }

        private void Walking() {
            Vector3 moveVelocity = moveDirection * moveSpeed;

            if (moveVelocity != Vector3.zero) {
                velocityReducingFactor += acceleration * Time.deltaTime;
            }
            else {
                velocityReducingFactor -= acceleration * Time.deltaTime;
            }

            velocityReducingFactor = Mathf.Clamp(velocityReducingFactor, maxReducingValue, 1);
            moveVelocity *= velocityReducingFactor;
            moveVelocity.y = rb.velocity.y;

            rb.velocity = Vector3.MoveTowards(rb.velocity, moveVelocity, 100 * Time.deltaTime);


            animator.SetBool("isWalking", moveDirection != Vector3.zero);
        }
        private void CheckGround() {
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRaycastDistance)) {
                if (hit.collider.CompareTag("ground")) {
                    isOnGround = true;
                    jumpCount = 0;
                }
            }
            else {
                isOnGround = false;
            }*/

        }
        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("ground")) {
                isOnGround = true;
                jumpCount = 0;
            }
            else
                isOnGround = false;
        }



        private void Jump() {
            /*rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            if (jumpCount >= 2) {
                isOnGround = false;
            }*/
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround  /*jumpCount < 2*/ ) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                StartCoroutine(ApplyDownForce());
            }
        }
        IEnumerator ApplyDownForce() {
        yield return new WaitForSeconds(0.2f);

            while (!isOnGround) {
            // Check if the player is above a certain height or velocity threshold
                 if (transform.position.y > startFallingPosition) 
                     rb.AddForce(Vector3.down * fallingRate, ForceMode.Force);
            
        }

        
    }
}