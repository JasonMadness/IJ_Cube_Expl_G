using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private LayerMask _cubeLayerMask;

    private const int LeftMouseButton = 0;
    
    private GameSettings _settings = new();
    private int _startingCubesCount = 3;
    private int _step = 5;

    private void Start()
    {
        for (int i = 0; i < _startingCubesCount; i++)
        {
            Cube cube = _cubeFactory.GetCube();
            cube.transform.position = new Vector3(i * _step, 0, 0);
            cube.transform.localScale = _settings.StartingCubeScale;
            cube.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cubeLayerMask))
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
            int newCubeCount = Random.Range(_settings.MinCubes, _settings.MaxCubes);
            List<Cube> newCubes = new List<Cube>();
            
            for (int i = 0; i < newCubeCount; i++)
            {
                Cube newCube = _cubeFactory.GetCube();
                newCube.transform.position = cubePosition;
                newCube.transform.localScale = cube.transform.localScale;
                newCube.HalveLocalScale();
                newCube.HalveSplitChance();
                newCubes.Add(newCube);
            }
            
            _exploder.Scatter(newCubes, cubePosition);
        }

        cube.gameObject.SetActive(false);
    }
}