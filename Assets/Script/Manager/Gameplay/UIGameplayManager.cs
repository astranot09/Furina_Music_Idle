using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UIGameplayManager : MonoBehaviour
{
    [Header("Point")]
    [SerializeField] private TMP_Text pointText;
    private Vector2 originalPointScale;
    [Header("Shop")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject buttonShop;

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject buttonSetting;

    private void OnEnable()
    {
        GameplayManager.instance.OnPointChange += UpdatePointUI;
        GameplayManager.instance.OnPointAdded += PointAddedAnimation;
    }

    private void OnDisable()
    {
        GameplayManager.instance.OnPointChange -= UpdatePointUI;
        GameplayManager.instance.OnPointAdded -= PointAddedAnimation;
    }
    private void Start()
    {
        originalPointScale = pointText.transform.localScale;
        UpdatePointUI();
    }
    private void UpdatePointUI()
    {
        pointText.text = GameplayManager.instance.Point.ToString();
    }

    private void PointAddedAnimation()
    {
        pointText.transform.DOComplete(true);
        pointText.transform.localScale = originalPointScale;
        pointText.transform.localScale = pointText.transform.localScale;
        pointText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0f), 0.15f, 1, 0.1f);
    }
    public void OpenShop()
    {
        UIManager.instance.ChangeAllPanel(shopPanel);
        UIManager.instance.OnPanelCloseEvent += CloseShop;
        UpgradeManager.instance.UpgradeSetUp();
        buttonShop.SetActive(!shopPanel.activeSelf);
    }
    public void CloseShop()
    {
        UIManager.instance.ClosePanel(shopPanel);
        buttonShop.SetActive(!shopPanel.activeSelf);
        UIManager.instance.OnPanelCloseEvent -= CloseShop;
    }
    public void OpenSetting()
    {
        UIManager.instance.ChangeAllPanel(settingPanel);
        UIManager.instance.OnPanelCloseEvent += CloseSetting;
        buttonSetting.SetActive(!settingPanel.activeSelf);
    }
    public void CloseSetting()
    {
        UIManager.instance.ClosePanel(settingPanel);
        buttonSetting.SetActive(!settingPanel.activeSelf);
        UIManager.instance.OnPanelCloseEvent -= CloseSetting;
    }

}
