using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float scaleUp = 1.2f;
    [SerializeField] private float scaleDown = 0.85f;
    [SerializeField] private float animationDuration = 0.15f;

    private Vector2 originalScale;
    private Vector2 targetUpScale;
    private Vector2 targetDownScale;
    private bool onClick;
    private Sequence buttonSequence;

    private void Start()
    {
        originalScale = transform.localScale;
        targetUpScale = originalScale * scaleUp;
        targetDownScale = originalScale * scaleDown;
    }

    //Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        buttonSequence.Append(transform.DOScale(targetUpScale, animationDuration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        buttonSequence.Append(transform.DOScale(originalScale, animationDuration));
    }

    //Click
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        buttonSequence.Append(transform.DOScale(targetDownScale, animationDuration));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        buttonSequence.Append(transform.DOScale(targetUpScale, animationDuration));
    }


    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = originalScale;
    }
}
