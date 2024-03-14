using UnityEngine;

public class PillarPlacer : MonoBehaviour
{
    public GameObject pillarPrefab; // Assign your pillar Prefab in the inspector
    public Vector2[] positions; // Assign the positions where you want to place the pillars in the inspector

    void Start()
    {
        foreach (Vector2 pos in positions)
        {
            Instantiate(pillarPrefab, pos, Quaternion.identity);
        }
    }
    // Draw Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector2 pos in positions)
        {
            Gizmos.DrawSphere(pos, 0.5f);
        }
    }
}