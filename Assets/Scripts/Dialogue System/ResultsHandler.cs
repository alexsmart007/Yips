using UnityEngine;
using TMPro;

public class ResultsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI successText;
    [SerializeField] private TextMeshProUGUI failText;
    [SerializeField] private TextMeshProUGUI successRateText;
    [SerializeField] SuccessData data;

    private void Start()
    {
        successText.text = "Number Of Successful Deductions: " + data.successes;
        failText.text = "Number Of Failed Deductions: " + data.fails;
        successRateText.text = "Ultimate Success Rate: " + data.SuccessRate * 100 + "%"; 
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        StartCoroutine(SceneTools.TransitionToScene(0));
    }

}