using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.VFX;
using UnityEngine;


public class BossController : MonoBehaviour
{
    Animator animController;

    public GameObject monsterPrefab;
    GameObject monsterForm;
    public VisualEffect smokeFX;

    bool growMonster = false;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        smokeFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if(growMonster)
        {
            StartCoroutine(GrowDelay());
        }
    }

    IEnumerator TrasformDelay()
    {
        animController.SetTrigger("SpazOut");
        yield return new WaitForSecondsRealtime(0.15f);
        smokeFX.Play();
        yield return new WaitForSecondsRealtime(5f);
        monsterForm = Instantiate(monsterPrefab, transform.position, transform.rotation);
        monsterForm.transform.localScale = Vector3.zero;
        growMonster = true;
    }

    IEnumerator GrowDelay()
    {
        monsterForm.transform.localScale += new Vector3(0.1f,0.1f,0.1f);
        if(monsterForm.transform.localScale.x >= 6.5f)
        {
            growMonster = false;
            Destroy(gameObject);
        }
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
