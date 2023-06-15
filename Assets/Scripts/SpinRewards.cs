using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spins
{
    [Serializable]
    public struct RewardItemProperties
    {
        [SerializeField] private RewardType rewardType;
        [SerializeField] private Sprite rewardSprite;
        [SerializeField] private int rewardCount;
        
        public RewardType RewardType => rewardType;
        public Sprite RewardSprite => rewardSprite;
        public int RewardCount => rewardCount;
    }

    [CreateAssetMenu(menuName = "SpinSystem/RewardSystem/SpinRewards", fileName= "SpinRewards")]
    public class SpinRewards : ScriptableObject
    {
        public List<RewardItemProperties> rewardItem;
    }
}

