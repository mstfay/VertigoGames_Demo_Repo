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
        [SerializeField] private int defaultStartZone;
        [SerializeField] private int silverZone;
        [SerializeField] private int goldZone;
        
        public int DefaultStartZone => defaultStartZone;
        public int SilverZone => silverZone;
        public int GoldZone => goldZone;
    }
    
    
    
    [CreateAssetMenu(menuName = "SpinSystem/SpinSystem/SpinSettings", fileName= "SpinSettings", order = 0)]
    public class SpinSettings : ScriptableObject
    {
        public bool isRandomOrderActive;
        public ZoneColors zoneColors;
        public SpinZones spinZones;
        public List<KindOfSpin> kinOfSpin;

    }
}


