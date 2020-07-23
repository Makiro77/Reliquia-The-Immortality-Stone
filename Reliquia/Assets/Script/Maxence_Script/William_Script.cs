using clavier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William_Script : MonoBehaviour
{

    public GameManager gameManager;
    RaccourciClavier_Script raccourciClavier;

    public Inventaire_Script inventaire;

    private CharacterController characterController;

    [SerializeField] private GameObject ParentMenu;

    private void Start()
    {
        LoadPlayer();
        characterController = GetComponent<CharacterController>();
        raccourciClavier = FindObjectOfType<RaccourciClavier_Script>();
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
