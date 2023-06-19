using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CollectRewardsPanelManager : MonoBehaviour
{
    [SerializeField] private CollectRewardsPanelButtons panelButtonPrefab;
    [SerializeField] private Transform buttonsScrollViewContentParent;

    [SerializeField] private List<Button> panelButtons = new List<Button>();

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
            CollectRewardsPanelButtons createdButton = Instantiate(panelButtonPrefab, buttonsScrollViewContentParent);
            panelButtons.Add(createdButton.GetButton());
        }
    }

    private void Awake()
    {
        panelButtons[0].onClick.AddListener(SetCollectButtonProperties);
        panelButtons[1].onClick.AddListener(SetBackButtonProperties);
    }

    private void SetCollectButtonProperties()
    {
        Debug.Log("Button Clicked");
    }
    
    private void SetBackButtonProperties()
    {
        gameObject.SetActive(false);
    }
}