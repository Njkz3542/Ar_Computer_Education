using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ScopeManager : MonoBehaviour
{
    [SerializeField] private GameObject scopePrefab;

    private GameObject scope;

    private ARRaycastManager aRRaycastManager;
    private Camera mainCamera;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        mainCamera = Camera.main;

        if (!scope)
            scope = Instantiate(scopePrefab);
    }

    void Update()
    {
        if (!scope) return;

        List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(Input.mousePosition, arRaycastHits, TrackableType.Planes);

        if (arRaycastHits.Count > 0)
        {
            scope.transform.position = arRaycastHits[0].pose.position;
        }
    }
}
