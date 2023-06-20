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
    public struct ZoneColors
    {
        [SerializeField] private Color standardZoneColor;
        [SerializeField] private Color silverZoneColor;
        [SerializeField] private Color goldZoneColor;
        
        public Color StandardZoneColor => standardZoneColor;
        public Color SilverZoneColor => standardZoneColor;
        public Color GoldZoneColor => standardZoneColor;
    }

    [Serializable]
    public struct KindOfSpin
    {
        [SerializeField] private SpinTypes spinType;
        [SerializeField] private SpinRewards spinRewards;
        [SerializeField] private PropertiesOfSpin propertiesOfSpin;
        
        public SpinTypes SpinType => spinType;
        public SpinRewards SpinRewards => spinRewards;
        public PropertiesOfSpin PropertiesOfSpin => propertiesOfSpin;
    }
    
    [Serializable]
    public struct SpinZones
    {
        [SerializeField] private int defaultStartZoneIndex;
        [SerializeField] private int silverZoneIndex;
        [SerializeField] private int goldZoneIndex;
        
        public int DefaultStartZoneIndex => defaultStartZoneIndex;
        public int SilverZoneIndex => silverZoneIndex;
        public int GoldZoneIndex => goldZoneIndex;
    }
    
    
    
    [CreateAssetMenu(menuName = "SpinSystem/SpinSystem/SpinSettings", fileName= "SpinSettings", order = 0)]
    public class SpinSettings : ScriptableObject
    {
        public bool isRandomOrderActive;
        public SpinZones spinZonesIndex;
        public List<KindOfSpin> kindOfSpin;

    }
}


