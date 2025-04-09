using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    
    private Queue<Cube> _pool = new Queue<Cube>();

    public Cube GetCube()
    {
        Cube cubeToGet = _pool.Dequeue();

        if (cubeToGet == null)
            cubeToGet = Create();
        
        cubeToGet.gameObject.SetActive(true);
        return cubeToGet;
    }

    public void ReturnToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }

    private Cube Create()
    {
        Cube newCube = Instantiate(_prefab, transform);
        ReturnToPool(newCube);
        return newCube;
    }
}