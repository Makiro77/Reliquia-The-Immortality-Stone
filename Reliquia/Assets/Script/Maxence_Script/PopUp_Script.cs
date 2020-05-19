using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUp_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.localPosition = new Vector3(1520, 0, 0);
        transform.DOMoveX(0f, 0.5f).SetEase(Ease.OutBack);
    }

    public void quitterJeu()
    {
        Application.Quit();
    }

    public void fermerPopUp()
    {
        transform.DOMoveX(-1630f, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
