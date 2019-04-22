﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [Header("Currently Equipped Element")]
    [SerializeField]
    private int equippedElementIndex = 0;
    bool blockElementSwap = false;

    [Header("Projectiles")]
    [SerializeField]
    private GameObject Shooter;

    [Header("Defense")]
    [SerializeField]
    List<GameObject> shields;
    [SerializeField]
    float shieldSpeed = 2f;
    [SerializeField]
    bool isDefending = false;

    private float verticalVelocity;
    private Vector3 movementVector;

    public static float maxHealth = 100;
    public static float currentHealth;

    [SerializeField]
    public Image healthBar;

    public static bool paused = false;
    public static bool menuUp = false;

    // Boolean to ignore jump for a half second after menu button is pressed
    public static bool jumpOk = true;

    public static string currentSpellName;

    public GameObject ShieldSpawn;
    //GameObject CurrentShield;
    GameObject cloneShield;
    public List<InventoryManager.Shield> localShields = new List<InventoryManager.Shield>();

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animController = this.GetComponent<Animator>();
        cam = Camera.main;
        initalSpeed = MoveSpeed;
        currentHealth = maxHealth;
        //deactivate shields at start
        //UIManager.instance.ShowScreen("Main Menu");
        foreach(GameObject foo in shields)
        {
            foo.SetActive(false);
        }

        Transform[] Children = this.GetComponentsInChildren<Transform>();

        foreach(Transform c in Children)
        {
            if (c.gameObject.name == "Shields")
            {
                ShieldSpawn = c.gameObject;
            }
        }

        foreach(InventoryManager.Shield s in InventoryManager.instance.PlayerShields)
        {
            localShields.Add(s);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!menuUp && !EventSystem.current.IsPointerOverGameObject())
        {
            blockRotation = false;
            CheckGrounded();
            isDefending = Input.GetButton("Block") ? true : false;
            Defend(isDefending);
            if (!isDefending)
            {
                if (!paused)
                {
                    CalcInputMagnitude();
                }
                UpdateEquippedElement();
                if (Input.GetButtonDown("Shoot"))
                {
                    UpdateAim();
                    Shoot();
                }
            }
            else
            {
                animController.SetFloat("Input X", 0f);
                animController.SetFloat("Input Z", 0f);
            }
        }

        if(Input.GetButtonDown("Pause"))
        {
            if(paused)
            {
                UnPause();
            }
            else if(!paused && !UIManager.instance.screens[UIManager.instance.curScreen].screen.activeSelf)
            {
                Pause();
            }
        }
    }

    void CheckGrounded()
    {
        isGrounded = controller.isGrounded;
        animController.SetBool("Grounded", isGrounded);

        if (isGrounded)
        {
            if (MoveSpeed != initalSpeed) { MoveSpeed = initalSpeed; }
            if (Input.GetButtonUp("Jump") && !isDefending && jumpOk)
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

    void UpdateEquippedElement()
    {
        // Gamepad support section
        if (Input.GetAxis("SwitchProjectile") == 1 && !blockElementSwap)
        {
            ++InventoryManager.instance.currentSpellIndex;

            if (InventoryManager.instance.currentSpellIndex == 3)
            {
                InventoryManager.instance.currentSpellIndex = 0;
            }

            while (!InventoryManager.instance.PlayerSpells[InventoryManager.instance.currentSpellIndex].available)
            {
                ++InventoryManager.instance.currentSpellIndex;

                if (InventoryManager.instance.currentSpellIndex == 3)
                {
                    InventoryManager.instance.currentSpellIndex = 0;
                }
            }

            blockElementSwap = true;
        }
        else if (Input.GetAxis("SwitchProjectile") == -1 && !blockElementSwap)
        {
            --InventoryManager.instance.currentSpellIndex;

            if (InventoryManager.instance.currentSpellIndex == -1)
            {
                InventoryManager.instance.currentSpellIndex = 2;
            }

            while (!InventoryManager.instance.PlayerSpells[InventoryManager.instance.currentSpellIndex].available)
            {

                --InventoryManager.instance.currentSpellIndex;

                if (InventoryManager.instance.currentSpellIndex == -1)
                {
                    InventoryManager.instance.currentSpellIndex = 2;
                }
            }

            blockElementSwap = true;
        }
        else if (Input.GetAxis("SwitchProjectile") == 0)
        {
            blockElementSwap = false;
        }
        currentSpellName = InventoryManager.instance.PlayerSpells[InventoryManager.instance.currentSpellIndex].name;

        // Keyboard support section
        if (Input.GetButtonUp("ProjectileForward") && !blockElementSwap)
        {
            ++InventoryManager.instance.currentSpellIndex;

            if (InventoryManager.instance.currentSpellIndex == 3)
            {
                InventoryManager.instance.currentSpellIndex = 0;
            }

            while (!InventoryManager.instance.PlayerSpells[InventoryManager.instance.currentSpellIndex].available)
            {
                ++InventoryManager.instance.currentSpellIndex;

                if (InventoryManager.instance.currentSpellIndex == 3)
                {
                    InventoryManager.instance.currentSpellIndex = 0;
                }
            }

            blockElementSwap = true;
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
        Instantiate(InventoryManager.instance.PlayerSpells[InventoryManager.instance.currentSpellIndex].projectile, cam.transform.position + cam.transform.forward * 5.0f, cam.transform.rotation);
    }

    void Defend(bool activate)
    {
        var shield = shields[InventoryManager.instance.currentSpellIndex];

        if (activate)
        {
            if (!shield.activeSelf)
            {
                shield.SetActive(true);
                shield.transform.localScale = new Vector3(1f, 0f, 1f);
            }
            else
            {
                shield.transform.localScale = Vector3.Lerp(shield.transform.localScale, Vector3.one, Time.deltaTime * shieldSpeed);
            }
        }
        else
        {
            shield.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth/maxHealth;

        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UIManager.instance.ShowScreen("Defeat Screen");
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UIManager.instance.ShowScreen("Pause Menu");
        menuUp = true;
        paused = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.instance.Play();
        menuUp = false;
        paused = false;
    }
}
