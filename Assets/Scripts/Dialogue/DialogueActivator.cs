using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI dialogueText;
    public float textSpeed = 0.05f;
    public UnityEvent onDialogueEnd;
    public List<DialogueLines> dialogueLines;
    private int currentIndex = 0;
    private bool inDialogue = false;
    private bool firstClick = false;
    public void PlayDialogue()
    {
        StopAllCoroutines();
        var dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogueBox.transform.GetChild(0).gameObject.SetActive(true);
        currentIndex = 0;
        inDialogue = true;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        var line = dialogueLines[currentIndex];
        dialogueText.text = string.Empty;

        // Wait one frame to ensure UI updates (a missing character bug occurs without this)
        yield return null;

        for (int i = 0; i < line.line.Length; i++)
        {
            dialogueText.text += line.line[i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (currentIndex < dialogueLines.Count - 1)
        {
            currentIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            onDialogueEnd.Invoke();
            var dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
            dialogueBox.transform.GetChild(0).gameObject.SetActive(false);
            inDialogue = false;
            firstClick = false;
            Debug.Log("End of conversation");
        }
    }

    private void Update()
    {
        if (inDialogue && Input.GetMouseButtonUp(0))
        {
            if(!firstClick)
            {
                firstClick = true;
                return;
            }
            Click();
        }
    }

    public void Click()
    {
        // If line finished, go to next
        if (dialogueText.text == dialogueLines[currentIndex].line)
        {
            NextLine();
        }
        // If still typing, instantly complete line
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentIndex].line;
        }
    }
}
