using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Upgrade
{
    public string upgradeName;
    public UnityEvent upgradeEvent;
    public int basePrice;
    public float priceMultiplier;
    public float harsherMultiplier;
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Data")]
    [SerializeField] private List<Upgrade> upgradeDatas = new();

    [Header("UI")]
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private Transform upgradeSpawner;

    [Header("Upgrade Value")]
    [field: SerializeField] public float UpgradeNormalCirclePoint { get; private set; }

    [field: SerializeField] public float UpgradeSpeed { get; private set; } = 1f;
    [field: SerializeField] public float UpgradeGoldenCircleChance { get; private set; } = 0.01f;
    [field: SerializeField] public int UpgradeCircleSpawn { get; private set; } = 1;

    [field: SerializeField] public float UpgradeOutcomeSource { get; private set; }


    private bool alreadyInit;
    public void UpgradeSetUp()
    {
        if(alreadyInit) return;
        alreadyInit = true;
        foreach (Upgrade upgrade in upgradeDatas)
        {
            GameObject x = Instantiate(upgradePrefab, upgradeSpawner);
            x.GetComponent<UpgradePrefab>().SetUp(upgrade);
        }
    }


    // ALL UPGRADE

    public void UpgradePointOnHit()
    {
        UpgradeNormalCirclePoint += 2;
    }
    public void UpgradeCircleSpeed()
    {
        UpgradeSpeed += 0.05f;
    }
    public void UpgradeGoldenCircle()
    {
        UpgradeGoldenCircleChance += 0.015f;
    }
    public void UpgradeOutline()
    {
        UpgradeCircleSpawn += 1;
    }
    public void UpgradeGlobalMultiplier()
    {
        UpgradeOutcomeSource += 0.25f;
    }
}
