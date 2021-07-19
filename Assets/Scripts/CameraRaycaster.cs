using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private GameObject scopeObject;

    private Camera camera;

    private void Start()
    {
        scopeObject.SetActive(false);
        camera = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out PartInfo partInfo))
            {
                scopeObject.SetActive(true);
                scopeObject.transform.position = hit.point;
                infoText.text = partInfo.GetInfo();
            }
        }
        else
        {
            scopeObject.SetActive(false); 
            infoText.text = null;
        }
    }
}
