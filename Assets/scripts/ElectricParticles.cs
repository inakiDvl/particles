using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private List<ParticleData> particlesData = new();

    private List<ParticleSystem> particleInstances = new();
    private ParticleController particleController;

    public void PlayParticles(ParticleEffectType type)
    {
        if (type != ParticleEffectType.Electric)
            return;

        foreach (var particle in particleInstances)
        {
            particle.Play();
        }
    }

    private void Awake()
    {
        particleController = GetComponent<ParticleController>();

        foreach (var swpawnPoint in particlesData)
        {
            ParticleSystem particleInstance = Instantiate(particlePrefab, swpawnPoint.SpawnPoint);
            particleInstance.Stop();
            var shape = particleInstance.shape;
            shape.radius = swpawnPoint.Radius;
            particleInstances.Add(particleInstance);
        }
    }

    private void OnEnable()
    {
        particleController.OnStartParticles += PlayParticles;
    }

    private void OnDisable()
    {
        particleController.OnStartParticles -= PlayParticles;
    }

    [Serializable] private class ParticleData
    {
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
    }
}
