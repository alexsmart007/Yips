using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class JsonDialogueConverterWindow : OdinEditorWindow
{
    [SerializeField, TextArea(10, 40)] string box;

    [MenuItem("Tools/Json Converter")]
    private static void OpenWindow()
    {
        GetWindow<JsonDialogueConverterWindow>().Show();
    }

    [Button]
    public void TryToConvert()
    {
        JsonDialogueConverter.ConvertToJson(box);
    }
}
