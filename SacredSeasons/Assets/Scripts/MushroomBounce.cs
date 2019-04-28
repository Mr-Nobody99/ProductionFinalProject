using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBounce : MonoBehaviour
{
    float keyWeight;
    bool activate = false;
    SkinnedMeshRenderer mushroomMesh;

    // Start is called before the first frame update
    void Start()
    {
        mushroomMesh = this.GetComponent<SkinnedMeshRenderer>();
        keyWeight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(activate)
        {
            keyWeight = Mathf.Lerp(keyWeight, 100, Time.deltaTime * 3);
            mushroomMesh.SetBlendShapeWeight(0, keyWeight);
        }
        else if(!activate && mushroomMesh.GetBlendShapeWeight(0) > 0)
        {
            keyWeight = Mathf.Lerp(keyWeight, 0, Time.deltaTime * 3);
            mushroomMesh.SetBlendShapeWeight(0, keyWeight);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            activate = true;
            other.GetComponent<PlayerController>().doBounce = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            StartCoroutine(Delay());
            other.GetComponent<PlayerController>().doBounce = false;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(.25f);
        activate = false;
    }
}
