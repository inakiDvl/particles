using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private List<ElectricParticleConfig> particlesConfig = new();

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

        foreach (var particleData in particlesConfig)
        {
            ParticleSystem particleInstance = Instantiate(particlePrefab, particleData.Parent);
            particleInstance.Stop();

            var shape = particleInstance.shape;
            /* var main = particleInstance.main;
            var curve = main.startSize;
            var particleSizeMin = curve.constantMin;
            var particleSizeMax = curve.constantMax; */

            Vector3 boundSize = Vector3.one;

            if (particleData.Mesh != null)
            {
                shape.shapeType = ParticleSystemShapeType.MeshRenderer;
                shape.meshRenderer = particleData.Mesh;
            }

            if (particleData.SkinnedMesh != null)
            {
                shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
                shape.skinnedMeshRenderer = particleData.SkinnedMesh;
            }

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

    [Serializable]
    private class ElectricParticleConfig
    {
        [field: SerializeField] public Transform Parent { get; private set; }
        [field: SerializeField] public MeshRenderer Mesh { get; private set; }
        [field: SerializeField] public SkinnedMeshRenderer SkinnedMesh { get; private set; }
    }
}
