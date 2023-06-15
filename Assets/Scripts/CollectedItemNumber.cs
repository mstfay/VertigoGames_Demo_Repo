using Spins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectedItemNumber : MonoBehaviour
{
    [Header("Item Visual References")]
    [SerializeField] private Image collectedItemIcon;
    [SerializeField] private TextMeshProUGUI collectedItemCount;
    
    private TextMeshProUGUI _zoneNumberText;
    private RewardItemProperties _rewardItemProperties;
    private int _siblingNumber;


    /// <summary>
    /// This method updates the object's name, sprite, and count value according to the properties provided by RewardItemProperties.
    /// </summary>
    /// <param name="itemProperties"></param>
    public void Initialize(RewardItemProperties itemProperties)
    {
        _rewardItemProperties = itemProperties;
        _siblingNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = "Collected_Item_Number_" + _siblingNumber;
        collectedItemIcon.sprite = itemProperties.RewardSprite;
        collectedItemCount.text = itemProperties.RewardCount.ToString();
    }

    /// <summary>
    /// This method updates the RewardCount value and text of the RewardItemProperties component attached to the object.
    /// </summary>
    /// <param name="count"></param>
    public void UpdateCollectedItemCount(int count)
    {
        _rewardItemProperties.RewardCount += count;
        collectedItemCount.text = _rewardItemProperties.RewardCount.ToString();
    }
    
    private void OnValidate()
    {
        _siblingNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = "Collected_Item_Number_" + _siblingNumber;
    }
}