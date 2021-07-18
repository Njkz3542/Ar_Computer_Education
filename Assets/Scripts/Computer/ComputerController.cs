using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public struct PartData
{
    public Transform Part;
    public Transform StartPosition;
    public Transform Destination;
}

public class ComputerController : MonoBehaviour
{
    [SerializeField] private float deltaTimeMove = 1.5f;
    [SerializeField] private List<PartData> Parts;

    private bool canDismantle = true;
    private bool isComputerDismantled = false;

    private PCSpawner pCSpawner;

    private void Start()
    {
        pCSpawner = FindObjectOfType<PCSpawner>();
    }

    private void Update()
    {
        if (!pCSpawner.IsSpanwed) return;

        if (canDismantle && Input.GetMouseButtonDown(0))
        {
            if (isComputerDismantled)
            {
                isComputerDismantled = false;
                StartCoroutine(Collect());
            }
            else
            {
                isComputerDismantled = true;
                StartCoroutine(Dismantle());
            }
        }
    }

    private IEnumerator Dismantle()
    {
        canDismantle = false;
        
        for (int i = 0; i < Parts.Count; i++)
        {
            Parts[i].Part.DOMove(Parts[i].Destination.position, deltaTimeMove).SetEase(Ease.Linear);
            Parts[i].Part.DORotate(Parts[i].Destination.rotation.eulerAngles, deltaTimeMove).SetEase(Ease.Linear);
            PartInfo partInfo;
            if(Parts[i].Part.TryGetComponent(out partInfo))
            {
                partInfo.isComputerDismantled = true;
            }
            
            yield return new WaitForSeconds(deltaTimeMove);
        }
        
        canDismantle = true;
    }
    
    private IEnumerator Collect()
    {
        canDismantle = false;

        for (int i = Parts.Count - 1; i >= 0; i--)
        {
            Parts[i].Part.DOMove(Parts[i].StartPosition.position, deltaTimeMove).SetEase(Ease.Linear);
            Parts[i].Part.DORotate(Parts[i].StartPosition.rotation.eulerAngles, deltaTimeMove).SetEase(Ease.Linear);
            PartInfo partInfo;
            if(Parts[i].Part.TryGetComponent(out partInfo))
            {
                partInfo.isComputerDismantled = false;
            }
            
            yield return new WaitForSeconds(deltaTimeMove);
        }

        canDismantle = true;
    }
}
