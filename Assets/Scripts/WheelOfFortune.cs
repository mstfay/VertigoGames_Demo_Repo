using System;
using DG.Tweening;
using Spin;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class WheelOfFortune : MonoBehaviour
{
    public float spinSpeed = 0f;
    public float elapsedTime;
    public bool spinning = false;
    public AnimationCurve spinCurve;
    [SerializeField] private Button spinButton;
    [SerializeField] private Transform spinner;
    public float slowDownRate = 1f;

    private const int NumberOfRewards = 8;

    public float minInitialSpeed = 300f;
    public float maxInitialSpeed = 500f;

    public void Spin()
    {
        spinning = true;
        spinButton.interactable = false;
        spinSpeed = UnityEngine.Random.Range(minInitialSpeed, maxInitialSpeed);
        Debug.Log("spinSpeed: " + spinSpeed);
        elapsedTime = 0;
    }

    void Update()
    {
        if (spinning)
        {
            elapsedTime += Time.deltaTime;
            slowDownRate += Time.deltaTime * 0.2f;

            float targetSpeed = spinCurve.Evaluate(elapsedTime);
            spinSpeed = Mathf.Lerp(spinSpeed, targetSpeed, Time.deltaTime * slowDownRate);

            if (spinSpeed > 50)
            {
                spinner.Rotate(0, 0, -spinSpeed * Time.deltaTime);
            }
            else
            {
                spinning = false;
                spinSpeed = 0;
                slowDownRate = 0.2f;
                spinButton.interactable = true;

                float zRotation = spinner.eulerAngles.z;

                if (zRotation < 0)
                {
                    zRotation += 360;
                }

                zRotation += 22.5f;
                if (zRotation >= 360)
                {
                    zRotation -= 360;
                }

                int rewardAngle = Mathf.RoundToInt(zRotation / 45) * 45;

                float rotationTime = 0.5f;
                spinner.DORotate(new Vector3(spinner.eulerAngles.x, spinner.eulerAngles.y, rewardAngle), rotationTime)
                    .OnComplete(() =>
                    {
                        Debug.Log("spinner angle: " + spinner.eulerAngles);

                        int reward = Mathf.FloorToInt(rewardAngle / 45);
                        Debug.Log("reward: " + reward);
                        CollectedItemsPanelsManager.Instance.CreateCollectedItemPrefab(reward);
                    });
            }
        }
    }


    
}