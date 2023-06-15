using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZonesPanelManager : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    

    private void Awake()
    {
        scrollRect.enabled = false;
    }

    
}
