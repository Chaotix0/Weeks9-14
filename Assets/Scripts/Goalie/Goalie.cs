using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    [System.Serializable]
    public struct HitboxSize
    {
        public float width;   // Total X width of the box
        public float height;  // Total Y height of the box
        public Vector2 offset; // Center shift (X, Y)
    }

    [Header("Movement Settings")]
    public float speed = 3f;
    public float maxXPosition = 2f;
    public float minXPosition = -2f;

    [Header("State Hitboxes (Calculated for 15x Scale)")]
    public HitboxSize idleHitbox = new HitboxSize { width = 2.2f, height = 2.4f, offset = new Vector2(0f, 0f) };
    public HitboxSize gloveHitbox = new HitboxSize { width = 3.8f, height = 2.0f, offset = new Vector2(0.8f, 0.4f) };
    public HitboxSize blockerHitbox = new HitboxSize { width = 3.2f, height = 2.2f, offset = new Vector2(-0.4f, 0.2f) };
    public HitboxSize butterflyHitbox = new HitboxSize { width = 4.2f, height = 1.6f, offset = new Vector2(0f, -0.2f) };

    public HitboxSize CurrentHitbox { get; private set; }

    private Animator animator;

    private float saveWindowTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        CurrentHitbox = idleHitbox; // Default starting stance
    }
    void Update()
    {
        HandleMovement();
        HandleSaves();

        // Countdown the save buffer timer
        if (saveWindowTimer > 0)
        {
            saveWindowTimer -= Time.deltaTime;
            if (saveWindowTimer <= 0)
            {
                CurrentHitbox = idleHitbox; // Return to idle once timer expires
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 pos = transform.position;
        pos.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minXPosition, maxXPosition);
        transform.position = pos;
    }

    private void HandleSaves()
    {
        if (animator == null) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Glove");
            CurrentHitbox = gloveHitbox;
            saveWindowTimer = 0.3f; // Keeps the glove hitbox active for 0.3s
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Butterfly");
            CurrentHitbox = butterflyHitbox;
            saveWindowTimer = 0.3f; // Keeps butterfly active for 0.3s
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Blocker");
            CurrentHitbox = blockerHitbox;
            saveWindowTimer = 0.3f; // Keeps blocker active for 0.3s
        }
    }

    // Call this at the end of an animation clip via Animation Events to reset to stance
    public void ResetToIdleHitbox()
    {
        CurrentHitbox = idleHitbox;
    }

    private void OnDrawGizmos()
    {
        // Visualizes the currently active stance box in the Scene view
        Gizmos.color = Color.cyan;
        Vector3 boxCenter = (Vector2)transform.position + CurrentHitbox.offset;
        Gizmos.DrawWireCube(boxCenter, new Vector3(CurrentHitbox.width, CurrentHitbox.height, 0.1f));
    }
}
