using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHere : MonoBehaviour
{
    public GameObject destroyMe;
    bool shieldActive = false;
    BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.GetComponent<BoxCollider>();
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.moveComplete == true)
        {
            box.enabled = true;
        }

        if (Input.GetButton("Block") && shieldActive == true)
        {
            StartCoroutine(ShieldCoroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Projectile"))
        {
            Destroy(destroyMe);
            Destroy(box);
            GameManager.instance.shootComplete = true;
            shieldActive = true;
        }
    }

    public IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.shieldComplete = true;
        GameManager.instance.tutorialComplete = true;
        Destroy(gameObject);
    }

}
