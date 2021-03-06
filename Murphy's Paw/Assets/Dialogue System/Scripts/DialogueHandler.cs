using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject sentencePref;
    [HideInInspector] public UnityEvent showNextSentenceEvent;
    private List<IDialogueEvent> dialogueEvents = new List<IDialogueEvent>();
    private int curSentenceIndex = 0;
    private DialogueViewer curDialogue;
    private List<GameObject> sentences = new List<GameObject>();

    private void Awake() 
    {
        EndDialogue();
    }

    private void Update() 
    {
        if(curDialogue != null)
        {
            if(Input.GetButtonDown("Next"))
                NextSentence();
            if(Input.GetButtonDown("Skip"))
                EndDialogue();
        }
        
    }

    public void StartDialogue(TextAsset dialogue, IDialogueEvent[] dialEvs, bool pauseGame)
    {
        ClearSentences();
        curSentenceIndex = 0;
        curDialogue = DialogueViewer.Load(dialogue);
        if(pauseGame) GamePauser.StopGame(gameObject);
        foreach(IDialogueEvent ev in dialEvs)
            dialogueEvents.Add(ev);
        dialogueFrame.SetActive(true);
        NextSentence();
    }

    public void NextSentence()
    {
        if(curDialogue != null && curSentenceIndex < curDialogue.sentences.Length)
        {
            var curSentence = curDialogue.sentences[curSentenceIndex];
            showNextSentenceEvent?.Invoke();
            if(dialogueEvents.Count > 0 && curSentence.triggerEvent) // Запускает первое в очереди событие и стерает его
            {
                if(curSentence.fmodSoundPath != "")
                {
                    FMOD.Studio.EventDescription discription;
                    FMODUnity.RuntimeManager.CreateInstance(curSentence.fmodSoundPath).getDescription(out discription);
                    int lenght;
                    discription.getLength(out lenght);
                    dialogueEvents[0].PlayEvent(lenght/1000 + 0.7f);
                }
                else dialogueEvents[0].PlayEvent(0);
                dialogueEvents.Remove(dialogueEvents[0]);
            }
            SpawnSentence(curSentence.text, curSentence.fmodSoundPath, curSentence.duration);
            curSentenceIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void SpawnSentence(string sentenceText, string fmodSoundPath, float delayBeforeShowNext)
    {
        GameObject sentence = Instantiate(sentencePref, contentTransform);
        sentences.Add(sentence);

        SentenceShower sentenceShower = sentence.GetComponent<SentenceShower>();
        sentenceShower.dialogueHandler = this;
        sentenceShower.refSentenceText = sentenceText;
        sentenceShower.fmodSoundPath = fmodSoundPath;
        sentenceShower.delayBeforeShowNext = delayBeforeShowNext;
    }

    private void ClearSentences()
    {
        foreach(GameObject sentence in sentences)
            Destroy(sentence);
        sentences.Clear();
        showNextSentenceEvent?.RemoveAllListeners();
        dialogueEvents?.Clear();
    }

    public void EndDialogue()
    {
        ClearSentences();
        dialogueFrame.SetActive(false);
        curDialogue = null;
        GamePauser.ContinueGame(gameObject);
    }
}
