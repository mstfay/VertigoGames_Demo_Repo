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
        [SerializeField] private int silverZone = 5, goldZone = 30, currentZone = 1;

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
        
        /// <summary>
        /// We are assigning the KindOfSpin value returned by GetSpinType to our _kindOfSpin variable. Thus, we will be able to use the spin properties we need.
        /// </summary>
        private void SetKindOfSpin()
        {
            foreach (var spinItem in spinSettings.kinOfSpin.Where(spinItem => spinItem.SpinType == GetSpinType()))
            {
                _kindOfSpin = spinItem;
            }
        }
        
        /// <summary>
        /// We are taking the modulus of the current zone value based on whether it's a silver zone or a gold zone and returning the SpinType value.
        /// </summary>
        /// <returns></returns>
        private SpinType GetSpinType()
        {
            if (currentZone % goldZone == 0)
                return SpinType.GoldSpin;
            
            return currentZone % silverZone == 0 ? SpinType.SilverSpin : SpinType.BronzeSpin;
        }
        
        /// <summary>
        /// With the SetKindOfSpin() method, we're assigning the spin properties that we've set to the variable to the visuals associated with the spinner.
        /// </summary>
        private void SetSpinImages()
        {
            spinBaseImage.sprite = _kindOfSpin.PropertiesOfSpin.SpinRouletteSprite;
            spinIndicatorImage.sprite = _kindOfSpin.PropertiesOfSpin.SpinIndicatorSprite;
        }
        
        /// <summary>
        /// We're accessing the RewardItem component in the child object of the specified points on the spinner. Here, we are pulling the visuals that should be on the spinner from the scriptable object.
        /// </summary>
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