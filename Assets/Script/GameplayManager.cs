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
    public void AddPoint(int value)
    {
        Point += value;
        OnPointChange?.Invoke();
        OnPointAdded?.Invoke();
    }
    public void RemovePoint(int value)
    {
        Point -= value;
        OnPointChange?.Invoke();
    }
    public bool CanRemovePoint(int value)
    {
        return Point >= value;
    }
}
