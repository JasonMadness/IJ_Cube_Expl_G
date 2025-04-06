using UnityEngine;

public class CubeSpawnSettings
{
    public Vector3 StartingCubeScale { get; } = Vector3.one * 4;
    public int MinCubes { get; } = 2;
    public int MaxCubes { get; } = 6;
}
