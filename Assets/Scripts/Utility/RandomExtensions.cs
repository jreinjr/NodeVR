using UnityEngine;

public static class RandomExtensions
{
    /// <summary>
    /// Generates a new Vector3 point inside a unit square
    /// centered at (0,0,0) with side length 1
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Vector3 InsideUnitCube()
    {
        float rX = UnityEngine.Random.Range(-0.5f, 0.5f);
        float rY = UnityEngine.Random.Range(-0.5f, 0.5f);
        float rZ = UnityEngine.Random.Range(-0.5f, 0.5f);

        return new Vector3(rX, rY, rZ);
    }
}