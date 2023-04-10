using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
        StartCoroutine(SceneTools.TransitionToScene(SceneTools.NextSceneIndex));
    }


    void Update()
    {
        CalculateSuccessRate();
        if (success + fail >= totalNumberOfSuspects)
        {
            //StartCoroutine(FadeToBlackSystem.TryCueFadeInToBlack(timeToFade));
            NextScene();
            data.successes = success;
            data.fails = fail;
        }

    }
}
