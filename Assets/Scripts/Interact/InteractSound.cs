using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSound : MonoBehaviour
{

    [SerializeField] private AudioSource interactSound;

    private void OnEnable()
    {
        DialogueManager.OnChoiceMenuClose += Interact;
    }

    private void Interact()
    {
        interactSound.Play();
    }

    private void OnDisable()
    {
        DialogueManager.OnChoiceMenuClose -= Interact;
    }
}
