using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBarrier : MonoBehaviour
{
    [SerializeField]
    List<GameObject> plants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject foo in plants)
        {
            if (foo == null) plants.Remove(foo);
        }
        
        if(plants.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
