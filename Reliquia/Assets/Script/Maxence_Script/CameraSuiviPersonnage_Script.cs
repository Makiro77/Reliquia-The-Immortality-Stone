using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuiviPersonnage_Script : MonoBehaviour
{
    MouvementWilliam_Script mouvementWilliam;


    /*private const float Y_Angle_Min = 0.0f;
    private const float Y_Angle_Max = 50.0f;*/

    private const float X_Angle_Min = -90.0f;
    private const float X_Angle_Max = 90.0f;

    public Transform Personnage;
    public Transform cameraTransform;

    private Camera cam;

    private float distance = 2.5f;
    private float currentX = 0.0f;
    //private float currentY = 0.0f;

    private void Start()
    {
        mouvementWilliam = FindObjectOfType<MouvementWilliam_Script>();

        cameraTransform = transform;
        cam = Camera.main;

        Debug.Log(cameraTransform.position);
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        //currentY += Input.GetAxis("Mouse Y");

        //currentY = Mathf.Clamp(currentY, Y_Angle_Min, Y_Angle_Max);
        if (!mouvementWilliam.enMouvement) currentX = Mathf.Clamp(currentX, X_Angle_Min, X_Angle_Max);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0.6f, 1.7f, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentX, 0);
        cameraTransform.position = Personnage.position + rotation * dir;
        cameraTransform.rotation = Quaternion.Euler(9.5f, currentX, 0);

        if(mouvementWilliam.enMouvement) Personnage.localRotation = rotation;
    }
}
