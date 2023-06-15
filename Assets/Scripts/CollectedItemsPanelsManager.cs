using System.Collections;
using System.Collections.Generic;
using Spin;
using UnityEngine;

public class CollectedItemsPanelsManager : MonoBehaviour
{
    public static CollectedItemsPanelsManager Instance { get; private set; } // Static instance
    [SerializeField] private CollectedItemNumber collectedItemNumberPrefab;
    [SerializeField] private Transform collectedItemsPanelContent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // If another instance already exists, destroy this one
        }
    }

    public void CreateCollectedItemPrefab(int reward)
    {
        SpinManager spinManager = FindObjectOfType<SpinManager>();
        CollectedItemNumber createdCollectedItem = Instantiate(collectedItemNumberPrefab, collectedItemsPanelContent);
        createdCollectedItem.Initialize(spinManager.rewardItems[reward]);
        
        
        //Debug.Log(spinManager.rewardItems[reward].RewardSprite.name);
        //Debug.Log(spinManager.rewardItems[reward].RewardCount);
    }
}