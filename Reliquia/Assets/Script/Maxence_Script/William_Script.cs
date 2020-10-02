﻿using clavier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William_Script : MonoBehaviour
{

    GameManager gameManager;
    RaccourciClavier_Script raccourciClavier;
    Compas_Script compas_Script;

    public Inventaire_Script inventaire;

    private CharacterController characterController;

    [SerializeField] private GameObject ParentMenu;

    public static William_Script instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        raccourciClavier = FindObjectOfType<RaccourciClavier_Script>();
        compas_Script = FindObjectOfType<Compas_Script>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(mItemToPickUp != null && Input.GetKeyDown(raccourciClavier.toucheClavier["Action"]))
        {
            inventaire.AddItem(mItemToPickUp);
            mItemToPickUp.OnPickup();
            gameManager.FermerMessageInteraction();
        }
    }

    private IInventaireItem mItemToPickUp = null;
    private void OnTriggerEnter(Collider other)
    {
        IInventaireItem item = other.GetComponent<IInventaireItem>();
        if (item != null)
        {
            mItemToPickUp = item;

            gameManager.AfficherMessageInteraction("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventaireItem item = other.GetComponent<IInventaireItem>();
        if (item != null)
        {
            gameManager.FermerMessageInteraction();
            mItemToPickUp = null;
        }
    }
}
