using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William_Script : MonoBehaviour
{

    private void Start()
    {
        LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem_Script.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData_Script data = SaveSystem_Script.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
