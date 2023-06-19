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
        public SpinSettings spinSettings;
        public int currentZoneIndex;
        [SerializeField] private Image spinBaseImage, spinIndicatorImage;
        public List<Transform> spinRewardPoints;

        public List<RewardItemProperties> rewardItems;

        private KindOfSpin _kindOfSpin;

        public Action OnZonePassed;

        private void OnEnable()
        {
            OnZonePassed += SetSpinProperties;
        }

        private void OnDisable()
        {
            OnZonePassed -= SetSpinProperties;
        }

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
        private SpinTypes GetSpinType()
        {
            if (currentZoneIndex % spinSettings.spinZones.GoldZone == 0)
                return SpinTypes.GoldSpin;
            
            return currentZoneIndex % spinSettings.spinZones.SilverZone == 0 ? SpinTypes.SilverSpin : SpinTypes.BronzeSpin;
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
            // Random order
            if (spinSettings.isRandomOrderActive)
            {
                var rnd = new System.Random();
                rewardItems = _kindOfSpin.SpinRewards.rewardItem.OrderBy(x => rnd.Next()).ToList();
            }
            
            for (var i = 0; i < spinRewardPoints.Count; i++)
            {
                var rewardItem = spinRewardPoints[i].GetComponentInChildren<RewardItem>();
                rewardItem.Initialize(rewardItems[i]);
            }
        }
    }
}