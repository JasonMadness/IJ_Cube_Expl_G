using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeBreaker : MonoBehaviour
{
    [FormerlySerializedAs("_cubeFactory")] [SerializeField] private CubePool _cubePool;
    [SerializeField] private Exploder _exploder;

    private CubeSpawnSettings _settings = new();

    public void Process(Cube cube)
    {
        if (Random.value <= cube.SplitChance)
        {
            Vector3 cubePosition = cube.transform.position;
            int newCubeCount = Random.Range(_settings.MinCubes, _settings.MaxCubes + 1);
            List<Rigidbody> newCubes = new();

            for (int i = 0; i < newCubeCount; i++)
            {
                Cube newCube = _cubePool.GetCube();
                newCube.transform.position = cubePosition;
                newCube.Init(_settings.StartingCubeScale, _settings.StartingSplitChance);
                newCubes.Add(newCube.GetComponent<Rigidbody>());
            }

            _exploder.Scatter(newCubes, cubePosition);
        }

        _cubePool.ReturnToPool(cube);
    }
}