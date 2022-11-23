using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance = null;

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
