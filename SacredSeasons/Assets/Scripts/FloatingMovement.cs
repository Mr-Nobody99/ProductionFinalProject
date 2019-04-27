using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMovement : MonoBehaviour
{

    [SerializeField]
    float duration = 10f;

    //[SerializeField]
    //GameObject rotator;
    [SerializeField]
    public GameObject water;

    private Transform WaterTransform;
    private Cloth waterMesh;
    [SerializeField]
    private int closestVertIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        WaterTransform = water.transform;
        transform.SetParent(WaterTransform);
        waterMesh = WaterTransform.GetComponent<Cloth>();
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        GetClosestVertex();
    }

    void GetClosestVertex()
    {
        for(int i = 0; i< waterMesh.vertices.Length; i++)
        {

            if(closestVertIndex == -1)
            {
                closestVertIndex = i;
            }
            float distance = Vector3.Distance(waterMesh.vertices[i], transform.localPosition);
            float closestDistance = Vector3.Distance(waterMesh.vertices[closestVertIndex], transform.localPosition);

            if(distance < closestDistance)
            {
                closestVertIndex = i;
            }
        }
        Vector3 normal = WaterTransform.TransformDirection(waterMesh.normals[closestVertIndex]);
        Vector3 vertPos = WaterTransform.TransformPoint(waterMesh.vertices[closestVertIndex]);
        Debug.DrawRay(vertPos, normal * 10, Color.red);
        transform.localRotation = Quaternion.LookRotation(normal, Vector3.forward);
        transform.localPosition = new Vector3(waterMesh.vertices[closestVertIndex].x, waterMesh.vertices[closestVertIndex].y + transform.localScale.z, waterMesh.vertices[closestVertIndex].z);
    }
}
