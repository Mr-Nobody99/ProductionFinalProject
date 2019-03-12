﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;

    Animator AnimController;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject shooter;

    [SerializeField]
    float MoveSpeed = 6.0f;
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

        AnimController.SetBool("Grounded", cc.isGrounded);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, shooter.transform.position, shooter.transform.rotation);
        }

        if (cc.isGrounded)
        {
            AnimController.SetFloat("Speed", Input.GetAxisRaw("Vertical"));
            AnimController.SetFloat("Direction", Input.GetAxisRaw("Horizontal"));
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir = moveDir * MoveSpeed;

            if (Input.GetButton("Jump"))
            {
                AnimController.SetTrigger("Jump");
                moveDir.y = JumpSpeed;
            }
        }

        //apply gravity
        moveDir.y = moveDir.y - (gravity * Time.deltaTime);
        cc.Move(moveDir * Time.deltaTime);

    }
}
