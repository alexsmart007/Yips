using System.Collections;
using UnityEngine;
using static DialogueHelperClass;

[CreateAssetMenu(fileName = "New Data", menuName = "Dialogue/Data")]
public class SOConversationData : ScriptableObject
{
    [SerializeField] ConversationData conversationData;

    public void SetConversation(ConversationData conversation)
    {
        conversationData = conversation;
    }

    public ConversationData Data => conversationData;
}
