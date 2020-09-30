using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    public Conversation conversation;
    public GameObject speakerGlobal;
    public GameObject speakerLeft;
    public GameObject speakerRight;
    
    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    void Start()
    {
        speakerUILeft  = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker  = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;        
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) {
            AdvanceConversation();
        }        
    }

    void AdvanceConversation() {
        if (activeLineIndex < conversation.lines.Length) {
            DisplayLine();
            activeLineIndex += 1;
        } else {
            speakerGlobal.SetActive(false);
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineIndex = 0;
        }
    }

    void DisplayLine() {
        Line line = conversation.lines[activeLineIndex];
        Character speaker = line.speaker;

        if (speakerUILeft.IsSpeaking(speaker)) {
            SetDialogue(speakerUILeft, speakerUIRight, line.text);
        } else {
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
        }
    }

    void SetDialogue(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text) {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();
        speakerGlobal.SetActive(true);
    }
}
