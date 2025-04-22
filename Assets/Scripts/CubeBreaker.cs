using System.Collections.Generic;
using UnityEngine;

public class CubeBreaker : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private Exploder _exploder;

    private CubeSpawnSettings _settings = new();

    public void Process(Cube cube)
    {
        if (Random.value <= cube.SplitChance)
        {
            int newGeneration = cube.Generation + 1;
            Vector3 cubePosition = cube.transform.position;
            float cubeScale = cube.transform.localScale.x;
            int newCubeCount = Random.Range(_settings.MinCubes, _settings.MaxCubes + 1);
            List<Rigidbody> newCubes = new();

            for (int i = 0; i < newCubeCount; i++)
            {
                Cube newCube = _cubePool.GetCube();
                newCube.transform.position = cubePosition;
                newCube.Init(newGeneration, _settings.StartingCubeScale, _settings.StartingSplitChance);
                newCubes.Add(newCube.GetComponent<Rigidbody>());
            }

            _exploder.Scatter(newCubes, cubePosition, cubeScale);
        }

        _cubePool.ReturnToPool(cube);
    }
}