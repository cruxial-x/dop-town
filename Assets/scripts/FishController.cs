using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 2f;
    public Rect moveBounds;

    private float currentSpeed;
    private Vector2 targetPosition;
    private float targetRotation;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;

        StartCoroutine(ChangeTargetPositionCoroutine());
    }

    private IEnumerator ChangeTargetPositionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            currentSpeed = Random.Range(minSpeed, maxSpeed);
            targetPosition = new Vector2(
                Random.Range(moveBounds.min.x, moveBounds.max.x),
                Random.Range(moveBounds.min.y, moveBounds.max.y)
            );

            Vector2 direction = targetPosition - (Vector2)transform.position;
            targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > 0.01f)  // If the fish is not already at the target position
        {
            // Rotate the fish to face the target position
            transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed * 360));  // Multiply by 360 to convert speed from rotations/second to degrees/second

            // Move the fish towards the target position
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * currentSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(moveBounds.center, moveBounds.size);
    }
}