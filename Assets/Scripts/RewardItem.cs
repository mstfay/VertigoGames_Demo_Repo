using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Spins;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private Image rewardIcon;
    [SerializeField] private TextMeshProUGUI rewardCount;

    public void Initialize(RewardItemProperties rewardItem)
    {
        rewardIcon.sprite = rewardItem.RewardSprite;
        rewardCount.text = rewardItem.RewardCount.ToString();
    }
}
