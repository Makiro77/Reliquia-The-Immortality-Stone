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
            _animator.SetBool("Reculer", false);
            _animator.SetBool("Droite", false);
            _animator.SetBool("Gauche", false);
            _animator.SetBool("Avancer", true);

            transform.position += transform.forward * vitesse * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Avancer"])) _animator.SetBool("Avancer", false);
    }

    public void Reculer()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Reculer"]))
        {
            _animator.SetBool("Avancer", false);
            _animator.SetBool("Droite", false);
            _animator.SetBool("Gauche", false);
            _animator.SetBool("Reculer", true);

            transform.position += -transform.forward * vitesse * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Reculer"])) _animator.SetBool("Reculer", false);
    }

    public void Gauche()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Gauche"]))
        {
            _animator.SetBool("Avancer", false);
            _animator.SetBool("Reculer", false);
            _animator.SetBool("Droite", false);
            _animator.SetBool("Gauche", true);

            transform.position += -transform.right * 2 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Gauche"])) _animator.SetBool("Gauche", false);
    }

    public void Droite()
    {
        if (Input.GetKey(raccourciClavier.toucheClavier["Droite"]))
        {
            _animator.SetBool("Avancer", false);
            _animator.SetBool("Reculer", false);
            _animator.SetBool("Gauche", false);
            _animator.SetBool("Droite", true);

            transform.position += transform.right * 2 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(raccourciClavier.toucheClavier["Droite"])) _animator.SetBool("Droite", false);
    }
}
