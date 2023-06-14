using System;
using System.Collections.Generic;
using System.Linq;
using Spins;
using UnityEngine;
using UnityEngine.UI;

namespace Spin
{
    public class SpinManager : MonoBehaviour
    {
        [SerializeField] private Image spinBaseImage, spinIndicatorImage;
        [SerializeField] private SpinSettings spinSettings;
        [SerializeField] private List<Transform> spinRewardPoints;
        [SerializeField] private int currentZone = 1;

        private KindOfSpin _kindOfSpin;

        private void OnValidate()
        {
            SetSpinProperties();
        }
        
        private void SetSpinProperties()
        {
            SetKindOfSpin();
            SetSpinImages();
            InitializeSpinnerItems();
        }
        
        private void SetKindOfSpin()
        {
            foreach (var spinItem in spinSettings.kinOfSpin.Where(spinItem => spinItem.SpinType == GetSpinType()))
            {
                _kindOfSpin = spinItem;
            }
        }
        
        private SpinType GetSpinType()
        {
            if (currentZone % 30 == 0)
                return SpinType.GoldSpin;
            
            return currentZone % 5 == 0 ? SpinType.SilverSpin : SpinType.BronzeSpin;
        }
        
        private void SetSpinImages()
        {
            spinBaseImage.sprite = _kindOfSpin.PropertiesOfSpin.SpinRouletteSprite;
            spinIndicatorImage.sprite = _kindOfSpin.PropertiesOfSpin.SpinIndicatorSprite;
        }
        
        private void InitializeSpinnerItems()
        {
            for (var i = 0; i < spinRewardPoints.Count; i++)
            {
                var rewardItem = spinRewardPoints[i].GetComponentInChildren<RewardItem>();
                rewardItem.Initialize(_kindOfSpin.SpinRewards.rewardItem[i]);
            }
        }
    }
}