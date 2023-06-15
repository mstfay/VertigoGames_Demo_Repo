using Spins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectedItemNumber : MonoBehaviour
{
    [SerializeField] private Image collectedItemIcon;
    [SerializeField] private TextMeshProUGUI collectedItemCount;
    private TextMeshProUGUI _zoneNumberText;
    private int _siblingNumber;


    public void Initialize(RewardItemProperties itemProperties)
    {
        _siblingNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = "Collected_Item_Number_" + _siblingNumber;
        collectedItemIcon.sprite = itemProperties.RewardSprite;
        collectedItemCount.text = itemProperties.RewardCount.ToString();
    }
    
    private void OnValidate()
    {
        _siblingNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = "Collected_Item_Number_" + _siblingNumber;
    }
}