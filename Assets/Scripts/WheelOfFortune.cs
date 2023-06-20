using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelOfFortune : MonoBehaviour, IAnimationState
{
    [Header("Script References")]
    [SerializeField] private Transform spinner;
    [SerializeField] private Button spinButton;
    [SerializeField] private Animator wheelAnimator;
    
    [Header("Spinner Animation Properties")]
    [SerializeField] private float slowDownRate = 1f;
    [SerializeField] private float minInitialSpeed = 300f;
    [SerializeField] private float maxInitialSpeed = 500f;
    [SerializeField] private AnimationCurve spinCurve;

    private float _slowDownRatio;
    private float _spinSpeed = 0f;
    private float _elapsedTime;
    private bool _spinning = false;
    private float _angleForEachReward;

    public bool Spinning => _spinning;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        spinButton ??= GetComponentInChildren<Button>();
        wheelAnimator ??= gameObject.GetComponent<Animator>();
    }
#endif
    
    private void Awake()
    {
        spinButton.onClick.AddListener(Spin);
    }

    /// <summary>
    /// Initiates the spin of the wheel. Sets the spinning state to true, 
    /// updates the OnWheelSpinning event, deactivates the spin button and sets the initial spin speed.
    /// </summary>
    public void Spin()
    {
        _spinning = true;
        CollectedItemsPanelsManager.Instance.OnWheelSpinning(_spinning);
        SetButtonInteractable(!_spinning);
        _spinSpeed = Random.Range(minInitialSpeed, maxInitialSpeed);
        _elapsedTime = 0;
    }

    /// <summary>
    /// Called every frame. Manages the spin of the wheel, 
    /// adjusts speed and rotation, calculates rewards and initiates next zone when spin is over.
    /// </summary>
    private void Update()
    {
        if (_spinning)
        {
            _elapsedTime += Time.deltaTime;
            slowDownRate += Time.deltaTime * 0.2f;

            float targetSpeed = spinCurve.Evaluate(_elapsedTime);
            _spinSpeed = Mathf.Lerp(_spinSpeed, targetSpeed, Time.deltaTime * slowDownRate);

            if (_spinSpeed > 50)
            {
                spinner.Rotate(0, 0, -_spinSpeed * Time.deltaTime);
            }
            else
            {
                _spinning = false;
                _spinSpeed = 0;
                slowDownRate = 0.2f;

                float zRotation = spinner.eulerAngles.z;

                if (zRotation < 0)
                {
                    zRotation += 360;
                }

                zRotation += _angleForEachReward/2f;
                if (zRotation >= 360)
                {
                    zRotation -= 360;
                }

                int rewardAngle = Mathf.RoundToInt(zRotation / 45) * 45;

                if (rewardAngle == 360)
                    rewardAngle -= 360;

                float rotationTime = 0.5f;
                var eulerAngles = spinner.eulerAngles;
                spinner.DORotate(new Vector3(eulerAngles.x, eulerAngles.y, rewardAngle), rotationTime)
                    .OnComplete(() =>
                    {
                        int reward = Mathf.FloorToInt(rewardAngle / 45);
                        CollectedItemsPanelsManager.Instance.CheckCollectedItemPrefab(reward);
                        GameManager.Instance.spinPanelManager.CurrentZoneIndex++;
                        DOVirtual.DelayedCall(1.0f, () =>
                        {
                            ZonesPanelManager.Instance.ScrollContentByZone();
                        });
                    });
            }
        }
    }

    /// <summary>
    /// Triggers the specified animation on the wheel animator.
    /// </summary>
    /// <param name="animationName"></param>
    public void TriggerAnimation(string animationName)
    {
        wheelAnimator.SetTrigger(animationName);
    }
    
    /// <summary>
    /// Sets the interactability of the spin button.
    /// </summary>
    /// <param name="value"></param>
    public void SetButtonInteractable(bool value)
    {
        spinButton.interactable = value;
    }
}