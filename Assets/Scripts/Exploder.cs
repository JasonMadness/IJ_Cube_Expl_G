using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _initialOffset = 0.1f;

    public void Scatter(List<Cube> cubes, Vector3 explosionOrigin)
    {
        foreach (Cube cube in cubes)
        {
            Vector3 offset = Random.insideUnitSphere * _initialOffset;
            cube.transform.position += offset;
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
            rigidbody.AddExplosionForce(_explosionForce, explosionOrigin, _explosionRadius);
        }
    }
}