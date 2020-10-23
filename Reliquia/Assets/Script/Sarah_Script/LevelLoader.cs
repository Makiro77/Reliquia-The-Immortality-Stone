using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelLoader : MonoBehaviour {

    public GameObject canvasToFade;
    public GameObject amulette; // Amulette remplie
    public Text loadingPoints; // "Chargement..."
    public int sceneIndex;

    void Start() {

        StartCoroutine(LoadAsynch(sceneIndex));
    }

    IEnumerator LoadAsynch(int sceneIndex) {

        /// Le code ci-dessous est présent uniquement pour l'aperçu ///
        /*
        loadingScreen1.SetActive(true);

        for(float i=0;i<1.1f;i+=.1f){

            if (loadingPoints.text.Length == 13)
                loadingPoints.text = "Chargement";

            else
                loadingPoints.text = loadingPoints.text + ".";

            amulette.GetComponent<CanvasGroup>().DOFade(i, 0.3f);
            Debug.Log(i);
            yield return new WaitForSeconds(.3f);
        }

        SceneManager.LoadScene(0);*/

        /// La véritable fonction prenant en compte le temps de chargement du niveau est en commentaire ci-dessous ///
        
        canvasToFade.GetComponent<CanvasGroup>().DOFade(0, .9f);
        yield return new WaitUntil(() => canvasToFade.GetComponent<CanvasGroup>().alpha == 0);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone) {

            if (loadingPoints.text.Length == 13)
                loadingPoints.text = "Chargement";

            else
                loadingPoints.text = loadingPoints.text + ".";
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            /* Génère une erreur
            amulette.GetComponent<CanvasGroup>().DOFade(progress, 0.3f);*/
            Debug.Log(progress);

            if (progress == .9f) {
                canvasToFade.GetComponent<CanvasGroup>().DOFade(1, .9f);
                yield return new WaitUntil(() => canvasToFade.GetComponent<CanvasGroup>().alpha == 1);
            }

            yield return null;
        }
    }
}
