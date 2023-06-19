using System;
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
    [SerializeField] private SpinManager spinManager;
    [SerializeField] private CardZoneNumber cardZoneNumberPrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private List<CardZoneNumber> cardZoneNumbers = new List<CardZoneNumber>();
    [SerializeField] private int maxZoneNumber = 60;

    [Header("CardZone State Management")] 
    public CardZoneNumber previousCardZoneNumber;
    public CardZoneNumber currentCardZoneNumber;
    public CardZoneNumber comingCardZoneNumber;
    
    private Vector3 _initialContentPosition;
    
    [HideInInspector] public bool gameOver;

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

        scrollRect.enabled = false;
        _initialContentPosition = scrollRect.content.localPosition;
        DetectCardZoneNumberStates();
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
        ChangePreviousCardZoneNumberVisual();
        ChangeComingCardZoneNumberVisual();
        AnimateContentXPosition(targetPosition);
    }

    /// <summary>
    /// The 'GameOverHandler' method handles the game over state by resetting variables and invoking the 'OnGameOver' event.
    /// </summary>
    private void GameOverHandler()
    {
        previousCardZoneNumber = null;
        spinManager.currentZoneIndex = 1;
        ChangeCurrentCardZoneNumberVisual(false);
        currentCardZoneNumber = cardZoneNumbers[spinManager.currentZoneIndex - 1];
        comingCardZoneNumber = cardZoneNumbers[spinManager.currentZoneIndex];
        CollectedItemsPanelsManager.Instance.OnGameOver.Invoke();
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
    /// </summary>
    /// <param name="targetPosition"></param>
    private void AnimateContentXPosition(Vector2 targetPosition)
    {
        scrollRect.content.DOLocalMove(targetPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            spinManager.OnZonePassed.Invoke();
            ChangeCurrentCardZoneNumberVisual(true);
            gameOver = false;
            CreateNewCardZoneNumber();
        });
    }

    /// <summary>
    ///  The 'CreateNewCardZoneNumber' method creates a new 'CardZoneNumber' object if certain conditions are met.
    /// </summary>
    private void CreateNewCardZoneNumber()
    {
        if (spinManager.currentZoneIndex % spinManager.spinSettings.spinZones.SilverZone == 0 && scrollRect.content.transform.childCount < maxZoneNumber)
        {
            for (int i = 0; i < spinManager.spinSettings.spinZones.SilverZone; i++)
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

        if (spinManager.currentZoneIndex > 1)
            previousCardZoneNumber = cardZoneNumbers[spinManager.currentZoneIndex - 2];

        currentCardZoneNumber = cardZoneNumbers[spinManager.currentZoneIndex - 1];

        comingCardZoneNumber = cardZoneNumbers[spinManager.currentZoneIndex];
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
}