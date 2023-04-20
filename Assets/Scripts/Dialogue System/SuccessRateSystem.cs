using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class SuccessRateSystem : MonoBehaviour
{

    private float successRate;
    private int success;
    private int fail;

    [SerializeField] private SuccessData data;

    [SerializeField] private int totalNumberOfSuspects;
    [SerializeField] private float timeToFade;
    [SerializeField] private float timeBeforeQuit;

    private List<string> unlocks;

    private void OnEnable()
    {
        Controller.OnQuit += NextScene;
    }

    private void OnDisable()
    {
        Controller.OnQuit -= NextScene;
    }


    public float CalculateSuccessRate()
    {
        int numOfSuccess = 0;
        int numOfFail = 0;
        unlocks = DialogueManager.Instance.DialogueUnlocks;
        foreach (string unlock in unlocks) 
        {
            if(unlock.Contains('_'))
            {
                if (unlock.Split('_')[1].Equals("success"))
                {
                    numOfSuccess++;
                }
                else if(unlock.Split('_')[1].Equals("fail"))
                {
                    numOfFail++;
                }
            }
        }
        if(numOfFail + numOfSuccess != 0) successRate = numOfSuccess / (float)(numOfFail + numOfSuccess);

        success = numOfSuccess;
        fail = numOfFail;
        return successRate;
    }

    public void NextScene()
    {
        CalculateSuccessRate();
        StartCoroutine(SceneTools.TransitionToScene(SceneTools.NextSceneIndex));
    }


    void Update()
    {
        CalculateSuccessRate();
        data.successes = success;
        data.fails = fail;
        if (success + fail >= totalNumberOfSuspects)
        {
            //StartCoroutine(FadeToBlackSystem.TryCueFadeInToBlack(timeToFade));
            NextScene();

        }

    }
}
