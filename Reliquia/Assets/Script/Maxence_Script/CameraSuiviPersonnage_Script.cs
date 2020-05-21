using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuiviPersonnage_Script : MonoBehaviour
{
    public Transform personnageTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFacteur = 0.5f;

    public bool regardePersonnage = false;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - personnageTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = personnageTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFacteur);
    }
}
