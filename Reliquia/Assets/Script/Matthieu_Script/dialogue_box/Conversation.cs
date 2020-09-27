using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line 
{
    public Speaker speaker;

    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "new conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Speaker speakerLeft;
    public Speaker speakerRight;
    public Line[] lines;
}
