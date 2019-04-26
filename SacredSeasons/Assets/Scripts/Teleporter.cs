using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    string TargetScene;

    // Start is called before the first frame update
    void Start()
    {
        if (TargetScene == null)
        {
            TargetScene = SceneManager.GetActiveScene().name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(TargetScene);
        }
    }

}
