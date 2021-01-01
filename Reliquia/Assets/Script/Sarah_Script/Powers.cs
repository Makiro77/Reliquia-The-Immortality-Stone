using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    public Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }
   
    public void Update()
    {   

        if (Input.GetKey(/*raccourciClavier.toucheClavier["Pouvoir 1"]*/KeyCode.E)) {

            _animator.SetBool("Lighting", true);
        }

        if (Input.GetKey(/*raccourciClavier.toucheClavier["Pouvoir 1"]*/KeyCode.R)) {

            _animator.SetBool("Lighting", false);
        }
    }
}
