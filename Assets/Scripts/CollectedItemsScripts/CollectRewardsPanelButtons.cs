using GameEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CollectedItemsScripts
{
    public class CollectRewardsPanelButtons : MonoBehaviour
    {
        [Header("Button Type Sprites")] [SerializeField]
        private Sprite collectButtonSprite;

        [SerializeField] private Sprite backButtonSprite;

        private Image _buttonImage;
        private TextMeshProUGUI _buttonText;

        public ButtonTypes ButtonTypes => GetButtonType();

        public void OnValidate()
        {
            SetButtonObjectName();
            GetButtonComponents();
            SetButtonProperties();
        }

        /// <summary>
        /// Sets the game object's name based on the type of button it is.
        /// </summary>
        private void SetButtonObjectName()
        {
            gameObject.name = "Collect_Rewards_Panel_Scroll_Content_Button_" + GetButtonType();
        }

        /// <summary>
        /// Returns the type of button based on its sibling index.
        /// </summary>
        /// <returns></returns>
        public Button GetButton()
        {
            return GetComponent<Button>();
        }

        /// <summary>
        /// Returns the type of button based on its sibling index.
        /// </summary>
        /// <returns></returns>
        private ButtonTypes GetButtonType()
        {
            return (ButtonTypes)GetSiblingIndex();
        }

        /// <summary>
        /// Returns the sibling index of the transform.
        /// </summary>
        /// <returns></returns>
        private int GetSiblingIndex()
        {
            return transform.GetSiblingIndex();
        }

        /// <summary>
        /// Retrieves the required button components if they are not already assigned.
        /// </summary>
        private void GetButtonComponents()
        {
            _buttonImage ??= GetComponent<Image>();

            _buttonText ??= GetComponentInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        /// Sets the properties of the button based on the type of button it is.
        /// </summary>
        private void SetButtonProperties()
        {
            if (GetButtonType() == ButtonTypes.Collect)
            {
                _buttonImage.sprite = collectButtonSprite;
                _buttonImage.color = Color.green;
                ;
                _buttonText.text = "Collect Rewards";
            }
            else
            {
                _buttonImage.sprite = backButtonSprite;
                _buttonImage.color = Color.white;
                ;
                _buttonText.text = "Go Back";
            }
        }
    }
}