using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject baitPrefab;
    public LineRenderer lineRenderer;

    private GameObject baitInstance;

    public void CastLine(Vector3 targetPosition)
    {
        // Instantiate the bait at the target position
        baitInstance = Instantiate(baitPrefab, targetPosition, Quaternion.identity);

        // Set the positions of the LineRenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, baitInstance.transform.position);
    }

    public void ReelIn()
    {
        if (baitInstance != null)
        {
            // Destroy the bait instance
            Destroy(baitInstance);
            baitInstance = null;

            // Clear the LineRenderer
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}