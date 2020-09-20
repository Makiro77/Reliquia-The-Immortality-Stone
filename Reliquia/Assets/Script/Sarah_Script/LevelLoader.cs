using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen1; // Ecran de chargement à afficher
    public GameObject loadingScreen2; // Second écran de chargement
    public GameObject amulette; // Amulette remplie
    public Slider slider; // Slider de chargement

    public void Start () {
        
        loadingScreen1.SetActive(false); 
        loadingScreen2.SetActive(false); 
    }

    public void LoadLevel (int sceneIndex) {

        StartCoroutine(LoadAsynch(sceneIndex));
    }

    IEnumerator LoadAsynch (int sceneIndex) {

        /// Le code ci dessous est présent uniquement pour l'aperçu ///

        loadingScreen1.SetActive(true);

        for(float i=0;i<1.1f;i+=.1f){
            slider.value = i;
            amulette.GetComponent<CanvasGroup>().alpha = i;
            Debug.Log(i);
            yield return new WaitForSeconds(.1f);
        }

        SceneManager.LoadScene(0);

        /// La véritable fonction prenant en compte le temps de chargement du niveau est en commentaire ci-dessous ///

        /*
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen1.SetActive(true);

        while (!operation.isDone) {
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;
            Debug.Log(progress);

            yield return null;
        }*/
    }
}
