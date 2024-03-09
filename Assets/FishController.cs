using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FishController : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public Bounds moveBounds;
    private float speed;
    private Vector2 direction;
    private Color originalColor;
    private GameObject fishNose;
    // private float collisionCooldown = 0f; 

    private void Start()
    {
        if (transform.childCount > 0)
        {
            fishNose = transform.GetChild(0).gameObject; // Get the first child of the fish GameObject
        }
        else
        {
            Debug.LogError("Fish GameObject has no children");
        }

        StartCoroutine(ChangeDirection());
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        originalColor = color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        // Ignore collisions with the "Default" layer (player)
        Physics2D.IgnoreLayerCollision(gameObject.layer, 0, true);
    }
    private void OnDrawGizmos()
    {
        // Draw a red box for the moveBounds
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(moveBounds.center, moveBounds.size);
    }
    private void Update()
    {
        Vector2 offset = fishNose.transform.localPosition; // Offset based on fishNose's local position
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
        Vector2 nosePosition = newPosition + offset;

        // Check if the new position is within the move bounds
        if (moveBounds.Contains(nosePosition))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // Round the position to the nearest pixel
            float pixelsPerUnit = 16; // Change this to match your Sprite's Pixels Per Unit setting
            newPosition = new Vector2(
                Mathf.Round(newPosition.x * pixelsPerUnit) / pixelsPerUnit,
                Mathf.Round(newPosition.y * pixelsPerUnit) / pixelsPerUnit
            );

            rb.MovePosition(newPosition);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            Vector2 newDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            float newSpeed = Random.Range(minSpeed, maxSpeed);

            float changeDuration = Random.Range(1f, 5f);
            float startTime = Time.time;

            Vector2 oldDirection = direction;
            float oldSpeed = speed;

            while (Time.time < startTime + changeDuration)
            {
                float t = (Time.time - startTime) / changeDuration;
                direction = Vector2.Lerp(oldDirection, newDirection, t);
                speed = Mathf.Lerp(oldSpeed, newSpeed, t);

                yield return null;
            }

            direction = newDirection;
            speed = newSpeed;

            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }
    public void CatchFish()
    {
        StopAllCoroutines();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
        Destroy(gameObject);
    }
}