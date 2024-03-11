using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 minCameraPos;
    [SerializeField] Vector3 maxCameraPos;

    void Start()
    {

    }

    void LateUpdate()
    {
        float posX = Mathf.Clamp(player.position.x, minCameraPos.x, maxCameraPos.x);
        float posY = Mathf.Clamp(player.position.y, minCameraPos.y, maxCameraPos.y);

        // Convert the position to PPU so you don't get decimal place positions
        Vector3 pos = new Vector3(posX, posY, transform.position.z);
        pos = PixelSnapper.SnapToPixelGrid(pos);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    void OnDrawGizmos()
    {
        // Draw a red line box representing the camera bounds
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minCameraPos.x, minCameraPos.y, 0), new Vector3(maxCameraPos.x, minCameraPos.y, 0));
        Gizmos.DrawLine(new Vector3(maxCameraPos.x, minCameraPos.y, 0), new Vector3(maxCameraPos.x, maxCameraPos.y, 0));
        Gizmos.DrawLine(new Vector3(maxCameraPos.x, maxCameraPos.y, 0), new Vector3(minCameraPos.x, maxCameraPos.y, 0));
        Gizmos.DrawLine(new Vector3(minCameraPos.x, maxCameraPos.y, 0), new Vector3(minCameraPos.x, minCameraPos.y, 0));
    }
}