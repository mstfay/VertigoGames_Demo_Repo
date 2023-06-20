using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZonesPanelManager : MonoBehaviour
{
    public static ZonesPanelManager Instance { get; private set; }

    [Header("General Script References")]
    [SerializeField] private CardZoneNumber cardZoneNumberPrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TextMeshProUGUI superZoneText, safeZoneText;
    [SerializeField] private int maxZoneNumber = 60;
    [SerializeField] private List<CardZoneNumber> cardZoneNumbers = new List<CardZoneNumber>();

    [Header("CardZone State Management")] 
    public CardZoneNumber previousCardZoneNumber;
    public CardZoneNumber currentCardZoneNumber;
    public CardZoneNumber comingCardZoneNumber;
    
    [HideInInspector] public bool gameOver;
    private Vector3 _initialContentPosition;
    private int _goldZoneIndex;
    private int _silverZoneIndex;
    private SpinPanelManager _spinPanelManager;

    private void OnValidate()
    {
        cardZoneNumbers.Clear();
        cardZoneNumbers.AddRange(scrollRect.content.GetComponentsInChildren<CardZoneNumber>());
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _spinPanelManager = GameManager.Instance.spinPanelManager;
        scrollRect.enabled = false;
        _initialContentPosition = scrollRect.content.localPosition;
        Initialize();
        DetectCardZoneNumberStates();
    }

    private void Initialize()
    {
        _goldZoneIndex = GameManager.Instance.spinSettings.spinZonesIndex.GoldZoneIndex;
        _silverZoneIndex = GameManager.Instance.spinSettings.spinZonesIndex.SilverZoneIndex;

        superZoneText.text = _goldZoneIndex.ToString();
        safeZoneText.text = _silverZoneIndex.ToString();
    }

    /// <summary>
    /// The 'ScrollContentByZone' method scrolls the content of the 'scrollRect' by one 'zone'.
    /// If the game is over, it handles the game over state; otherwise, it calculates the target position of the 'scrollRect.content' and animates it to that position.
    /// </summary>
    public void ScrollContentByZone()
    {
        Vector2 targetPosition = _initialContentPosition;
        
        if (gameOver)
            GameOverHandler();
        else
            targetPosition = DetectTargetPosition();

        DetectCardZoneNumberStates();
        CheckZonesKindsPanelTexts();
        ChangePreviousCardZoneNumberVisual();
        ChangeComingCardZoneNumberVisual();
        AnimateContentXPosition(targetPosition);
    }

    /// <summary>
    /// The 'GameOverHandler' method handles the game over state by resetting variables and invoking the 'OnGameOver' event.
    /// Triggers the "MakeBigger" animation on the wheel of fortune.
    /// </summary>
    private void GameOverHandler()
    {
        previousCardZoneNumber = null;
        var defaultZoneIndex = GameManager.Instance.spinSettings.spinZonesIndex.DefaultStartZoneIndex;
        _spinPanelManager.CurrentZoneIndex = defaultZoneIndex;
        ChangeCurrentCardZoneNumberVisual(false);
        currentCardZoneNumber = cardZoneNumbers[_spinPanelManager.CurrentZoneIndex - 1];
        comingCardZoneNumber = cardZoneNumbers[_spinPanelManager.CurrentZoneIndex];
        Initialize();
        CollectedItemsPanelsManager.Instance.OnGameOver.Invoke();
        GameManager.Instance.wheelOfFortune.TriggerAnimation("MakeBigger");
    }

    /// <summary>
    /// // The 'DetectTargetPosition' method calculates and returns the target position of the 'scrollRect.content' based on the width of the 'cardZoneNumberPrefab'.
    /// </summary>
    /// <returns></returns>
    private Vector2 DetectTargetPosition()
    {
        var contentItemWidth = (RectTransform)cardZoneNumberPrefab.gameObject.transform;
        var localPosition = scrollRect.content.localPosition;
        var targetX = localPosition.x - contentItemWidth.rect.width;
        var targetPosition = new Vector2(targetX, localPosition.y);
        
        return targetPosition;
    }

    /// <summary>
    /// The 'AnimateContentXPosition' method animates the 'scrollRect.content' to the specified 'targetPosition'.
    /// When the animation is completed, it invokes the 'OnZonePassed' event, resets the 'gameOver' variable, and creates a new 'CardZoneNumber' object.
    /// Triggers the "MakeBigger" animation on the wheel of fortune, and sets the interactability of the spin button depending on whether the wheel is spinning or not.
    /// </summary>
    /// <param name="targetPosition"></param>
    private void AnimateContentXPosition(Vector2 targetPosition)
    {
        scrollRect.content.DOLocalMove(targetPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            CollectedItemsPanelsManager.Instance.OnWheelSpinning.Invoke(GameManager.Instance.wheelOfFortune.Spinning);
            _spinPanelManager.OnZonePassed.Invoke();
            ChangeCurrentCardZoneNumberVisual(true);
            gameOver = false;
            CreateNewCardZoneNumber();
            
            GameManager.Instance.wheelOfFortune.TriggerAnimation("MakeBigger");
            GameManager.Instance.wheelOfFortune.SetButtonInteractable(!GameManager.Instance.wheelOfFortune.Spinning);
        });
    }

    /// <summary>
    ///  The 'CreateNewCardZoneNumber' method creates a new 'CardZoneNumber' object if certain conditions are met.
    /// </summary>
    private void CreateNewCardZoneNumber()
    {
        if (_spinPanelManager.CurrentZoneIndex % GameManager.Instance.spinSettings.spinZonesIndex.SilverZoneIndex == 0 && scrollRect.content.transform.childCount < maxZoneNumber)
        {
            for (int i = 0; i < GameManager.Instance.spinSettings.spinZonesIndex.SilverZoneIndex; i++)
            {
                var createdCardZoneNumber = Instantiate(cardZoneNumberPrefab, scrollRect.content.transform);
                cardZoneNumbers.Add(createdCardZoneNumber);
                createdCardZoneNumber.Initialize();
            }
        }
    }

    /// <summary>
    /// The 'DetectCardZoneNumberStates' method determines the current states of the 'previousCardZoneNumber', 'currentCardZoneNumber', and 'comingCardZoneNumber' objects.
    /// </summary>
    private void DetectCardZoneNumberStates()
    {
        previousCardZoneNumber = null;

        if (_spinPanelManager.CurrentZoneIndex > 1)
            previousCardZoneNumber = cardZoneNumbers[_spinPanelManager.CurrentZoneIndex - 2];

        currentCardZoneNumber = cardZoneNumbers[_spinPanelManager.CurrentZoneIndex - 1];

        comingCardZoneNumber = cardZoneNumbers[_spinPanelManager.CurrentZoneIndex];
    }

    /// <summary>
    /// The 'ChangePreviousCardZoneNumberVisual' method changes the visual state of the 'previousCardZoneNumber' object.
    /// </summary>
   private void ChangePreviousCardZoneNumberVisual()
   {
       if(previousCardZoneNumber is null) 
           return;
       Color textColor = previousCardZoneNumber.CardZoneNumberText().color;
       textColor = new Color(textColor.r, textColor.g, textColor.b, 0.25f);
       previousCardZoneNumber.ChangeCardZoneNumberVisual(false, textColor);
   }
   
    /// <summary>
    /// // The 'ChangeCurrentCardZoneNumberVisual' method changes the visual state of the 'currentCardZoneNumber' object.
    /// </summary>
    /// <param name="value"></param>
   private void ChangeCurrentCardZoneNumberVisual(bool value)
   {
       currentCardZoneNumber.ChangeCardZoneNumberVisual(value, currentCardZoneNumber.CardZoneNumberText().color);
   }
   
    /// <summary>
    /// // The 'ChangeComingCardZoneNumberVisual' method changes the visual state of the 'comingCardZoneNumber' object.
    /// </summary>
   private void ChangeComingCardZoneNumberVisual()
   {
       comingCardZoneNumber.ChangeCardZoneNumberVisual(false, comingCardZoneNumber.CardZoneNumberText().color);
   }

    /// <summary>
    /// Checks the kind of the current zone and updates the relevant UI text depending on whether the zone is a super or safe zone.
    /// </summary>
    private void CheckZonesKindsPanelTexts()
    {
        if (currentCardZoneNumber.ZoneNumber % _goldZoneIndex is 0)
        {
            superZoneText.text = (currentCardZoneNumber.ZoneNumber + _goldZoneIndex).ToString();
            return;
        }

        if (currentCardZoneNumber.ZoneNumber % _silverZoneIndex is not 0) return;
        safeZoneText.text = (currentCardZoneNumber.ZoneNumber + _silverZoneIndex).ToString();
    }
}