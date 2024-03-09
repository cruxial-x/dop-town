using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FishController : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public Tilemap waterTilemap;
    private float speed;
    private Vector2 direction;
    private Color originalColor;

    private void Start()
    {
        StartCoroutine(ChangeDirection());
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        originalColor = color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    private void Update()
    {
        Vector2 offset = direction * 0.5f; // Change this value to adjust the offset
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime + offset;
        Vector3Int tilePosition = waterTilemap.WorldToCell(newPosition);
        if (waterTilemap.HasTile(tilePosition))
        {
            transform.position = newPosition - offset;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            Vector3Int tilePosition = waterTilemap.WorldToCell(newPosition);
            if (!waterTilemap.HasTile(tilePosition))
            {
                continue;
            }

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