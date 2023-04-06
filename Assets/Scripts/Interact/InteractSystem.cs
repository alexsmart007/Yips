using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.Utilities;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] private float rangeOfInteract = 2;
    [SerializeField] private AudioSource interactSound;
    private IInteractable item;
    private GameObject glowPlane;
    private Dictionary<GameObject, Boolean> objectsHit = new Dictionary<GameObject, bool>();
    //[SerializeField] private SOConversationData startingDialogue;
    private bool hasHitItem;
    private bool hasHitItem1;
    private GameObject closestItem;

    private void OnEnable()
    {
        Controller.OnInteract += Interact;
    }

    private void OnDisable()
    {
        Controller.OnInteract -= Interact;
    }

    void Start()
    {
        //DialogueManager.Instance.StartDialogue(startingDialogue);
    }

    void Update()
    {
        RaycastHit2D[] hits;
        float minDistance = 1000f;
        hits = Physics2D.CircleCastAll(transform.position, rangeOfInteract, transform.up, 0);
        Debug.DrawRay(transform.position, transform.up, Color.green);
        foreach (RaycastHit2D hit in hits)
        {
                if (hit.collider.gameObject.CompareTag("Item") || hit.collider.gameObject.CompareTag("Key"))
                {
                hasHitItem = true;
                    if (!objectsHit.ContainsKey(hit.collider.gameObject)) objectsHit.Add(hit.collider.gameObject, false);
                    if (hit.distance < minDistance)
                    {
                        closestItem = hit.collider.gameObject;
                        minDistance = hit.distance;
                    }
                }
        }
        if(!hasHitItem) foreach (GameObject key in objectsHit.Keys.ToList()) objectsHit[key] = false;
        else
        {
            foreach (GameObject key in objectsHit.Keys.ToList()) objectsHit[key] = false;
            objectsHit[closestItem] = true;
        }
        hasHitItem= false;
        objectsHit.Where(x => x.Value).ToList().ForEach(x => Glow(x.Key));
        objectsHit.Where(x => !x.Value).ToList().ForEach(x => NotGlow(x.Key));
    }

    void Glow(GameObject objectToGlow)
    {
        if (objectToGlow.transform.GetChild(0) != null)
        {
            objectToGlow.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void NotGlow(GameObject objectToNotGlow)
    {
        if (objectToNotGlow.transform.GetChild(0) != null)
        {
            objectToNotGlow.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Interact()
    {
        RaycastHit2D[] hits;
        float minDistance = 1000f;
        hits = Physics2D.CircleCastAll(transform.position, rangeOfInteract, transform.up, 0);
        foreach (RaycastHit2D hit in hits)
        {
                if (hit.collider.gameObject.CompareTag("Item") || hit.collider.gameObject.CompareTag("Key"))
                {
                    hasHitItem1 = true;
                    if (hit.distance < minDistance)
                    {
                        item = hit.collider.gameObject.GetComponent<IInteractable>();
                        minDistance = hit.distance;
                    }
                }
        }
        if (hasHitItem1)
        {
            if (item.ExecuteDialogue()) interactSound.Play();
            item.OpenDoor();
        }
        hasHitItem1 = false;
    }
}
