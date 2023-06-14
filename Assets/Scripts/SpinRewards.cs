using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spins
{
    [Serializable]
    public struct RewardItemProperties
    {
        [SerializeField] private Sprite rewardSprite;
        [SerializeField] private int rewardCount;

        public Sprite RewardSprite => rewardSprite;
        public int RewardCount => rewardCount;
    }

    [CreateAssetMenu(menuName = "SpinSystem/RewardSystem/SpinRewards", fileName= "SpinRewards")]
    public class SpinRewards : ScriptableObject
    {
        public List<RewardItemProperties> rewardItem;
    }
}

