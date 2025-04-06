using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MouseClickHandler : MonoBehaviour
{
    private const int MouseButton = 0;
    
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private LayerMask _cubeLayerMask;
    
    private CubeSpawnSettings _settings = new();
    private Camera _mainCamera; 

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButton))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cubeLayerMask))
            {
                if (hit.collider.TryGetComponent(out Cube cube))
                {
                    Break(cube);
                }
            }
        }
    }

    private void Break(Cube cube)
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
                newCube.transform.localScale = cube.transform.localScale;
                newCube.Init();
                newCubes.Add(newCube);
            }
            
            _exploder.Scatter(newCubes, cubePosition);
        }

        _cubeFactory.ReturnToPool(cube);
    }
}