using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Update()
    {
        float posX = Mathf.Clamp(player.position.x, minCameraPos.x, maxCameraPos.x);
        float posY = Mathf.Clamp(player.position.y, minCameraPos.y, maxCameraPos.y);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}