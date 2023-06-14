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
        [SerializeField] private RewardItem rewardItemPrefab;
        [SerializeField] private SpinSettings spinSettings;
        [SerializeField] private List<Transform> spinRewardPoints;

        private KindOfSpin _kindOfSpin;
        private void Awake()
        {
            SetSpinImages();
            SpawnItems();
        }
        
        private void SpawnItems()
        {
            
            for (int i = 0; i < spinRewardPoints.Count; i++)
            {
                RewardItem createdRewardItem = Instantiate(rewardItemPrefab, spinRewardPoints[i], false);
                createdRewardItem.Initialize(_kindOfSpin.SpinRewards.rewardItem[i]);
            }
        }

        private void SetSpinImages()
        {
            SetKindOfSpin();
            spinBaseImage.sprite = _kindOfSpin.PropertiesOfSpin.SpinRouletteSprite;
            spinIndicatorImage.sprite = _kindOfSpin.PropertiesOfSpin.SpinIndicatorSprite;
        }
        
        private void SetKindOfSpin()
        {
            foreach (var spinItem in spinSettings.kinOfSpin)
            {
                if (spinItem.SpinType == GetSpinType())
                {
                    _kindOfSpin = spinItem;
                }
            }
        }

        private SpinType GetSpinType()
        {
            var currentZone = GameManager.Instance.CurrentZone;
            var spinType = SpinType.BronzeSpin;
            
            if (currentZone % 30 == 0)
            {
                spinType = SpinType.GoldSpin;
            }
            else if (currentZone % 5 == 0)
            {
                spinType = SpinType.SilverSpin;
            }

            return spinType;
        }
    }
}

