using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    
    private Queue<Cube> _pool = new Queue<Cube>();

    public Cube GetCube()
    {
        if (_pool.Count == 0)
            Create();
        
        Cube cubeToGet = _pool.Dequeue();
        
        cubeToGet.gameObject.SetActive(true);
        return cubeToGet;
    }

    public void ReturnToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }

    private void Create()
    {
        Cube newCube = Instantiate(_prefab, transform);
        ReturnToPool(newCube);
    }
}