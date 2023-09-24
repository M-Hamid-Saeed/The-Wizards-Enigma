using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour {

    [Header("Eagle Movement Section")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeedVertical = 2f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float velocityReducingFactor = 1f;
    [SerializeField] private float maxReducingValue = .3f;
    private Rigidbody rb;
    [SerializeField] private Transform EagleChild;
    private Quaternion initialRotation;
    public Vector3 moveDirection = Vector3.zero;

    [Header("Eagle Flying Boundaries")]
    [SerializeField] private float minHeight = -0.38f;
    [SerializeField] private float maxHeight = 11f;

    [Header("Animation Section")]
    public Animator animator;

    private void Awake() {
        // To make the cursor invisible while playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        initialRotation = EagleChild.localRotation;
    }

    private void Update() {
        Inputs();
        Walking();
        if (Input.GetKey(KeyCode.Space))
        {
           
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("Leave");
        }
       
    }

    private void Inputs() {
        //Taking input through axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Getting main camera transform, to use it for getting camera forward direction
        //This will then be used to rotate the player towards the camera face, Making the forward of the player= forward of camera
        /*Vector3 cameraForward = Camera.main.transform.forward;
        //cameraForward.y = 0f;
        cameraForward.Normalize();    // Normalizing to make it unit vector

        Vector3 cameraRight = Camera.main.transform.right;
        //cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 cameraUP = Camera.main.transform.up;
        cameraUP.Normalize();*/

        //moveDirection = new Vector3(horizontalInput, 0, 0);
        transform.Rotate(verticalInput * rotationSpeedVertical,0f,0f) ;
        // Rotate the player towards the movement direction
        if (moveDirection != Vector3.zero) {
            
           // EagleChild.forward = Vector3.Slerp(EagleChild.forward, moveDirection, Time.deltaTime * rotationSpeedVertical);
          //  EagleChild.forward = Vector3.Slerp(EagleChild.forward, new Vector3(0,horizontalInput*rotationSpeedVertical,0), Time.deltaTime * rotationSpeedVertical);
        }


    }

    private void Walking() {
        Vector3 moveVelocity;
        moveDirection = transform.forward;
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveVelocity = moveDirection * (moveSpeed * 2f);
            animator.SetBool("isFlyingFast", true);
        }
        else {
            animator.SetBool("isFlyingFast", false);
            moveVelocity = moveDirection * moveSpeed;
        }

        if (moveVelocity != Vector3.zero)
            velocityReducingFactor += acceleration * Time.deltaTime;
        else
            velocityReducingFactor -= acceleration * Time.deltaTime;

        velocityReducingFactor = Mathf.Clamp(velocityReducingFactor, maxReducingValue, 1);
        moveVelocity *= velocityReducingFactor;
        moveVelocity.y = Mathf.Clamp(moveVelocity.y, minHeight, maxHeight);
        rb.velocity = Vector3.MoveTowards(rb.velocity, moveVelocity, 10 * Time.deltaTime);

        animator.SetBool("isFlying", moveDirection != Vector3.zero);
    }
}
