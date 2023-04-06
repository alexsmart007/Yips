using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransistionSceneManager : MonoBehaviour
{
    [SerializeField] float transitionLength;

    void Start()
    {
        StartCoroutine(HandleSplashScreen());
    }

    private IEnumerator HandleSplashScreen()
    {
        yield return new WaitForSeconds(transitionLength);

        var nextScene = SceneTools.NextSceneExists ? SceneTools.NextSceneIndex : 0;
        StartCoroutine(SceneTools.TransitionToScene(nextScene));
    }
}
