﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EffetBouton_Script : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private bool changementCouleurText = true;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (changementCouleurText) gameObject.GetComponentInChildren<Text>().color = new Color32(105,105,105,255);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (changementCouleurText) gameObject.GetComponentInChildren<Text>().color = Color.white;
    }
}
