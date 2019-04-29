using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    bool BossFight = false;
    bool activated = false;
    bool transformActive = false;

    int count;

    public List<Dialogue> dialogue;
    public int currentDialogueIndex = 0;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().BeginConversation(dialogue[currentDialogueIndex]);
    }

    private void Update()
    {
        if (!BossFight)
        {
            //count = 0;
            //TutorialTarget[] targets = FindObjectsOfType<TutorialTarget>();
            /*foreach (TutorialTarget target in targets)
            {
                if (!target.active)
                {
                    count++;
                }
            }
            if (count == targets.Length)
            {
                currentDialogueIndex = 1;
            }
            if (currentDialogueIndex == 1 && Input.GetButtonDown("Block"))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                StartCoroutine(EndTutorial());
            }*/
        }
        else if(BossFight && activated)
        {
            if (!transformActive)
            {
                StartCoroutine(ActivateTransform());
                StartCoroutine(DioDelay(2));
                StartCoroutine(DioDelay(4));
                StartCoroutine(DioDelay(6));
            }
        }
    }

    /*IEnumerator EndTutorial()
    {
        yield return new WaitForSecondsRealtime(12);
        SceneManager.LoadScene("HUB");
    }*/

    IEnumerator ActivateTransform()
    {
        transformActive = true;
        yield return new WaitForSecondsRealtime(10);
        FindObjectOfType<BossController>().StartTransform();
        FindObjectOfType<DialogueManager>().EndConversation();
        Destroy(gameObject);
    }

    IEnumerator DioDelay(int count)
    {
        yield return new WaitForSeconds(count);
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (!BossFight)
            {
                TriggerDialogue();
            }
            else if( BossFight)
            {
                TriggerDialogue();
                activated = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            FindObjectOfType<DialogueManager>().EndConversation();
        }
    }
}
