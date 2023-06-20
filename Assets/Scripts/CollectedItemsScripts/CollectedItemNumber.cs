using DG.Tweening;
using ScriptableObjectScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CollectedItemsScripts
{
    public class CollectedItemNumber : MonoBehaviour
    {
        [Header("Item Visual References")] [SerializeField]
        private Image collectedItemIcon;

        [SerializeField] private TextMeshProUGUI collectedItemCount;

        private TextMeshProUGUI _zoneNumberText;
        private RewardItemProperties _rewardItemProperties;
        private int _siblingNumber;

        private const string ObjectRootName = "Collected_Item_Number_";

        private void OnValidate()
        {
            _siblingNumber = transform.GetSiblingIndex() + 1;

            gameObject.name = ObjectRootName + _siblingNumber;
        }

        /// <summary>
        /// Initializes the collected item with the given properties, sets its name and sibling number, 
        /// and assigns its sprite. Also starts the text animation to display the reward count.
        /// </summary>
        /// <param name="itemProperties"></param>
        public void Initialize(RewardItemProperties itemProperties)
        {
            _rewardItemProperties = itemProperties;
            _siblingNumber = transform.GetSiblingIndex() + 1;

            gameObject.name = ObjectRootName + _siblingNumber;
            collectedItemIcon.sprite = itemProperties.RewardSprite;

            AnimateText(itemProperties.RewardCount);
        }

        /// <summary>
        /// This method updates the RewardCount value and text of the RewardItemProperties component attached to the object.
        /// </summary>
        /// <param name="count"></param>
        public void UpdateCollectedItemCount(int count)
        {
            int newCount = _rewardItemProperties.RewardCount + count;
            _rewardItemProperties.RewardCount = newCount;
            AnimateText(newCount);
        }

        /// <summary>
        /// Animates the text by changing its value from the initial to the final value over one second.
        /// </summary>
        /// <param name="finalValue"></param>
        private void AnimateText(int finalValue)
        {
            int initialValue = int.Parse(collectedItemCount.text);

            DOTween.To(() => initialValue, x => initialValue = x, finalValue, 1f)
                .OnUpdate(() => collectedItemCount.text = initialValue.ToString());
        }

        /// <summary>
        /// Returns the position of the collected item icon in the game world.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPosition()
        {
            return collectedItemIcon.transform.position;
        }

        /// <summary>
        /// Returns the sprite of the collected item icon.
        /// </summary>
        /// <returns></returns>
        public Sprite GetSprite()
        {
            return collectedItemIcon.sprite;
        }
    }
}