using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public Text fullName;
    public Text dialogue;
    
    private Speaker speaker;
    public Speaker Speaker
    {
        get { return speaker; }
        set {
            speaker     = value;
            portrait    = speaker.portrait;
            fullName    = speaker.fullName;
        }
    }
    public string Dialogue
    {
        set { dialogue.text = value; }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsSpeaking(Speaker crt_speaker) {
        return speaker == crt_speaker;
    }
}
