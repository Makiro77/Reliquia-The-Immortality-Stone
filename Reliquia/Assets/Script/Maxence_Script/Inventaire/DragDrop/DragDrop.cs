﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvas = GameObject.FindGameObjectWithTag("HUD").GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDown");
        canvasGroup.DOFade(0.6f, 0.5f);
        canvasGroup.blocksRaycasts = false;
        eventData.pointerDrag.transform.parent = canvas.transform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDown");
        canvasGroup.DOFade(1f, 0.5f);
        canvasGroup.blocksRaycasts = true;
    }
}