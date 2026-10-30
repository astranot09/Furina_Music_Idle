using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    [field: SerializeField] public int Point { get; private set; }

    public event Action OnPointChange;
    public event Action OnPointAdded;

    private void Start()
    {
        Point = SaveManager.instance.Load().point;
    }
    public void AddPoint(int value)
    {
        Point += value;
        OnPointChange?.Invoke();
        OnPointAdded?.Invoke();
        SaveManager.instance.SavePoint(Point);
    }
    public void RemovePoint(int value)
    {
        Point -= value;
        OnPointChange?.Invoke();
        SaveManager.instance.SavePoint(Point);
    }
    public bool CanRemovePoint(int value)
    {
        return Point >= value;
    }
}
