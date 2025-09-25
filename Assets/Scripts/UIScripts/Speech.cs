using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SpeechEvent
{
    public float time;
    [TextArea(2, 5)]
    public string content;
}

public class Speech : MonoBehaviour
{
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TMP_Text speechText;
    [SerializeField] private List<SpeechEvent> speechEvents;

    private int currentEventIndex = 0;

    private void Start()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);

        StartCoroutine(RunSpeech());
    }

    private IEnumerator RunSpeech()
    {
        while (currentEventIndex < speechEvents.Count)
        {
            SpeechEvent currentEvent = speechEvents[currentEventIndex];
            yield return new WaitForSeconds(currentEvent.time);

            speechBubble.SetActive(true);
            speechText.text = currentEvent.content;

            yield return new WaitForSeconds(2f);

            speechBubble.SetActive(false);
            currentEventIndex++;
        }
    }
}
