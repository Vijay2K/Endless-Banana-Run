using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f; //player moving speed
    [SerializeField] private float gravity = -9.8f; // gravity pull towards y pos
    [SerializeField] private float groundDistance = 0.4f; // distance between player and the ground
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float laneDistance = 2.5f;

    private CharacterController controller;
    private Vector3 direction;
    private bool isGrounded;
    private int desiredLane = 1;
    private SwipeController swipeController;

    private void Start() {
        controller = GetComponent<CharacterController>();
        swipeController = GetComponent<SwipeController>();
    }

    private void Update() {
        //moving forward
        direction.z = forwardSpeed;

        // To check the player whether the player is in the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // For jumping
        if(isGrounded && direction.y < 0f) {
            direction.y = -2f;
            if(SwipeController.swipeUp) {
                Jump();
            }
        } else {
            // pull the player to the ground
            direction.y += gravity * Time.deltaTime;
        }

        controller.Move(direction * Time.deltaTime);

        // For moving left
        if(SwipeController.swipeLeft) {
            desiredLane--;
            if(desiredLane == -1) {
                desiredLane = 0;
            }
        }

        // For moving right
        if(SwipeController.swipeRight) {

            desiredLane++;
            if (desiredLane == 3) {
                desiredLane = 2;
            }

        }

        // initializing the player position in the middle
        Vector3 playerPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 2) {
            playerPos += Vector3.right * laneDistance;
        } else if(desiredLane == 0) {
            playerPos += Vector3.left * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, playerPos, 80);
        Vector3 diff = playerPos - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if(moveDir.sqrMagnitude < diff.sqrMagnitude) {
            controller.Move(moveDir);
        } else {
            controller.Move(diff);
        }
    }

    private void Jump() {
        // FORMULA -> Sqrt(height * -2 * gravity)
        direction.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void FixedUpdate() {
        // Move the player using the character controller
        controller.Move(direction * Time.fixedDeltaTime);
    }

}
