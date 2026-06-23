using UnityEngine;

public class PoolableParticle : MonoBehaviour, IPoolable
{
    //[FormerlySerializedAs("particles")] 
    [SerializeField] private ParticleSystem _particleSystem;
    public bool IsActive => gameObject.activeSelf;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        _particleSystem.Play();
    }

    public void Deactivate()
    {
        _particleSystem.Stop();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(IsActive && !_particleSystem.isPlaying)
            Deactivate();
    }
}
