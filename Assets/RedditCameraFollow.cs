using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

    public Transform target;
    public float PPU;
    public float xSpeed;
    public float ySpeed;
    public float minX, maxX, minY, maxY;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //determine target location and move gradually to the position
        float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, xSpeed);
        float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, ySpeed);
        //Convert the position to PPU so you don't get decimal place positions
        float ppuPosX = Mathf.RoundToInt(posX * PPU) / PPU;
        float ppuPosY = Mathf.RoundToInt(posY * PPU) / PPU;
        // Move camera to new location
        transform.position = new Vector3(Mathf.Clamp(ppuPosX, minX, maxX), Mathf.Clamp(ppuPosY, minY, maxY), transform.position.z);
    }
}

