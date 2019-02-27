using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    Vector3 DollyDir;
    Vector3 AdjustedDollyDir;

    [SerializeField]
    float DollySmooth = 10.0f;
    [SerializeField]
    float MinDistance = 1.0f;
    [SerializeField]
    float MaxDistance = 4.0f;
    [SerializeField]
    float Distance;

    void Awake()
    {
        DollyDir = transform.localPosition.normalized;
        Distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //Do camera object avoidance
        Vector3 targetCamPos = transform.parent.TransformPoint(DollyDir * MaxDistance);
        RaycastHit hit;

        //if object is blocking
        if (Physics.Linecast(transform.parent.position, targetCamPos, out hit))
        {
            //Set distance to adjust camera as distance to hit clamped between max & min
            Distance = Mathf.Clamp((hit.distance * 0.85f), MinDistance, MaxDistance);
        }
        else
        {
            //No blocking object set to max distance
            Distance = MaxDistance;
        }

        //Move the camera
        transform.localPosition = Vector3.Lerp(transform.localPosition, DollyDir * Distance, Time.deltaTime * DollySmooth);
    }
}
