using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMovement : MonoBehaviour
{

    [SerializeField]
    float duration = 10f;

    private Transform WaterPlane;
    private Cloth clothMesh;
    private int closestVertIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        WaterPlane = GameObject.Find("Water").transform;
        clothMesh = WaterPlane.GetComponent<Cloth>();
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        GetClosestVertex();
    }

    void GetClosestVertex()
    {
        for(int i = 0; i< clothMesh.vertices.Length; i++)
        {
            if(closestVertIndex == -1)
            {
                closestVertIndex = i;
            }
            float distance = Vector3.Distance(clothMesh.vertices[i], transform.position);
            float closestDistance = Vector3.Distance(clothMesh.vertices[closestVertIndex], transform.position);

            if(distance < closestDistance)
            {
                closestVertIndex = i;
            }
        }

        transform.localPosition = new Vector3(transform.localPosition.x, clothMesh.vertices[closestVertIndex].y + transform.localScale.y, transform.localPosition.z);
    }
}
