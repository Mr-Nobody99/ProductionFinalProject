using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    Image healthBar;

    [Header("Movement Settings")]
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
    float airControl = 0.5f;
    [SerializeField]
    float bounceForce = 1.0f;

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

    [Header("Currently Equipped Element")]
    [SerializeField]
    private int equippedElementIndex = 0;
    bool blockElementSwap = false;

    [Header("Projectiles")]
    [SerializeField]
    List<GameObject> Projectiles;
    [SerializeField]
    Transform shooter;

    [Header("Defense")]
    [SerializeField]
    List<GameObject> shields;
    [SerializeField]
    float shieldSpeed = 2f;
    [SerializeField]
    bool isDefending = false;

    [Header("Movement Debugs")]
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    bool blockRotation;
    [SerializeField]
    bool isGrounded;

    private float verticalVelocity;
    private Vector3 movementVector;

    public static float maxHealth = 100;
    public static float currentHealth;

    public static bool paused = false;
    public static bool menuUp = false;

    public static string currentSpellName;

    public bool doBounce = false;

    [SerializeField]
    public static List<GameObject> spellsShot = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animController = this.GetComponent<Animator>();
        cam = Camera.main;
        initalSpeed = MoveSpeed;

        currentHealth = maxHealth;
        healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
        
        //deactivate shields at start
        foreach(GameObject foo in shields)
        {
            foo.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!menuUp && !EventSystem.current.IsPointerOverGameObject())
        {
            blockRotation = false;
            isDefending = Input.GetButton("Block") ? true : false;
            Defend(isDefending);

            if (!paused)
            {
                isGrounded = controller.isGrounded;
                animController.SetBool("Grounded", isGrounded);
                CheckGrounded();
                if (!isDefending)
                {
                    CalcInputMagnitude();
                    CalcMoveAndRotation();
                }
                else
                {
                    animController.SetFloat("Input X", 0f);
                    animController.SetFloat("Input Z", 0f);
                }
                ApplyMove();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }

        if (!paused && !isDefending)
        {
            UpdateEquippedElement();

            if (Input.GetButtonDown("Shoot"))
            {
                animController.SetTrigger("Throw");
            }
        }
    }

    void CheckGrounded()
    {
        if (isGrounded)
        {
            if (MoveSpeed != initalSpeed) { MoveSpeed = initalSpeed; }
            if (Input.GetButtonDown("Jump") || doBounce && !isDefending)
            {
                animController.SetTrigger("Jump");
                MoveSpeed *= airControl;
                if(doBounce)
                {
                    verticalVelocity = bounceForce;
                }
                else
                {
                    verticalVelocity = jumpForce;
                }
            }
            else
            {
                verticalVelocity -= 0;
            }
        }
        else
        {
            verticalVelocity -= 1;
        }

        movementVector = new Vector3(0, verticalVelocity, 0);
    }

    void CalcInputMagnitude()
    {
        L_InputX = Input.GetAxis("Horizontal");
        L_InputZ = Input.GetAxis("Vertical");
        L_inputMagnitude = new Vector2(L_InputX, L_InputZ).sqrMagnitude;

        R_InputX = Input.GetAxis("Mouse X");
        R_InputZ = Input.GetAxis("Mouse Y");
        R_inputMagnitude = new Vector2(R_InputX, R_InputZ).sqrMagnitude;

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
        }
        else if(L_inputMagnitude > 0.0f && L_inputMagnitude < rotationBuildUp && R_inputMagnitude == 0.0f)
        {
            blockRotation = true;
            animController.SetFloat("Input X", L_InputX);
            animController.SetFloat("Input Z", L_InputZ);
        }
    }

    void CalcMoveAndRotation()
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
        moveDirection *= MoveSpeed;

        if (!blockRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
        }
    }

    void ApplyMove()
    {
        if (isDefending)
        {
            moveDirection = Vector3.zero;
        }
        movementVector.x = moveDirection.x;
        movementVector.z = moveDirection.z;
        controller.Move(movementVector * Time.deltaTime);
    }

    void UpdateEquippedElement()
    {
        if (Input.GetAxis("SwitchProjectile") == 1 || Input.GetKeyDown(KeyCode.Tab) && !blockElementSwap)
        {
            equippedElementIndex = (equippedElementIndex < Projectiles.Count - 1) ? ++equippedElementIndex : 0;
            blockElementSwap = true;
        }
        else if (Input.GetAxis("SwitchProjectile") == -1 || Input.GetKeyDown(KeyCode.Tab) && !blockElementSwap)
        {
            equippedElementIndex = (equippedElementIndex > 0) ? --equippedElementIndex : Projectiles.Count - 1;
            blockElementSwap = true;
        }
        else if (Input.GetAxis("SwitchProjectile") == 0 || Input.GetKeyUp(KeyCode.Tab))
        {
            blockElementSwap = false;
        }

        switch (equippedElementIndex)
        {
            case 0:
                currentSpellName = "Ice Spell";
                break;
            case 1:
                currentSpellName = "Earth Spell";
                break;
            case 2:
                currentSpellName = "Fire Spell";
                break;
        }
    }

    public void Shoot()
    {
        if (Vector3.Dot(cam.transform.forward, transform.forward) > 0.25f)
        {
            RaycastHit Hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out Hit, 100000.0f))
            {
                shooter.LookAt(Hit.point);
                spellsShot.Add(Instantiate(Projectiles[equippedElementIndex], shooter.position, shooter.rotation));
            }
            else
            {
                spellsShot.Add(Instantiate(Projectiles[equippedElementIndex], cam.transform.position + cam.transform.forward * 5.0f, cam.transform.rotation));
            }
        }
        else
        {
            shooter.LookAt(transform.forward * 10000.0f);
            spellsShot.Add(Instantiate(Projectiles[equippedElementIndex], shooter.position, shooter.rotation));
        }
    }

    void Defend(bool activate)
    {
        var shield = shields[equippedElementIndex];

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
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            UIManager.instance.TutorialUnPause();
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UIManager.instance.Play();
            menuUp = false;
            paused = false;
        }
    }

}
