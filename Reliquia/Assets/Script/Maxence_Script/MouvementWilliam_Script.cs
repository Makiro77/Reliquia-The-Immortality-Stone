using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using clavier;

public class MouvementWilliam_Script : MonoBehaviour
{
    RaccourciClavier_Script raccourciClavier;

    private Animator _animator;
    private CharacterController _characterController;

    public float vitesse = 5.0f;
    public float vitesseRotation = 240.0f;
    private float gravite = 20.0f;

    private Vector3 _mouvementDir = Vector3.zero;

    private Transform relativeTransform;

    public bool enMouvement;

    // Start is called before the first frame update
    void Start()
    {
        raccourciClavier = FindObjectOfType<RaccourciClavier_Script>();

        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Avancer();
        Reculer();
        Gauche();
        Droite();
    }


    public void Avancer()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Avancer"]))
        {
            enMouvement = true;

            _animator.SetBool("Reculer", false);
            _animator.SetBool("Droite", false);
            _animator.SetBool("Gauche", false);
            _animator.SetBool("Avancer", enMouvement);

            transform.position += transform.forward * vitesse * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Avancer"]))
        {
            enMouvement = false;
            _animator.SetBool("Avancer", enMouvement);
        }
    }

    public void Reculer()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Reculer"]))
        {
            enMouvement = true;


            _animator.SetBool("Avancer", false);
            _animator.SetBool("Droite", false);
            _animator.SetBool("Gauche", false);
            _animator.SetBool("Reculer", enMouvement);

            transform.position += -transform.forward * vitesse * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Reculer"]))
        {
            enMouvement = false;
            _animator.SetBool("Reculer", enMouvement);
        }
    }

    public void Gauche()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Gauche"]))
        {
            enMouvement = true;


            _animator.SetBool("Avancer", false);
            _animator.SetBool("Reculer", false);
            _animator.SetBool("Droite", false);
            _animator.SetBool("Gauche", enMouvement);

            transform.position += -transform.right * 2 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Gauche"]))
        {
            enMouvement = false;
            _animator.SetBool("Gauche", enMouvement);
        }
    }

    public void Droite()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Droite"]))
        {
            enMouvement = true;


            _animator.SetBool("Avancer", false);
            _animator.SetBool("Reculer", false);
            _animator.SetBool("Gauche", false);
            _animator.SetBool("Droite", enMouvement);

            transform.position += transform.right * 2 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Droite"]))
        {
            enMouvement = false;
            _animator.SetBool("Droite", enMouvement);
        }
    }
}
