using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private int _generation = 0;
    private float _splitChance = 1f;
    private float _half = 0.5f;

    public int Generation => _generation;
    public float SplitChance => _splitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Random.ColorHSV();
    }

    public void Init(int generation, Vector3 startingScale, float startingSplitChance)
    {
        _generation = generation;
        CalculateSplitChance(startingSplitChance);
        CalculateLocalScale(startingScale);
    }

    private void CalculateSplitChance(float startingSplitChance)
    {
        for (int i = 0; i < _generation; i++)
            startingSplitChance *= _half;

        _splitChance = startingSplitChance;
    }

    private void CalculateLocalScale(Vector3 startingScale)
    {
        for (int i = 0; i < _generation; i++)
            startingScale *= _half;

        transform.localScale = startingScale;
    }
}