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

    private void SetZoneNumber()
    {
        _zoneNumber = transform.GetSiblingIndex() + 1;
    }

    private void SetZoneNumberText()
    {
        _zoneNumberText ??= GetComponentInChildren<TextMeshProUGUI>();
        
        _zoneNumberText.SetText(_zoneNumber.ToString());

        _zoneNumberText.color = GetCardZoneNTextColor();
    }

    private void SetZoneNumberItemBackGround()
    {
        _zoneNumberItemBackGround ??= GetComponent<Image>();

        _zoneNumberItemBackGround.sprite = GetCardZoneBackGroundImage();

        _zoneNumberItemBackGround.enabled = (_zoneNumber == 1);
    }
    
    private Sprite GetCardZoneBackGroundImage()
    {
        if (_zoneNumber % 30 == 0)
        {
            return goldZone;
        }

        return _zoneNumber % 5 == 0 ? silverZone : bronzeZone;
    }
    
    private Color GetCardZoneNTextColor()
    {
        if (_zoneNumber % 30 == 0)
        {
            return _superSafeZoneTextColor;
        }

        return _zoneNumber % 5 == 0 ? _safeZoneTextColor : _standardZoneTextColor;
    }
    
    public void ChangeCardZoneNumberVisual(bool enableBackground, Color textColor)
    {
        _zoneNumberItemBackGround.enabled = enableBackground;
        _zoneNumberText.color = textColor;
    }
    
    public TextMeshProUGUI CardZoneNumberText()
    {
        return _zoneNumberText;
    }
    
}
