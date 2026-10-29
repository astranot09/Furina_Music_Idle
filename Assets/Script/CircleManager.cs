using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class CircleManager : MonoBehaviour
{
    public static CircleManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [Header("BaseCircle")]
    [SerializeField] private Transform baseCircleTransform;
    [SerializeField] private float targetScale = 1.05f;
    [SerializeField] private float animationDuration = 0.15f;
    private Vector2 originalBaseCircleScale;
    private Vector2 targetBaseCircleScale;

    [Header("BaseCircle -- Cycling")]
    [SerializeField] private Transform baseCircleVisual;
    [SerializeField] private float cyclingDuration = 8f;

    [Header("BaseCircle -- VFX")]
    [SerializeField] private ParticleSystem normalParticleSystem;
    [SerializeField] private ParticleSystem goldenParticleSystem;

    [Header("NormalCircle")]
    [SerializeField] private int baseNormalCirclePoint = 2;

    [Header("GoldenCircle")]
    [SerializeField] private float baseGoldenCircleChance = 1;
    [SerializeField] private int minMultiplier = 5;
    [SerializeField] private int maxMultiplier = 10;

    [Header("Sprite")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite goldenSprite;

    [Header("Setting")]
    [SerializeField] private float duration = 2f;
    [SerializeField] private float circleDelay = 2f;

    [Header("Prefab")]
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private Transform circleSpawner;

    private void Start()
    {
        originalBaseCircleScale = baseCircleTransform.localScale;
        targetBaseCircleScale = originalBaseCircleScale * targetScale;
        BaseCircleAnimation();
        StartCoroutine(GameCoroutine());
    }

    IEnumerator GameCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(circleDelay);
            StartCoroutine(SpawnCircle());
        }
    }

    private IEnumerator SpawnCircle()
    {
        for (int i = 0; i < (1 + UpgradeManager.instance.UpgradeCircleSpawn); i++)
        {
            float dur = CalculationSpeedToDuration();
            int point;
            Sprite sprite;
            AudioClip clip;
            bool gold;
            if (GoldenChance())
            {
                point = (((int)(baseNormalCirclePoint + UpgradeManager.instance.UpgradeNormalCirclePoint)) * (int)Random.Range(minMultiplier,maxMultiplier+1));
                sprite = goldenSprite;
                clip = AudioManager.instance.goldenSFX;
                gold = true;
            }
            else
            {
                point = ((int)(baseNormalCirclePoint + UpgradeManager.instance.UpgradeNormalCirclePoint));
                sprite = normalSprite;
                clip = AudioManager.instance.normalSFX;
                gold = false;
            }

            point += (int)(point * UpgradeManager.instance.UpgradeOutcomeSource);

            GameObject circle = Instantiate(circlePrefab, circleSpawner);
            circle.GetComponent<CirclePrefabScript>().CircleSetUp(dur, sprite, point, clip, gold);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private float CalculationSpeedToDuration()
    {
        float x = duration/UpgradeManager.instance.UpgradeSpeed;
        return x;
    }
    private bool GoldenChance()
    {
        float goldenChance = baseGoldenCircleChance + UpgradeManager.instance.UpgradeGoldenCircleChance;
        return Random.value <= goldenChance;
    }

    public void BaseCircleAnimation()
    {
        baseCircleVisual.DOKill();

        baseCircleVisual.localRotation = Quaternion.identity;

        baseCircleVisual.DORotate(new Vector3(0, 0, -360f), cyclingDuration, RotateMode.FastBeyond360)
                           .SetEase(Ease.Linear) // Wajib agar kecepatan rotasi konstan & tidak patah-patah
                           .SetLoops(-1, LoopType.Incremental) // Berputar terus menerus menambah derajat
                           .SetUpdate(true);
    }
    public void TriggerBaseCircleAnimation(bool isGold)
    {
        baseCircleTransform.DOKill();
        baseCircleTransform.DOPunchScale(targetBaseCircleScale, animationDuration, 1, 0.1f);
        if (isGold)
            goldenParticleSystem.Play();
        else
            normalParticleSystem.Play();
    }
}
