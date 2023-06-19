using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spins
{
    [Serializable]
    public struct RewardItemProperties
    {
        [SerializeField] private RewardTypes rewardType;
        [SerializeField] private Sprite rewardSprite;
        [SerializeField] private int rewardCount;
        
        public RewardTypes RewardType => rewardType;
        public Sprite RewardSprite => rewardSprite;
        public int RewardCount
        {
            get => rewardCount;

            set => rewardCount = value;
        }
    }

    [CreateAssetMenu(menuName = "SpinSystem/RewardSystem/SpinRewards", fileName= "SpinRewards")]
    public class SpinRewards : ScriptableObject
    {
        public List<RewardItemProperties> rewardItem;
    }
}

