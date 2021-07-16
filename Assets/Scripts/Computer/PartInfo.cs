using System;
using UnityEngine;

public class PartInfo : MonoBehaviour
{
    [SerializeField] private GameObject textObj;
    private Transform mainCamera;

    public bool isComputerDismantled = false;
    
    private void Start()
    {
        textObj.SetActive(false);
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (isComputerDismantled && Vector3.Distance(transform.position, mainCamera.position) < 5f)
        {
            textObj.SetActive(true);
            textObj.transform.LookAt(mainCamera.forward * 1000);
        }
        else
        {
            textObj.SetActive(false);
        }
    }
}
