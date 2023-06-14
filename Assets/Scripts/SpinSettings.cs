using UnityEngine;
using System;
using System.Collections.Generic;

namespace Spins
{
    [Serializable]
    public struct PropertiesOfSpin
    {
        [SerializeField] private int id;
        [SerializeField] private Sprite spinRouletteSprite;
        [SerializeField] private Sprite spinIndicatorSprite;

        public int SpinTypeId => id;
        public Sprite SpinRouletteSprite => spinRouletteSprite;
        public Sprite SpinIndicatorSprite => spinIndicatorSprite;
    }

    [Serializable]
    public struct KindOfSpin
    {
        [SerializeField] private SpinType spinType;
        [SerializeField] private SpinRewards spinRewards;
        [SerializeField] private PropertiesOfSpin propertiesOfSpin;
        
        public SpinType SpinType => spinType;
        public SpinRewards SpinRewards => spinRewards;
        public PropertiesOfSpin PropertiesOfSpin => propertiesOfSpin;
    }
    
    [CreateAssetMenu(menuName = "SpinSystem/SpinSystem/SpinSettings", fileName= "SpinSettings", order = 0)]
    public class SpinSettings : ScriptableObject
    {
        public List<KindOfSpin> kinOfSpin;

    }
}


