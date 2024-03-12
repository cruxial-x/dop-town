using System.Collections;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject baitPrefab;
    public LineRenderer lineRenderer;

    private GameObject baitInstance;
    public float castSpeed = 1f; // Speed of casting and reeling in
    public float castHeight = 2f; // The height of the parabolic trajectory
    private void Update()
    {
        if(baitInstance != null)
        {
            // lineRenderer.SetPosition(0, transform.position);
            // lineRenderer.SetPosition(1, baitInstance.transform.position);
        }
    }


    private void Start()
    {
        // Set the color of the LineRenderer to white
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }
    private IEnumerator UpdateLineRenderer()
    {
        while (baitInstance != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, baitInstance.transform.position);
            yield return null;
        }
    }

    public IEnumerator CastLine(Vector3 targetPosition)
    {
        // Check if a bait instance is already out
        if (baitInstance != null)
        {
            // A bait instance is already out, so return without doing anything
            yield break;
        }

        // Instantiate the bait at the player's position
        baitInstance = Instantiate(baitPrefab, transform.position, Quaternion.identity);
        Rigidbody2D baitRigidbody = baitInstance.GetComponent<Rigidbody2D>();

        StartCoroutine(UpdateLineRenderer());

        // Calculate the velocity needed to throw the bait to the target position
        Vector3 velocity = CalculateLaunchVelocity(transform.position, targetPosition, castHeight);

        // Apply the velocity to the bait's Rigidbody2D
        baitRigidbody.velocity = velocity;

    // While the bait is in the air
    while (baitInstance.transform.position.y > transform.position.y)
    {
        // Update the LineRenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, baitInstance.transform.position);
        yield return null;
    }

        // Wait for a bit after the bait has started descending
        yield return new WaitForSeconds(1f); // adjust this value as needed

        // Stop the bait's movement
        baitRigidbody.velocity = Vector3.zero;
        baitRigidbody.gravityScale = 0;

        // Start the bobbing animation
        // StartCoroutine(BobbingAnimation());
    }

    private IEnumerator BobbingAnimation()
    {
        float bobbingSpeed = 0.1f; // adjust this value as needed
        float bobbingAmount = 0.5f; // adjust this value as needed
        float initialY = baitInstance.transform.position.y;

        while (true)
        {
            float newY = initialY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;
            baitInstance.transform.position = new Vector3(baitInstance.transform.position.x, newY, baitInstance.transform.position.z);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, baitInstance.transform.position);

            yield return null;
        }
    }
    private Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float height)
    {
        // Calculate the distance between the start and end positions
        float distance = Vector3.Distance(start, end);

        // Calculate the initial velocity needed to reach the target position
        float initialVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * height);

        // Calculate the total time of flight
        float time1 = (-2 * height / Physics2D.gravity.y) > 0 ? Mathf.Sqrt(-2 * height / Physics2D.gravity.y) : 0;
        float time2 = (2 * (distance - height) / Physics2D.gravity.y) > 0 ? Mathf.Sqrt(2 * (distance - height) / Physics2D.gravity.y) : 0;
        float time = time1 + time2;

        // Check if time is zero to avoid division by zero
        if (time == 0)
        {
            return Vector3.zero;
        }

        // Calculate the velocity vector
        Vector3 velocity = new Vector3((end - start).x / time, initialVelocity, 0);

        return velocity;
    }
    public IEnumerator ReelIn()
    {
        if (baitInstance != null)
        {
            // Gradually move the bait back to the player's position
            Vector3 startPosition = baitInstance.transform.position;
            float startTime = Time.time;
            while (Time.time < startTime + castSpeed)
            {
                baitInstance.transform.position = Vector3.Lerp(startPosition, transform.position, (Time.time - startTime) / castSpeed);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, baitInstance.transform.position);
                yield return null;
            }

            // Destroy the bait instance
            Destroy(baitInstance);
            baitInstance = null;

            // Clear the LineRenderer
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}