using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 500f; 
    [SerializeField] private float _baseExplosionRadius = 5f; 
    [SerializeField] private float _initialOffset = 0.1f; 
    [SerializeField] private LayerMask _cubeLayerMask;

    public void Scatter(List<Rigidbody> newCubes, Vector3 explosionOrigin, float cubeScale)
    {
        float scaleFactor = Mathf.Clamp(4f / cubeScale, 1f, 10f);
        float explosionRadius = _baseExplosionRadius * scaleFactor;
        float explosionForce = _baseExplosionForce * scaleFactor;

        Collider[] colliders = Physics.OverlapSphere(explosionOrigin, explosionRadius, _cubeLayerMask);
        List<Rigidbody> affectedCubes = new List<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                affectedCubes.Add(rigidbody);
        }

        affectedCubes.AddRange(newCubes);

        foreach (Rigidbody cube in affectedCubes)
        {
            Vector3 offset = Random.insideUnitSphere * _initialOffset;
            cube.transform.position += offset;
            cube.AddExplosionForce(explosionForce, explosionOrigin, explosionRadius);
        }
    }
}