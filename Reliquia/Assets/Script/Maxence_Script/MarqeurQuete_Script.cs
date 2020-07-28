using UnityEngine;
using UnityEngine.UI;

public class MarqeurQuete_Script : MonoBehaviour
{
    public Sprite icone;
    public Image image;

    public Vector2 position
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }
}
