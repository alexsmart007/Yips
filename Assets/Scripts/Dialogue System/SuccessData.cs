using UnityEngine;

[CreateAssetMenu(fileName = "New Success Data", menuName = "Succcess Data")]
public class SuccessData : ScriptableObject
{
    public float successes;
    public float fails;
    public float SuccessRate => successes / (fails + successes); 
}
