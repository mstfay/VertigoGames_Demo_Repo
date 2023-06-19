using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CollectRewardsPanelManager : MonoBehaviour
{
    [SerializeField] private CollectRewardsPanelButtons panelButtonPrefab;
    [SerializeField] private Transform buttonsScrollViewContentParent;

#if UNITY_EDITOR
    private void OnValidate()
    {
        CheckTheButtonsCount();
    }
#endif

    private void CheckTheButtonsCount()
    {
        if (buttonsScrollViewContentParent is null || panelButtonPrefab is null)
        {
            Debug.LogWarning("Please set the Buttons Scroll View Content Parent and Panel Button Prefab fields in the inspector.");
            return;
        }

        int currentChildCount = buttonsScrollViewContentParent.childCount;

        if (currentChildCount >= 2) return;
        for (int i = currentChildCount; i < 2; i++)
        {
            Instantiate(panelButtonPrefab, buttonsScrollViewContentParent);
        }
    }
}