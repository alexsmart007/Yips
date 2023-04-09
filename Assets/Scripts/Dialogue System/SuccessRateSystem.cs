using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SuccessRateSystem : MonoBehaviour
{

    private float successRate;
    private int success;
    private int fail;

    private List<string> unlocks;

    public float CalculateSuccessRate()
    {
        unlocks = DialogueManager.Instance.DialogueUnlocks;
        foreach (string unlock in unlocks) 
        {
            if(unlock.Contains('_'))
            {
                if (unlock.Split('_')[1].Equals("success"))
                {
                    success++;
                }
                else if(unlock.Split('_')[1].Equals("fail"))
                {
                    fail++;
                }
            }
        }
        if(fail+success != 0) successRate = success / (float)(fail + success);
        print("Success " + success);
        print("Fail " + fail);
        success = 0;
        fail = 0;
        return successRate;
    }


    void Update()
    {
        print("Success Rate: " + CalculateSuccessRate());
    }

}
