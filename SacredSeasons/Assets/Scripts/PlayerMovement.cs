using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public CinemachineFreeLook cineCam;

    Animator AnimController;

    [SerializeField]
    List<GameObject> projectiles;
    [SerializeField]
    private GameObject shooter;

    [SerializeField]
    float MoveSpeed = 6.0f;
    [SerializeField]
    float RotationSpeed = 60.0f;
    [SerializeField]
    float JumpSpeed = 10.0f;
    [SerializeField]
    float gravity = 25.0f;

    private bool allowProjectileSwap = true;
    private int ProjectileIndex = 0;
    private Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
       cc = GetComponent<CharacterController>();
       AnimController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if (cc.velocity.magnitude > 0)
        {
            transform.Rotate(0.0f, Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime, 0.0f);
        }
        AnimController.SetBool("Grounded", cc.isGrounded);

        if (Input.GetAxisRaw("SwitchProjectile") == 1 && allowProjectileSwap)
        {
            allowProjectileSwap = false;
            if (ProjectileIndex < projectiles.Count - 1)
            {
                ProjectileIndex++;
                print(ProjectileIndex);
            }
            else if (++ProjectileIndex >= projectiles.Count)
            {
                ProjectileIndex = 0;
            }
        }
        else if (Input.GetAxisRaw("SwitchProjectile") == 0)
        {
            allowProjectileSwap = true;
        }

        if (Input.GetButtonDown("Shoot"))
        {
            Instantiate(projectiles[ProjectileIndex], shooter.transform.position, shooter.transform.rotation);
        }

        if (cc.isGrounded)
        {
            AnimController.SetFloat("Speed", Input.GetAxisRaw("Vertical"));
            AnimController.SetFloat("Direction", Input.GetAxisRaw("Horizontal"));

            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir = moveDir * MoveSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                AnimController.SetTrigger("Jump");
                moveDir.y = JumpSpeed;
            }
        }

        //apply gravity
        moveDir.y = moveDir.y - (gravity * Time.deltaTime);
        cc.Move(moveDir * Time.deltaTime);

    }

    void Aim()
    {
        Vector3 AimDir = shooter.transform.position - cineCam.transform.position;
        shooter.transform.LookAt(AimDir * 100.0f, shooter.transform.up);
        Debug.DrawRay(shooter.transform.position, AimDir);
    }
}
