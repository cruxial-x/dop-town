using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 minCameraPos;
    [SerializeField] Vector3 maxCameraPos;
    [HideInInspector] public Vector3 minEdgePos;
    [HideInInspector] public Vector3 maxEdgePos;

    void Start()
    {
        GetCameraBounds();
    }

    void LateUpdate()
    {
        float posX = Mathf.Clamp(player.position.x, minCameraPos.x, maxCameraPos.x);
        float posY = Mathf.Clamp(player.position.y, minCameraPos.y, maxCameraPos.y);

        // Convert the position to PPU so you don't get decimal place positions
        Vector3 pos = new Vector3(posX, posY, transform.position.z);
        pos = PixelSnapper.SnapToPixelGrid(gameObject, pos);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    void GetCameraBounds()
    {
        // Get the camera's size
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        // Calculate the bounds for the edges of the camera
        minEdgePos = minCameraPos - new Vector3(width / 2, height / 2, 0);
        maxEdgePos = maxCameraPos + new Vector3(width / 2, height / 2, 0);
    }

    void OnDrawGizmos()
    {
        GetCameraBounds();
        // Draw a red line box representing the camera bounds
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minEdgePos.x, minEdgePos.y, 0), new Vector3(maxEdgePos.x, minEdgePos.y, 0));
        Gizmos.DrawLine(new Vector3(maxEdgePos.x, minEdgePos.y, 0), new Vector3(maxEdgePos.x, maxEdgePos.y, 0));
        Gizmos.DrawLine(new Vector3(maxEdgePos.x, maxEdgePos.y, 0), new Vector3(minEdgePos.x, maxEdgePos.y, 0));
        Gizmos.DrawLine(new Vector3(minEdgePos.x, maxEdgePos.y, 0), new Vector3(minEdgePos.x, minEdgePos.y, 0));
    }
}