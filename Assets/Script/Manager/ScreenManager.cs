using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScreenManager : MonoBehaviour
{
    [Header("Windowed")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite checkSprite;
    [SerializeField] private Sprite crossSprite;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private TMP_Text confirmTimer;
    [SerializeField] private float maxTimer = 15f;
    private float currTimer = 15f;
    private bool isWindowed = false;
    private bool isConfirmation = false;

    private void Start()
    {
        isWindowed = !Screen.fullScreen;
        UpdateIconUI();
    }
    public void ChangeScreen()
    {
        UIManager.instance.OpenPanel(confirmationPanel);
        UIManager.instance.OnPanelCloseEvent += CancelSceneChange;
        ToggleWindowed();
        isConfirmation = true;
        currTimer = maxTimer;
    }
    private void Update()
    {
        if (!isConfirmation) return;

        if(currTimer > 0)
        {
            currTimer -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            currTimer = 0;
            UpdateUI();
            CancelSceneChange();
        }
    }
    
    public void UpdateUI()
    {
        confirmTimer.text = $"{currTimer:F1}";
    }

    public void ToggleWindowed()
    {
        isWindowed = !isWindowed;
        Screen.fullScreen = !isWindowed;
        isConfirmation = false;
        if (isWindowed)
        {
            iconImage.sprite = checkSprite;
        }
        else
        {
            iconImage.sprite = crossSprite;
        }
        UpdateIconUI();
    }
    private void UpdateIconUI()
    {
        if (iconImage != null)
        {
            iconImage.sprite = isWindowed ? checkSprite : crossSprite;
        }
    }
    public void ConfirmSceneChange()
    {
        isConfirmation = false;
        UIManager.instance.ClosePanel(confirmationPanel);
        UIManager.instance.OnPanelCloseEvent -= CancelSceneChange;
    }
    public void CancelSceneChange()
    {
        isConfirmation = false;
        UIManager.instance.ClosePanel(confirmationPanel);
        UIManager.instance.OnPanelCloseEvent -= CancelSceneChange;
        ToggleWindowed();
    }
}
