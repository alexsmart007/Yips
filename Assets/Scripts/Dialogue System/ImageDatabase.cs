using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/ImageDatabase")]
public class ImageDatabase : SerializedScriptableObject
{
    [SerializeField] Dictionary<string, Sprite> portraits;

    public Sprite GetPortrait(string characterName)
    {
        if(portraits.TryGetValue(characterName.ToLower(), out var sprite))
        {
            return sprite;
        }

        Debug.Log("Could not find " + characterName);
        return null;
    }
}