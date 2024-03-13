using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public interface IMovementStrategy
{
    void HandleMovement(ref float moveHorizontal, ref float moveVertical);
}

public class DiagonalMovement : IMovementStrategy
{
    public void HandleMovement(ref float moveHorizontal, ref float moveVertical)
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }
}

public class CardinalMovement : IMovementStrategy
{
    public void HandleMovement(ref float moveHorizontal, ref float moveVertical)
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(inputHorizontal) > Mathf.Abs(inputVertical))
        {
            moveHorizontal = inputHorizontal;
            moveVertical = 0;
        }
        else
        {
            moveVertical = inputVertical;
            moveHorizontal = 0;
        }
    }
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("Speed of the player movement")]
    [SerializeField] float moveSpeed = 2f;

    private float moveHorizontal, moveVertical;

    [Tooltip("Flag for movement state")]
    private bool isMoving;

    private Animator animator;
    private Rigidbody2D rb;

    [Tooltip("Flag for enabling diagonal movement")]
    [SerializeField] bool enableDiagonalMovement = false;

    private IMovementStrategy movementStrategy;

    [Tooltip("Threshold for changing walking animation")]
    [SerializeField] private float animationThreshold = 0.1f;
    public FishingRod fishingRod;
    private Vector3 targetPosition;
    private Vector3 lastCastDirection = Vector3.up; // Default to up
    public float castDistance = 3f;
    public GameObject actionUI;
    public Sprite keydownImage;
    private Sprite originalImage;
    private bool canPerformAction = false;
    private Vector2 minEdgePos;
    private Vector2 maxEdgePos;

// Start is called before the first frame update
void Start()
{
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();

    // Ignore collisions with fish
    int playerLayer = LayerMask.NameToLayer("Default");
    int fishLayer = LayerMask.NameToLayer("Water");
    Physics2D.IgnoreLayerCollision(playerLayer, fishLayer);
    movementStrategy = enableDiagonalMovement ? new DiagonalMovement() : new CardinalMovement();

    // Get a reference to the CameraController script
    CameraFollow cameraController = Camera.main.GetComponent<CameraFollow>();

    // Initialize the camera boundaries
    minEdgePos = cameraController.minEdgePos;
    maxEdgePos = cameraController.maxEdgePos;
}

    // Update is called once per frame
    void Update()
    {
        // Movement
        movementStrategy.HandleMovement(ref moveHorizontal, ref moveVertical);

        // // Update the last cast direction if the player is moving
        // Vector3 currentDirection = new Vector3(moveHorizontal, moveVertical, 0).normalized;
        // if (currentDirection.magnitude != 0)
        // {
        //     lastCastDirection = currentDirection;
        // }

        // //Fishing
        // // Check if the player has pressed the cast button (e.g., the space bar)
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // Set the target position to some position in the direction of the player's movement
        //     targetPosition = transform.position + lastCastDirection * castDistance;
        //     // Cast the line to the target position
        //     StartCoroutine(fishingRod.CastLine(targetPosition));
        // }
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     StartCoroutine(fishingRod.ReelIn());
        // }

        // If the absolute value of the horizontal or vertical movement is less than the animation threshold, consider it as zero
        float effectiveMoveHorizontal = Mathf.Abs(moveHorizontal) < animationThreshold ? 0 : moveHorizontal;
        float effectiveMoveVertical = Mathf.Abs(moveVertical) < animationThreshold ? 0 : moveVertical;

        isMoving = effectiveMoveHorizontal != 0 || effectiveMoveVertical != 0;

        if (effectiveMoveHorizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (effectiveMoveHorizontal > 0 || (effectiveMoveHorizontal == 0 && effectiveMoveVertical != 0))
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetBool("movingUp", effectiveMoveVertical > 0 && effectiveMoveHorizontal == 0);
        animator.SetBool("movingDown", effectiveMoveVertical < 0 && effectiveMoveHorizontal == 0);
        animator.SetBool("isMoving", isMoving);

        if (canPerformAction)
        {
            if (actionUI.TryGetComponent<UnityEngine.UI.Image>(out var actionUIImage))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Set originalImage here, only when the E key is first pressed down
                    if (originalImage == null)
                    {
                        originalImage = actionUIImage.sprite;
                    }

                    actionUIImage.sprite = keydownImage;
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    // Change the image back to the original when the E key is released
                    actionUIImage.sprite = originalImage;
                    originalImage = null; // Reset originalImage for the next key press
                }
            }
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
        rb.velocity = movement;

        Vector2 pos = rb.position;
        pos = PixelSnapper.SnapToPixelGrid(pos);

        // Clamp the player's position within the box
        pos.x = Mathf.Clamp(pos.x, minEdgePos.x, maxEdgePos.x);
        pos.y = Mathf.Clamp(pos.y, minEdgePos.y, maxEdgePos.y);

        rb.position = pos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Action"))
        {
            actionUI.SetActive(true);
            canPerformAction = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Action"))
        {
            actionUI.SetActive(false);
            canPerformAction = false;
        }
    }
}