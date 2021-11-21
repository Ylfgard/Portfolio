using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset dialogueText;
    [SerializeField] private bool stopTime;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool playOnStartDelayed;
    [SerializeField] private float playbackDelay;
    private DialogueHandler dialogueHandler;

    private void Start()
    {
        dialogueHandler = FindObjectOfType<DialogueHandler>();
        
        if(playOnStart) TriggerDialogue();
        else if(playOnStartDelayed) StartCoroutine(PlaybackDelayCoroutine(playbackDelay));
    }

    public void TriggerDialogue()
    {
        IDialogueEvent[] dialogueEvents = gameObject.GetComponents<IDialogueEvent>();
        dialogueHandler.StartDialogue(dialogueText, dialogueEvents, stopTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            TriggerDialogue();
            gameObject.GetComponent<Collider2D>().enabled = false;
        } 
    }

    private IEnumerator PlaybackDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerDialogue();
    }
}
