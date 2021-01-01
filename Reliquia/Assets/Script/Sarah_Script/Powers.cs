using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    public Animator _animator;
    public GameObject lighting;
    public Transform tlighting;
    public bool isCreated;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void InstantiateSpell() {

        Instantiate(lighting, transform);
    }
   
    public void Update()
    {   

        if (Input.GetKey(/*raccourciClavier.toucheClavier["Pouvoir 1"]*/KeyCode.E)) {

            if(!isCreated) {
                Instantiate(lighting, tlighting);
                isCreated = true;
            }
            _animator.SetBool("Lighting", true);
        }

        if (Input.GetKey(/*raccourciClavier.toucheClavier["Pouvoir 1"]*/KeyCode.R)) {

            _animator.SetBool("Lighting", false);
        }
    }
}
