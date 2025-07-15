using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleEffectType particleEffectType;
    [SerializeField] private bool toggle = false;

    public event Action<ParticleEffectType> OnStartParticles;
    
    public void Update()
    {
        if (toggle)
        {
            toggle = false;
            OnStartParticles?.Invoke(particleEffectType);
        }
    }
}
