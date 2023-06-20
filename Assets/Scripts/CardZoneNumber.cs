using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardZoneNumber : MonoBehaviour
{
    [SerializeField] private Sprite bronzeZone;
    [SerializeField] private Sprite silverZone;
    [SerializeField] private Sprite goldZone;
    private TextMeshProUGUI _zoneNumberText;
    private Image _zoneNumberItemBackGround;
    private int _zoneNumber;

    private readonly Color _standardZoneTextColor = Color.white;
    private readonly Color _safeZoneTextColor = Color.green;
    private readonly Color _superSafeZoneTextColor = Color.yellow;

    private void Start()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetZoneNumber();
        SetZoneNumberText();
        SetZoneNumberItemBackGround();
        gameObject.name = "Card_Zone_Number_" + _zoneNumber;
    }

    /// <summary>
    /// This method sets the zone number to the index of the sibling objects + 1.
    /// </summary>
    private void SetZoneNumber()
    {
        _zoneNumber = transform.GetSiblingIndex() + 1;
    }

    /// <summary>
    /// This method sets the zone number's text and color.
    /// </summary>
    private void SetZoneNumberText()
    {
        _zoneNumberText ??= GetComponentInChildren<TextMeshProUGUI>();
        
        _zoneNumberText.SetText(_zoneNumber.ToString());

        _zoneNumberText.color = GetCardZoneNTextColor();
    }

    /// <summary>
    /// This method sets the zone number item's background sprite and enabled status.
    /// </summary>
    private void SetZoneNumberItemBackGround()
    {
        _zoneNumberItemBackGround ??= GetComponent<Image>();

        _zoneNumberItemBackGround.sprite = GetCardZoneBackGroundImage();

        _zoneNumberItemBackGround.enabled = (_zoneNumber == 1);
    }
    
    /// <summary>
    /// This method returns the correct background sprite based on the zone number.
    /// </summary>
    /// <returns></returns>
    private Sprite GetCardZoneBackGroundImage()
    {
        if (_zoneNumber % 30 == 0)
        {
            return goldZone;
        }

        return _zoneNumber % 5 == 0 ? silverZone : bronzeZone;
    }
    
    /// <summary>
    /// This method returns the correct text color based on the zone number.
    /// </summary>
    /// <returns></returns>
    private Color GetCardZoneNTextColor()
    {
        if (_zoneNumber % 30 == 0)
        {
            return _superSafeZoneTextColor;
        }

        return _zoneNumber % 5 == 0 ? _safeZoneTextColor : _standardZoneTextColor;
    }
    
    /// <summary>
    /// This method changes the visuals of the CardZoneNumber based on the given parameters.
    /// </summary>
    /// <param name="enableBackground"></param>
    /// <param name="textColor"></param>
    public void ChangeCardZoneNumberVisual(bool enableBackground, Color textColor)
    {
        _zoneNumberItemBackGround.enabled = enableBackground;
        _zoneNumberText.color = textColor;
    }
    
    /// <summary>
    /// This method returns the TextMeshProUGUI component of the CardZoneNumber.
    /// </summary>
    /// <returns></returns>
    public TextMeshProUGUI CardZoneNumberText()
    {
        return _zoneNumberText;
    }
    
}
