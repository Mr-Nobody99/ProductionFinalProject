using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Main Camera")]
    [SerializeField]
    Camera cam;

    [Header("Player Components")]
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Animator animController;

    [Header("Inputs")]
    [SerializeField]
    float InputX;
    [SerializeField]
    float InputZ;

    [Header("Movement Controls")]
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed = 0.1f;
    [SerializeField]
    float allowRotation = 0.1f;

    [Header("Movement Parameters")]
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    bool blockRotation;
    [SerializeField]
    bool isGrounded;

    private float verticalVelocity;
    private Vector3 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animController = this.GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        ApplyMove();
    }

    void ApplyMove()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        speed = new Vector2(InputX, InputZ).sqrMagnitude;
        animController.SetFloat("Speed", speed);

        if (speed > allowRotation)
        {
            MoveAndRotation();
            controller.Move(moveDirection * 10.0f * Time.deltaTime);
        }
    }

    void MoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * InputZ) + (right * InputX);

        if(!blockRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
        }
    }

}
