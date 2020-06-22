using clavier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    RaccourciClavier_Script raccourciClavier;

    [SerializeField] private GameObject MenuPause;
    bool voirMenu;

    // Start is called before the first frame update
    void Start()
    {
        voirMenu = false;
        MenuPause.SetActive(voirMenu);
        raccourciClavier = FindObjectOfType<RaccourciClavier_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["MenuPause"]))
        {
            voirMenu = !voirMenu;
            MenuPause.SetActive(voirMenu);
        }
    }
}
