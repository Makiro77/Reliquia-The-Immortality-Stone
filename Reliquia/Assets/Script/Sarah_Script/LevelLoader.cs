﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public void LoadLevel (int sceneIndex) {

        StartCoroutine(LoadAsynch(sceneIndex));
        
    }

    IEnumerator LoadAsynch (int sceneIndex) {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone) {
            
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
