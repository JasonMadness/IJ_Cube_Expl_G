using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBreaker : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private Exploder _exploder;
    
    private CubeSpawnSettings _settings = new();

    public void Process(Cube cube)
    {
        if (Random.value <= cube.SplitChance)
        {
            Vector3 cubePosition = cube.transform.position;
            int newCubeCount = Random.Range(_settings.MinCubes, _settings.MaxCubes + 1);
            List<Cube> newCubes = new List<Cube>();
            
            for (int i = 0; i < newCubeCount; i++)
            {
                Cube newCube = _cubeFactory.GetCube();
                newCube.transform.position = cubePosition;
                newCube.Init(_settings.StartingCubeScale, _settings.StartingSplitChance);
                newCubes.Add(newCube);
            }
            
            _exploder.Scatter(newCubes, cubePosition);
        }

        _cubeFactory.ReturnToPool(cube);
    }
}
