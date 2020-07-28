﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compas_Script : MonoBehaviour
{
    public GameObject iconePrefab;
    GameObject newMarqueur;
    List<MarqeurQuete_Script> marqueurQuete = new List<MarqeurQuete_Script>();

    public RawImage compasImage;
    public Transform player;

    public float maxDistance = 50f;

    float CompasUnite;

    public MarqeurQuete_Script one;
    public MarqeurQuete_Script two;

    private void Start()
    {
        CompasUnite = compasImage.rectTransform.rect.width / 360f;

        AddMarqueurQuete(one);
        AddMarqueurQuete(two);
    }

    void Update()
    {
        compasImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(MarqeurQuete_Script marqueur in marqueurQuete)
        {
            marqueur.image.rectTransform.anchoredPosition = GetPosOnCompas(marqueur);
            
            float dist = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marqueur.position);
            float scale = 0f;

            if (dist < maxDistance) scale = 1f - (dist / maxDistance);

            marqueur.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void AddMarqueurQuete (MarqeurQuete_Script marqueur)
    {
        newMarqueur = Instantiate(iconePrefab, compasImage.transform);
        marqueur.image = newMarqueur.GetComponent<Image>();
        marqueur.image.sprite = marqueur.icone;

        marqueurQuete.Add(marqueur);
    }

    public void RemoveMarqueurQuete (MarqeurQuete_Script marqueur)
    {
        Destroy(newMarqueur);
        marqueurQuete.Remove(marqueur);
    }

    Vector2 GetPosOnCompas (MarqeurQuete_Script marqueur)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marqueur.position - playerPos, playerFwd);

        return new Vector2(CompasUnite * angle, 0f);
    }
}