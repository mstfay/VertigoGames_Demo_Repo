using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;


    [SerializeField] private int currentZone;

    public int CurrentZone
    {
        get => currentZone;
        set => currentZone = value;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
