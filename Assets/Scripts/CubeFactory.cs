using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    
    private List<Cube> _pool;

    private void Awake()
    {
        _pool = new List<Cube>();
    }

    public Cube GetCube()
    {
        Cube cubeToGet = _pool.FirstOrDefault(cube => cube.gameObject.activeSelf == false);

        if (cubeToGet == null)
            cubeToGet = CreateNewCube();
        
        cubeToGet.gameObject.SetActive(true);
        return cubeToGet;
    }

    private Cube CreateNewCube()
    {
        Cube newCube = Instantiate(_prefab, transform);
        newCube.gameObject.SetActive(false);
        _pool.Add(newCube);
        return newCube;
    }
}