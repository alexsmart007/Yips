using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class SuccessRateSystem : MonoBehaviour
{

    private float successRate;
    private int success;
    private int fail;

    [SerializeField] private GameObject successText;
    [SerializeField] private GameObject failText;
    [SerializeField] private GameObject successRateText;
    [SerializeField] private GameObject EndText;

    [SerializeField] private int totalNumberOfSuspects;
    [SerializeField] private float timeToFade;
    [SerializeField] private float timeBeforeQuit;

    private TextMeshProUGUI SuccessText;
    private TextMeshProUGUI FailText;
    private TextMeshProUGUI SuccessRateText;

    private List<string> unlocks;

    void Start()
    {
        SuccessText = successText.GetComponent<TextMeshProUGUI>();
        SuccessText.text = "Number Of Successful Deductions: 0";
        FailText = failText.GetComponent<TextMeshProUGUI>();
        FailText.text = "Number Of Failed Deductions: 0";
        SuccessRateText = successRateText.GetComponent<TextMeshProUGUI>();
        SuccessRateText.text = "Ultimate Success Rate: 0%";
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

    private IEnumerator TimeBeforeQuit()
    {
        yield return new WaitForSecondsRealtime(timeBeforeQuit);
        Application.Quit();
    }


    void Update()
    {
        CalculateSuccessRate();
        if (success + fail >= totalNumberOfSuspects)
        {
            StartCoroutine(FadeToBlackSystem.TryCueFadeInToBlack(timeToFade));
            successText.SetActive(true);
            failText.SetActive(true);
            successRateText.SetActive(true);
            EndText.SetActive(true);
            StartCoroutine(TimeBeforeQuit());
        }
        SuccessText.text = "Number Of Successful Deductions:                " + success;
        FailText.text = "Number Of Failed Deductions:                    " + fail;
        SuccessRateText.text = "Ultimate Success Rate:                   " + successRate + "%";

    }


}
