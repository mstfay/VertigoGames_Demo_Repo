using Spin;
using Spins;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Panel Managers References")]
    public ZonesPanelManager zonesPanelManager;
    public CollectedItemsPanelsManager collectedItemsPanelsManager;
    public SpinPanelManager spinPanelManager;
    public WheelOfFortune wheelOfFortune;
    public CollectRewardsPanelManager collectRewardsPanelManager;
    public SpinSettings spinSettings;

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

    private void OnValidate()
    {
        GetScriptReferences();
    }

    private void GetScriptReferences()
    {
        zonesPanelManager ??= FindObjectOfType<ZonesPanelManager>();

        collectedItemsPanelsManager ??= FindObjectOfType<CollectedItemsPanelsManager>();

        spinPanelManager ??= FindObjectOfType<SpinPanelManager>();
        
        wheelOfFortune ??= FindObjectOfType<WheelOfFortune>();

        collectRewardsPanelManager ??= FindObjectOfType<CollectRewardsPanelManager>();
    }
}