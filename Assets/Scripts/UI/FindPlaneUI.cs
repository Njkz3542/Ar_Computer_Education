using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FindPlaneUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private TMP_Text findText;

    private const string defText = "Пошук площини";

    private ARPlaneManager aRPlaneManager;

    private void Start()
    {
        aRPlaneManager = FindObjectOfType<ARPlaneManager>();
        aRPlaneManager.planesChanged += ARPlaneManager_planesChanged;

        StartCoroutine(TextAnimation());
    }

    private void ARPlaneManager_planesChanged(ARPlanesChangedEventArgs obj)
    {
        aRPlaneManager.planesChanged -= ARPlaneManager_planesChanged;
        content.gameObject.SetActive(false);
    }

    IEnumerator TextAnimation()
    {
        while (content.gameObject.activeSelf)
        {
            findText.text = defText;
            yield return new WaitForSeconds(.3f);
            findText.text = defText + ".";
            yield return new WaitForSeconds(.3f);
            findText.text = defText + "..";
            yield return new WaitForSeconds(.3f);
            findText.text = defText + "...";
            yield return new WaitForSeconds(1f);
        }
    }
}
