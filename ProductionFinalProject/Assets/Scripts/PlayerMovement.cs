using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;

    Animator AnimController;

    [SerializeField]
    float MoveSpeed = 6.0f;
    [SerializeField]
    float RotationSpeed = 100.0f;
    [SerializeField]
    float JumpSpeed = 10.0f;
    [SerializeField]
    float gravity = 20.0f;

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
        if (cc.isGrounded)
        {
            print(cc.velocity.magnitude);
            AnimController.SetFloat("MoveBlend", cc.velocity.magnitude);
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir = moveDir * MoveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDir.y = JumpSpeed;
            }
        }

        //apply gravity
        moveDir.y = moveDir.y - (gravity * Time.deltaTime);
        cc.Move(moveDir * Time.deltaTime);

    }
}
