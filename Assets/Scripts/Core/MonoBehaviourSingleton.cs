using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    [SerializeField] private bool dontDestroyOnLoad = true;

    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            OnAwaken();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            OnDestroyed();
        }
    }

    protected virtual void OnAwaken() { }

    protected virtual void OnDestroyed() { }
}
