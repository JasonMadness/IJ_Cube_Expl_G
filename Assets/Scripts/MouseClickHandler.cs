using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MouseClickHandler : MonoBehaviour
{
    private const int MouseButton = 0;

    [SerializeField] private CubeBreaker _cubeBreaker;
    [SerializeField] private LayerMask _cubeLayerMask;
    
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
                    _cubeBreaker.Process(cube);
                }
            }
        }
    }
}