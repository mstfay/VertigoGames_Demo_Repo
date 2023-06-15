using System.Collections.Generic;
using System.Linq;
using Spin;
using UnityEngine;

public class CollectedItemsPanelsManager : MonoBehaviour
{
    public static CollectedItemsPanelsManager Instance { get; private set; }
    
    [Header("General Script Properties")]
    [SerializeField] private SpinManager spinManager;
    [SerializeField] private CollectedItemNumber collectedItemNumberPrefab;
    [SerializeField] private Transform collectedItemsPanelContent;
    
    private List<KeyValuePair<RewardType, CollectedItemNumber>> _collectedItems = new List<KeyValuePair<RewardType, CollectedItemNumber>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This method creates or updates a collected item based on the provided reward. 
    /// If an item with the same RewardType already exists, it updates the count. 
    /// Otherwise, it creates a new item and adds it to the collection.
    /// </summary>
    /// <param name="reward"></param>
    public void CheckCollectedItemPrefab(int reward)
    {
        var collectedItemProperties = spinManager.rewardItems[reward];

        foreach (var item in _collectedItems.Where(item => item.Key == collectedItemProperties.RewardType))
        {
            item.Value.UpdateCollectedItemCount(collectedItemProperties.RewardCount);
            return;
        }

        var createdCollectedItem = Instantiate(collectedItemNumberPrefab, collectedItemsPanelContent);
        createdCollectedItem.Initialize(collectedItemProperties);
        _collectedItems.Add(new KeyValuePair<RewardType, CollectedItemNumber>(collectedItemProperties.RewardType, createdCollectedItem));
    }
}