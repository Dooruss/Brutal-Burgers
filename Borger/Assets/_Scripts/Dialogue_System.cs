using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue_System : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] DialogueLines;
    public float textSpeed;
    private int index;

    private void Start()
    {
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf)
        {
            if (DialogueText.text == DialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = DialogueLines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        DialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in DialogueLines[index].ToCharArray())
        {
            DialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < DialogueLines.Length - 1)
        {
            index++;
            DialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
