using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 2f;
    private Rect spawnBounds;
    private float currentSpeed;
    private Vector2 targetPosition;
    private float targetRotation;
    private Animator animator;  // Reference to the Animator component

    private void Start()
    {
        FishSpawner[] fishSpawners = FindObjectsOfType<FishSpawner>();
        float closestDistance = Mathf.Infinity;
        FishSpawner closestFishSpawner = null;  // Declare closestFishSpawner outside the foreach loop

        foreach (FishSpawner fishSpawner in fishSpawners)
        {
            float distance = Vector3.Distance(transform.position, fishSpawner.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestFishSpawner = fishSpawner;  // Update closestFishSpawner, not declare a new one
            }
        }

        if (closestFishSpawner != null)  // Check if a FishSpawner was found
        {
            spawnBounds = closestFishSpawner.spawnBounds;  // Set spawnBounds to the bounds of the closest FishSpawner
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;

        animator = GetComponent<Animator>();  // Get the Animator component

        StartCoroutine(ChangeTargetPositionCoroutine());
    }

    private IEnumerator ChangeTargetPositionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

            currentSpeed = Random.Range(minSpeed, maxSpeed);
            targetPosition = new Vector2(
                Random.Range(spawnBounds.min.x, spawnBounds.max.x),
                Random.Range(spawnBounds.min.y, spawnBounds.max.y)
            );

            Vector2 direction = targetPosition - (Vector2)transform.position;
            targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Normalize the current speed to a value between 0 and 1
            float normalizedSpeed = (currentSpeed - minSpeed) / (maxSpeed - minSpeed);

            // Scale the normalized speed to a value between 1 and 2
            animator.speed = 1 + normalizedSpeed;
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
    // Swim towards bait if it's close enough
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("TriggerStay");
        if (other.CompareTag("Bait"))
        {
            targetPosition = other.transform.position;
        }
    }
}