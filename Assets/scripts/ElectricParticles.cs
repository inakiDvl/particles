using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private List<SwpanPoint> spawnPoints = new();

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

        foreach (var swpawnPoint in spawnPoints)
        {
            ParticleSystem particleInstance = Instantiate(particlePrefab, swpawnPoint.Point);
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

    [Serializable] private class SwpanPoint
    {
        [field: SerializeField] public Transform Point { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
    }
}
