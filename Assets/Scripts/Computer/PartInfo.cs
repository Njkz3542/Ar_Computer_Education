using System;
using UnityEngine;

public class PartInfo : MonoBehaviour
{
    [SerializeField, Multiline] private string info;

    [HideInInspector] public bool isComputerDismantled = false;

    public string GetInfo()
    {
        if (!isComputerDismantled) return null;
        return info;
    }
}
