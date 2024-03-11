using UnityEngine;
using UnityEngine.U2D;
public static class PixelSnapper
{
    private static float PixelsPerUnit;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        PixelPerfectCamera pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();
        PixelsPerUnit = pixelPerfectCamera.assetsPPU;
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