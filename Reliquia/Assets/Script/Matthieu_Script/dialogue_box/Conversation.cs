using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line 
{
    public Character speaker;

    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "new conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
}
