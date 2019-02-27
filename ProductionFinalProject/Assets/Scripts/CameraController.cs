using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Reference to camera game object
    [SerializeField]
    GameObject CameraRef;

    //Reference to player game object
    [SerializeField]
    GameObject PlayerRef;

    //Reference to object that will be followed
    [SerializeField]
    GameObject ObjToFollow;

    Vector3 FollowPos;

    [SerializeField]
    float CameraMoveSpeed = 120.0f;
    [SerializeField]
    float ClampAngle = 80.0f;
    [SerializeField]
    float InputSensitivity = 150.0f;

    float DistanceToPlayerX;
    float DistanceToPlayerY;
    float DistanceToPlayerZ;

    float MouseX;
    float MouseY;

    float FinalInputX;
    float FinalInputZ;

    [SerializeField]
    float SmoothX;
    [SerializeField]
    float SmoothY;

    private float RotX;
    private float RotY;

    // Start is called before the first frame update
    void Start()
    {

        //set object references
        CameraRef = this.gameObject;
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        ObjToFollow = GameObject.FindGameObjectWithTag("CamFollowTarget");

        //Get inital euler rotation in local space 
        Vector3 rot = transform.localRotation.eulerAngles;
        RotX = rot.x;
        RotY = rot.y;

        //Lock the curser and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate new camera rotation from inputs
        float InputX = Input.GetAxis("RightJstkHorizontal");
        float InputZ = Input.GetAxis("RightJstkVertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        FinalInputX = InputX + MouseX;
        FinalInputZ = InputZ + MouseY;

        RotX += FinalInputZ * InputSensitivity * Time.deltaTime;
        RotY += FinalInputX * InputSensitivity * Time.deltaTime;

        RotX = Mathf.Clamp(RotX, -ClampAngle, ClampAngle);

        Quaternion localRoation = Quaternion.Euler(RotX, RotY, 0.0f);
        transform.rotation = localRoation;//Apply new camera rotation
        if(PlayerRef.GetComponent<CharacterController>().velocity.magnitude > 0.0f)
        {
            Quaternion newPlayerRotation = Quaternion.Euler(0.0f, RotY, 0.0f);
            PlayerRef.transform.rotation = newPlayerRotation;
        }

    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    //Called to move camera
    void UpdateCamera()
    {
        //set the target object to follow'
        Transform target = ObjToFollow.transform;

        //Move towards the target object
        float move = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, move);
    }
}
