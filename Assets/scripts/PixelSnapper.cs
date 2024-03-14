using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;

public static class PixelSnapper
{
    private static float PixelsPerUnit;
    private static Dictionary<GameObject, Vector2> remainders2D = new Dictionary<GameObject, Vector2>();
    private static Dictionary<GameObject, Vector3> remainders3D = new Dictionary<GameObject, Vector3>();

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        PixelPerfectCamera pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();
        PixelsPerUnit = pixelPerfectCamera.assetsPPU;
    }

    public static Vector2 SnapToPixelGrid(GameObject obj, Vector2 position)
    {
        if (!remainders2D.ContainsKey(obj))
        {
            remainders2D[obj] = Vector2.zero;
        }

        Vector2 remainder2D = remainders2D[obj];

        float newX = position.x * PixelsPerUnit;
        float newY = position.y * PixelsPerUnit;

        remainder2D.x += newX - Mathf.Floor(newX);
        remainder2D.y += newY - Mathf.Floor(newY);

        if (remainder2D.x >= 1)
        {
            newX += 1;
            remainder2D.x -= 1;
        }

        if (remainder2D.y >= 1)
        {
            newY += 1;
            remainder2D.y -= 1;
        }

        position.x = Mathf.Floor(newX) / PixelsPerUnit;
        position.y = Mathf.Floor(newY) / PixelsPerUnit;

        remainders2D[obj] = remainder2D;

        return position;
    }

    public static Vector3 SnapToPixelGrid(GameObject obj, Vector3 position)
    {
        if (!remainders3D.ContainsKey(obj))
        {
            remainders3D[obj] = Vector3.zero;
        }

        Vector3 remainder3D = remainders3D[obj];

        float newX = position.x * PixelsPerUnit;
        float newY = position.y * PixelsPerUnit;

        remainder3D.x += newX - Mathf.Floor(newX);
        remainder3D.y += newY - Mathf.Floor(newY);

        if (remainder3D.x >= 1)
        {
            newX += 1;
            remainder3D.x -= 1;
        }

        if (remainder3D.y >= 1)
        {
            newY += 1;
            remainder3D.y -= 1;
        }

        position.x = Mathf.Floor(newX) / PixelsPerUnit;
        position.y = Mathf.Floor(newY) / PixelsPerUnit;

        remainders3D[obj] = remainder3D;

        return position;
    }
    public static Vector2 SnapToPixelGrid(Vector2 position)
    {
        position.x = Mathf.Round(position.x * PixelsPerUnit) / PixelsPerUnit;
        position.y = Mathf.Round(position.y * PixelsPerUnit) / PixelsPerUnit;
        return position;
    }

    public static Vector3 SnapToPixelGrid(Vector3 position)
    {
        position.x = Mathf.Round(position.x * PixelsPerUnit) / PixelsPerUnit;
        position.y = Mathf.Round(position.y * PixelsPerUnit) / PixelsPerUnit;
        return position;
    }
}