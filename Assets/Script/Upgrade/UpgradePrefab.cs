using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradePrefab : MonoBehaviour
{
    [SerializeField] private Upgrade upgradeData;
    [SerializeField] private int level = 0;
    [SerializeField] private int currentPrice;

    [Header("UI")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text priceText;

    public void SetUp(Upgrade data)
    {
        upgradeData = data;
        currentPrice = upgradeData.basePrice;
        UpdateUI();
    }

    public void Upgrade()
    {
        if (upgradeData != null && GameplayManager.instance.CanRemovePoint(currentPrice))
        {
            upgradeData.upgradeEvent?.Invoke();
            level++;
            UpdateAfterUpgrade();
        }
    }

    private void UpdateAfterUpgrade()
    {
        GameplayManager.instance.RemovePoint(currentPrice);
        currentPrice = (int)(currentPrice * upgradeData.priceMultiplier);
        UpdateUI();
    }

    private void UpdateUI()
    {
        nameText.text = upgradeData.upgradeName;
        levelText.text = $"Level = {level + 1}";
        priceText.text = currentPrice.ToString();
    }
}
