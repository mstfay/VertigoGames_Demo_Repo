using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardZoneNumber : MonoBehaviour
{
    private TextMeshProUGUI _zoneNumberText;
    private int _zoneNumber;
    
    
    void OnValidate()
    {
        _zoneNumber = transform.GetSiblingIndex() + 1;
        if (_zoneNumberText == null)
            _zoneNumberText = GetComponentInChildren<TextMeshProUGUI>();
        
        _zoneNumberText.SetText(_zoneNumber.ToString());

        gameObject.name = "Card_Zone_Number_" + _zoneNumber;
    }
}
