using DG.Tweening;
using Spin;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortune : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private SpinManager spinManager;
    [SerializeField] private Transform spinner;
    [SerializeField] private Button spinButton;
    
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
    
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (spinButton == null)
        {
            // İlgili Button referansını otomatik olarak ata
            spinButton = GetComponentInChildren<Button>();
        }
    }
#endif
    
    private void Awake()
    {
        spinButton.onClick.AddListener(Spin);
    }

    public void Spin()
    {
        _spinning = true;
        spinButton.interactable = false;
        _spinSpeed = Random.Range(minInitialSpeed, maxInitialSpeed);
        _elapsedTime = 0;
    }

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
                spinButton.interactable = true;

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
                        spinManager.currentZoneIndex++;
                        ZonesPanelManager.Instance.ScrollContentByZone();
                    });
            }
        }
    }
}