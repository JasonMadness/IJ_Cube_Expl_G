using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private float _splitChance = 1f;
    private float _half = 0.5f;

    public float SplitChance => _splitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Random.ColorHSV();
    }

    public void HalveSplitChance()
    {
        _splitChance *= _half;
    }

    public void HalveLocalScale()
    {
        transform.localScale *= _half;
    }
}