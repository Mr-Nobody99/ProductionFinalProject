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
    float L_InputX;
    [SerializeField]
    float L_InputZ;
    [SerializeField]
    float L_inputMagnitude;
    [SerializeField]
    float R_InputX;
    [SerializeField]
    float R_InputZ;
    [SerializeField]
    float R_inputMagnitude;

    [Header("Movement Controls")]
    [SerializeField]
    float MoveSpeed = 10.0f;
    float initalSpeed;
    [SerializeField]
    float rotationSpeed = 0.1f;
    [SerializeField]
    float rotationBuildUp = 0.1f;
    [SerializeField]
    float jumpForce = 1.0f;
    [SerializeField]
    float gravity = 1.0f;
    [SerializeField]
    float fallDamping = 0.1f;

    [Header("Movement Debugs")]
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    bool blockRotation;
    [SerializeField]
    bool isGrounded;

    [Header("Projectiles")]
    [SerializeField]
    private GameObject Shooter;
    [SerializeField]
    List<GameObject> Projectiles;
    [SerializeField]
    private int equippedProjectileIndex = 0;

    private float verticalVelocity;
    private Vector3 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animController = this.GetComponent<Animator>();
        cam = Camera.main;
        initalSpeed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        blockRotation = false;
        CheckGrounded();
        CalcInputMagnitude();
        //UpdateAim();
        if(Input.GetButtonDown("Shoot"))
        {
            UpdateAim();
            Shoot();
        }
    }

    void CheckGrounded()
    {
        isGrounded = controller.isGrounded;
        animController.SetBool("Grounded", isGrounded);

        if (isGrounded)
        {
            if (MoveSpeed != initalSpeed) { MoveSpeed = initalSpeed; }
            if (Input.GetButton("Jump"))
            {
                animController.SetTrigger("Jump");
                MoveSpeed /= 2;
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity -= 0;
            }
        }
        else
        {
            verticalVelocity -= fallDamping;
        }

        movementVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(movementVector * gravity * Time.deltaTime);

    }

    void CalcInputMagnitude()
    {
        L_InputX = Input.GetAxis("Horizontal");
        L_InputZ = Input.GetAxis("Vertical");
        L_inputMagnitude = new Vector2(L_InputX, L_InputZ).sqrMagnitude;

        R_InputX = Input.GetAxis("Mouse X");
        R_InputZ = Input.GetAxis("Mouse Y");
        R_inputMagnitude = new Vector2(R_InputX, R_InputZ).sqrMagnitude;

        //animController.SetFloat("Speed", L_inputMagnitude);
        //animController.SetFloat("Input Z", inputMagnitude);
        //animController.SetFloat("Input X", InputX);

        if (L_inputMagnitude == 0.0f)
        {
            blockRotation = true;
            animController.SetFloat("Input X", L_InputX);
            animController.SetFloat("Input Z", L_InputZ);
        }
        else if (L_inputMagnitude > rotationBuildUp || R_inputMagnitude > 0.0f)
        {
            blockRotation = false;
            animController.SetFloat("Input Z", L_inputMagnitude);
            animController.SetFloat("Input X", 0.0f);
            MoveAndRotation();
            controller.Move(moveDirection * MoveSpeed * Time.deltaTime);
        }
        else if(L_inputMagnitude > 0.0f && L_inputMagnitude < rotationBuildUp && R_inputMagnitude == 0.0f)
        {
            blockRotation = true;
            animController.SetFloat("Input X", L_InputX);
            animController.SetFloat("Input Z", L_InputZ);
            MoveAndRotation();
            controller.Move(moveDirection * MoveSpeed * Time.deltaTime);
        }
    }

    void MoveAndRotation()
    {
        L_InputX = Input.GetAxis("Horizontal");
        L_InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * L_InputZ) + (right * L_InputX);

        if(!blockRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
        }
    }

    void UpdateAim()
    {
        Vector3 AimDirection = Shooter.transform.position - cam.transform.position;
        Shooter.transform.LookAt(AimDirection * 100.0f, Shooter.transform.up);
        Debug.DrawRay(Shooter.transform.position, AimDirection);
    }

    void Shoot()
    {
        Instantiate(Projectiles[equippedProjectileIndex], Shooter.transform.position, Shooter.transform.rotation);
    }

}
