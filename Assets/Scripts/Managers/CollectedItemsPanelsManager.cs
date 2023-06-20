using System;
using System.Collections.Generic;
using System.Linq;
using CollectedItemsScripts;
using DG.Tweening;
using GameEnums;
using ScriptableObjectScripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class CollectedItemsPanelsManager : MonoBehaviour
    {
        public static CollectedItemsPanelsManager Instance { get; private set; }

        [Header("General Script Properties")]
        //[SerializeField] private SpinPanelManager spinManager;
        [SerializeField]
        private GameObject collectRewardsPanelManager;

        [SerializeField] private CollectedItemNumber collectedItemNumberPrefab;
        [SerializeField] private Transform collectedItemsPanelContent;
        [SerializeField] private Transform rewardsParent;
        [SerializeField] private GameObject willCreateObjectPrefab;
        [SerializeField] private Button exitButton;

        [Header("Collected Item Animation Properties")] [SerializeField]
        private int numberOfRewardCopy = 5;

        [SerializeField] private float circleRadiusForCollectedItem = 100f;
        [SerializeField] private float collectedItemAnimationDuration = .5f;

        private List<KeyValuePair<RewardTypes, CollectedItemNumber>> _collectedItems;

        public Action OnGameOver;
        public Action<bool> OnWheelSpinning;
        private SpinPanelManager _spinManager;

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
            _spinManager = GameManager.Instance.spinPanelManager;
            _collectedItems = new List<KeyValuePair<RewardTypes, CollectedItemNumber>>();
            exitButton.onClick.AddListener(ExitGameAndCollectRewards);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            exitButton ??= GetComponentInChildren<Button>();
        }
#endif

        /// <summary>
        /// Registers the 'ClearAllCollectedRewards' method to the 'OnGameOver' event when the object becomes enabled.
        /// </summary>
        private void OnEnable()
        {
            OnGameOver += ClearAllCollectedRewards;
            OnWheelSpinning += WheelSpinning;
        }

        /// <summary>
        /// Deregisters the 'ClearAllCollectedRewards' method from the 'OnGameOver' event when the object becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            OnGameOver -= ClearAllCollectedRewards;
        }

        /// <summary>
        /// Checks and manages the rewarded item, instantiates it on the screen, and animates it.
        /// </summary>
        /// <param name="reward"></param>
        public void CheckCollectedItemPrefab(int reward)
        {
            // This function starts by getting the properties of the reward item. 
            var collectedItemProperties = _spinManager.rewardItems[reward];

            // If the reward is of "Death" type, the game ends.
            if (collectedItemProperties.RewardType == RewardTypes.Death)
            {
                ZonesPanelManager.Instance.gameOver = true;
                return;
            }

            // If a collected item of the same type already exists, it updates the item count and initiates its animation.
            foreach (var item in _collectedItems.Where(item => item.Key == collectedItemProperties.RewardType))
            {
                UpdateAndAnimateExistingItem(item, collectedItemProperties, reward);
                return;
            }

            // If the item doesn't exist, it creates a new instance of the item, adds it to the collected items list and initiates its animation.
            InstantiateNewItem(collectedItemProperties, reward);
        }

        /// <summary>
        /// Updates the count of an existing collected item and initiates its animation.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="collectedItemProperties"></param>
        /// <param name="reward"></param>
        private void UpdateAndAnimateExistingItem(KeyValuePair<RewardTypes, CollectedItemNumber> item,
            RewardItemProperties collectedItemProperties, int reward)
        {
            item.Value.UpdateCollectedItemCount(collectedItemProperties.RewardCount);
            AnimateItem(item.Value, reward);
        }

        /// <summary>
        /// Instantiates a new collected item and initiates its animation.
        /// </summary>
        /// <param name="collectedItemProperties"></param>
        /// <param name="reward"></param>
        private void InstantiateNewItem(RewardItemProperties collectedItemProperties, int reward)
        {
            var createdCollectedItem = Instantiate(collectedItemNumberPrefab, collectedItemsPanelContent);
            createdCollectedItem.Initialize(collectedItemProperties);
            _collectedItems.Add(
                new KeyValuePair<RewardTypes, CollectedItemNumber>(collectedItemProperties.RewardType,
                    createdCollectedItem));
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)collectedItemsPanelContent.transform);
            AnimateItem(createdCollectedItem, reward);
        }

        /// <summary>
        /// Animates the specified item, causing it to move in a specific way on the screen.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="reward"></param>
        private void AnimateItem(CollectedItemNumber item, int reward)
        {
            CreateAndMoveSprites(item.GetSprite(), _spinManager.spinRewardPoints[reward],
                item.GetPosition(), numberOfRewardCopy, circleRadiusForCollectedItem, collectedItemAnimationDuration);
        }

        /// <summary>
        /// Clears all collected rewards by destroying the game objects and then clearing the list.
        /// Triggers the "MakeBigger" animation on the wheel of fortune.
        /// TO DO: Object pooling for this method.
        /// </summary>
        private void CreateAndMoveSprites(Sprite sprite, Transform start, Vector3 end, int numberOfCopies, float radius,
            float duration)
        {
            for (int i = 0; i < numberOfCopies; i++)
            {
                // Instantiate the parent GameObject, not the Image component directly
                GameObject obj = Instantiate(willCreateObjectPrefab, rewardsParent);

                // Then get its Image component
                Image image = obj.GetComponent<Image>();
                if (image)
                {
                    image.sprite = sprite;
                }

                // Generate a random direction for the offset
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector3 offset = new Vector3(randomDirection.x, randomDirection.y, 0) * radius;

                // Create a sequence for the two move operations
                Sequence mySequence = DOTween.Sequence();

                // First, move the object to its offset position relative to the start
                mySequence.Append(obj.transform.DOMove(start.position + offset, duration).SetEase(Ease.InOutQuad));

                // Then, move the object to the final position
                mySequence.Append(obj.transform.DOMove(end, duration).SetEase(Ease.InOutQuad));

                // Destroy the object when the sequence is complete
                mySequence.OnComplete(() =>
                {
                    Destroy(obj);
                    GameManager.Instance.wheelOfFortune.TriggerAnimation("MakeBigger");
                });
            }
        }

        /// <summary>
        /// Clears all collected rewards by destroying the game objects and then clearing the list.
        /// </summary>
        private void ClearAllCollectedRewards()
        {
            foreach (Transform child in collectedItemsPanelContent)
            {
                Destroy(child.gameObject);
            }

            _collectedItems.Clear();
        }

        /// <summary>
        /// Make false the exit button state.
        /// </summary>
        private void ExitGameAndCollectRewards()
        {
            collectRewardsPanelManager.SetActive(true);
        }

        /// <summary>
        /// The exit button interactable is enabled or disabled based on the state of the spinner.
        /// </summary>
        /// <param name="value"></param>
        private void WheelSpinning(bool value)
        {
            exitButton.interactable = !value;
        }
    }
}