using UnityEngine;
using DG.Tweening;
public class CirclePrefabScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer circleSpriteRenderer;

    private int point;
    private Transform circleTransform;
    private float duration = 2;
    private AudioClip clip;

    private Vector2 startScale = new Vector2(1.2f,1.2f);
    private Vector2 endScale = new Vector2(0.5f, 0.5f);
    private bool isGolden;
    private void Start()
    {
        circleTransform = GetComponent<Transform>();
        CircleGoSmall();
    }

    public void CircleSetUp(float dur, Sprite sprite, int poin, AudioClip sfx, bool gold)
    {
        duration = dur;
        circleSpriteRenderer.sprite = sprite;
        point = poin;
        clip = sfx;
        isGolden = gold;
    }

    public void CircleGoSmall()
    {
        circleTransform.DOScale(endScale, duration).SetUpdate(true).OnComplete(() =>
        {
            CircleFinish();
            Destroy(gameObject);
        });
    }

    private void CircleFinish()
    {
        AudioManager.instance.PlaySFX(clip);
        GameplayManager.instance.AddPoint(point);
        CircleManager.instance.TriggerBaseCircleAnimation(isGolden);
    }

}
