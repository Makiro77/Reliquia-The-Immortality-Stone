using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class ObjetSelection : MonoBehaviour
{
    static ObjetSelection()
    {
        EditorApplication.update += () => {
            if (Selection.activeGameObject != null ||  Selection.activeGameObject != LastActiveGameObject)
                LastActiveGameObject = Selection.activeGameObject;
        };
    }

    public static GameObject LastActiveGameObject { get; private set; }

}
