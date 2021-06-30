using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PCPrefab;

    private ARPlaneManager aRPlaneManager;
    private ARRaycastManager aRRaycastManager;

    bool isFindPlane;
    bool isSpanwed;
    private void Start()
    {
        aRPlaneManager = FindObjectOfType<ARPlaneManager>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();

        aRPlaneManager.planesChanged += ARPlaneManager_planesChanged;
    }

    private void ARPlaneManager_planesChanged(ARPlanesChangedEventArgs obj)
    {
        aRPlaneManager.planesChanged -= ARPlaneManager_planesChanged;
        isFindPlane = true;
    }

    private void Update()
    {
        if (isSpanwed) return;
        if (!isFindPlane) return;

        if (Input.touchCount > 0)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            aRRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                SpawnPC(hits[0].pose.position);
            }
        }
    }

    private void SpawnPC(Vector3 pos)
    {
        //Vector3 spawnPos = new Vector3(0, -1, 2);
        Instantiate(PCPrefab, pos, Quaternion.identity);
        isSpanwed = true;
    }
}
