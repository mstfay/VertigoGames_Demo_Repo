using System.Collections;
using DG.Tweening;
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

    public void Initialize(RewardItemProperties itemProperties)
    {
        _rewardItemProperties = itemProperties;
        _siblingNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = "Collected_Item_Number_" + _siblingNumber;
        collectedItemIcon.sprite = itemProperties.RewardSprite;

        // Burada text animasyonunu başlatıyoruz
        AnimateText(itemProperties.RewardCount);
    }

    /// <summary>
    /// This method updates the RewardCount value and text of the RewardItemProperties component attached to the object.
    /// </summary>
    /// <param name="count"></param>
    public void UpdateCollectedItemCount(int count)
    {
        int newCount = _rewardItemProperties.RewardCount + count;
        _rewardItemProperties.RewardCount = newCount;
        AnimateText(newCount);
    }
    
    private void AnimateText(int finalValue)
    {
        int initialValue = int.Parse(collectedItemCount.text);

        DOTween.To(() => initialValue, x => initialValue = x, finalValue, 1f)
            .OnUpdate(() => collectedItemCount.text = initialValue.ToString());
    }

    private void OnValidate()
    {
        _siblingNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = "Collected_Item_Number_" + _siblingNumber;
    }
    
    public Vector3 GetPosition()
    {
        return collectedItemIcon.transform.position;
    }

    public Sprite GetSprite()
    {
        return collectedItemIcon.sprite;
    }

}