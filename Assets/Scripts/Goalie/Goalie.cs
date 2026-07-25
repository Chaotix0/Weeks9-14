using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalie : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float maxXPosition = 2f;
    public float minXPosition = -2f;

    [Header("Sprites (With Custom Physics Shapes)")]
    public Sprite idleSprite;
    public Sprite gloveSprite;
    public Sprite blockerSprite;
    public Sprite butterflySprite;

    [Header("4 Polygon Colliders")]
    public PolygonCollider2D idleCollider;
    public PolygonCollider2D gloveCollider;
    public PolygonCollider2D blockerCollider;
    public PolygonCollider2D butterflyCollider;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Bake the custom physics shapes into each polygon collider at launch
        BakeSpriteToCollider(idleSprite, idleCollider);
        BakeSpriteToCollider(gloveSprite, gloveCollider);
        BakeSpriteToCollider(blockerSprite, blockerCollider);
        BakeSpriteToCollider(butterflySprite, butterflyCollider);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        ResetToIdle();
    }

    void Update()
    {
        HandleMovement();
        HandleSaves();
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
            EnableOnlyCollider(gloveCollider);
            if (spriteRenderer != null && gloveSprite != null) spriteRenderer.sprite = gloveSprite;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Butterfly");
            EnableOnlyCollider(butterflyCollider);
            if (spriteRenderer != null && butterflySprite != null) spriteRenderer.sprite = butterflySprite;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Blocker");
            EnableOnlyCollider(blockerCollider);
            if (spriteRenderer != null && blockerSprite != null) spriteRenderer.sprite = blockerSprite;
        }
    }

    private void BakeSpriteToCollider(Sprite sprite, PolygonCollider2D collider)
    {
        if (sprite == null || collider == null) return;

        collider.pathCount = sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < collider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            collider.SetPath(i, path);
        }
    }

    private void EnableOnlyCollider(PolygonCollider2D target)
    {
        if (idleCollider != null) idleCollider.enabled = (target == idleCollider);
        if (gloveCollider != null) gloveCollider.enabled = (target == gloveCollider);
        if (blockerCollider != null) blockerCollider.enabled = (target == blockerCollider);
        if (butterflyCollider != null) butterflyCollider.enabled = (target == butterflyCollider);
    }

    // Call this via Animation Event at the end of save clips
    public void ResetToIdle()
    {
        EnableOnlyCollider(idleCollider);

        if (spriteRenderer != null && idleSprite != null)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }
}