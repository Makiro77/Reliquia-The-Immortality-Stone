using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSuiviPersonnage_Script : MonoBehaviour
{
    private const float Y_Angle_Min = 0.0f;
    private const float Y_Angle_Max = 50.0f;

    private const float X_Angle_Min = -90.0f;
    private const float X_Angle_Max = 90.0f;

    public Transform regardPersonnage;
    public Transform cameraTransform;

    private Camera cam;

    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 4.0f;

    private void Start()
    {
        cameraTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        //currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_Angle_Min, Y_Angle_Max);
        //currentX = Mathf.Clamp(currentX, X_Angle_Min, X_Angle_Max);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0.51f, 2, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        cameraTransform.position = regardPersonnage.position + rotation * dir;
        cameraTransform.LookAt(regardPersonnage.position);

        regardPersonnage.localRotation = rotation;
    }
}
