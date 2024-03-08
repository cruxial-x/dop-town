using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float speed = 1.0f;
    public LayerMask waterLayer;
    public float detectionRadius = 5.0f;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        ChooseNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the target
        Vector3 direction = targetPosition - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate to face the direction of movement
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // If we've reached the target, choose a new one
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChooseNewTarget();
        }
    }

    void ChooseNewTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, waterLayer);
        if (hitColliders.Length > 0)
        {
            int randomIndex = Random.Range(0, hitColliders.Length);
            targetPosition = hitColliders[randomIndex].transform.position;
        }
    }
}