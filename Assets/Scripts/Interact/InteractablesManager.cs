using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractablesManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Transform> interactables;
    private List<Vector3> orginialPositions;

    public List<Transform> Interactables
    {
        get => interactables;
    }

    // Start is called before the first frame update
    void Start()
    {
        orginialPositions= new List<Vector3>(); 
        for (int i = 0; i < transform.childCount; i++)
        {
            orginialPositions.Add(transform.GetChild(i).position);
        }
    }

    void Update()
    {
        AllChildrenWorldToScreenPoint();
    }

    private void AllChildrenWorldToScreenPoint()
    {
        for(int i=0;i<orginialPositions.Count; i++)
        {
            transform.GetChild(i).position = mainCamera.WorldToScreenPoint(orginialPositions[i]);
        }
    }
}
