using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CursorController : MonoBehaviour
{
    [SerializeField] private InteractablesManager interactablesManager;

    [SerializeField] private Transform newSelectionTransform;
    private Transform currentSelectionTransform;

    [SerializeField] private float distanceThreshold;

    [SerializeField] private Texture2D interactiveCursorTexture;

    private Cursor cursor;

    private bool cursorIsInteractive = false;

    private Vector2 inputPositionVector;

    [SerializeField] private AudioSource interactSound;

    private void OnEnable()
    {
        Controller.OnPosition += Position;
        Controller.OnClick += Click;
    }

    private void OnDisable()
    {
        Controller.OnPosition -= Position;
        Controller.OnClick -= Click;
    }

    void Update()
    {
        FindInteractableWithinDistanceThreshold();
    }

    private void FindInteractableWithinDistanceThreshold()
    {
        newSelectionTransform =null;

        for(int itemIndex=0; itemIndex<interactablesManager.Interactables.Count; itemIndex++)
        {
            Vector2 fromMouseToInteractableOffset = interactablesManager.Interactables[itemIndex].position - new Vector3(inputPositionVector.x, inputPositionVector.y, 0f);
            if(fromMouseToInteractableOffset.sqrMagnitude < distanceThreshold * distanceThreshold) 
            {
                //Found an interable exit out of loop
                newSelectionTransform = interactablesManager.Interactables[itemIndex].transform;
                if (!cursorIsInteractive) InteractiveCursorTexture();
                break;
            }        
        }

        if(currentSelectionTransform != newSelectionTransform)
        {
            //Make CursorDefault no interactable found
            currentSelectionTransform = newSelectionTransform;
            DefaultCursorTexture();
        }
    }

    void Position(Vector2 input)
    {
        if (!Controller.Instance.InGameplay) return;

        inputPositionVector = input;
    }

    void Click()
    {
        if (!Controller.Instance.InGameplay) return;
        OnClickInteractable();
    }

    private void OnClickInteractable()
    {
        if(newSelectionTransform != null)
        {
            IInteractable interactable = newSelectionTransform.gameObject.GetComponent<IInteractable>();
            if(interactable != null)
            {
                if (interactable.ExecuteDialogue()) interactSound.Play();
                interactable.OpenDoor();
            }
            newSelectionTransform = null;
        }
    }

    private void InteractiveCursorTexture()
    {
        cursorIsInteractive = true;
        Vector2 hotspot = new Vector2(interactiveCursorTexture.width / 2, 0);
        Cursor.SetCursor(interactiveCursorTexture, hotspot, CursorMode.Auto);
    }

    private void DefaultCursorTexture()
    {
        cursorIsInteractive = false;
        Cursor.SetCursor(default, default, default);
    }
}
