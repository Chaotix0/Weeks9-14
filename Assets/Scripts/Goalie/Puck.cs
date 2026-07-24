using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Puck : MonoBehaviour
{
    [Header("Spawn Boundaries")]
    public float maxXPosition = 2f;
    public float minXPosition = -2f;
    public float maxYPosition = 0f;
    public float minYPosition = -2f;

    [Header("Animation Curves")]
    public AnimationCurve size;   // Scales down to mimic flying toward the net
    public AnimationCurve crazy;  // Handles the erratic zig-zag movement

    [Header("References")]
    public GameObject goalie;
    public GameObject puckPrefab; // Rename 'puck' to 'puckPrefab' to avoid self-reference confusion
    public GameObject score;
    public GameObject textControls;

    private int puckTypeChance = 0;
    private float timeElapsed;
    private float targetX;
    private float targetY;

    // Making this static or assigning it directly is safer, but keeping your UnityEvent workflow:
    private UnityEvent onSave;

    void Start()
    {
        // 1. Pick a random target spot in the net
        targetX = Random.Range(minXPosition, maxXPosition);
        targetY = Random.Range(minYPosition, maxYPosition);

        // Start the puck at its baseline position (optionally, you can start it at full scale)
        transform.position = new Vector3(targetX, targetY, 0);

        // 2. Set up scoring events
        onSave = new UnityEvent();
        if (score != null) onSave.AddListener(score.GetComponent<Score>().save);
        if (textControls != null) onSave.AddListener(textControls.GetComponent<Controls>().save);

        // 3. Roll the dice: 10% chance to be a Crazy Puck
        puckTypeChance = Random.Range(0, 11);

        if (puckTypeChance == 10)
        {
            StartCoroutine(CrazyPuckRoutine());
        }
    }

    void Update()
    {
        // Only run standard movement if it's a regular puck
        if (puckTypeChance < 10)
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector3.one * size.Evaluate(timeElapsed);

            CheckPuckArrival();
        }
    }

    public IEnumerator CrazyPuckRoutine()
    {
        // Loop runs until the puck shrinks away
        while (transform.localScale.x > 0.001f)
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector3.one * size.Evaluate(timeElapsed);

            // Zig-zag Lerp calculation based on your original math
            float crunchX = Mathf.Lerp(targetX - 1f, targetX, crazy.Evaluate(timeElapsed));
            transform.position = new Vector3(crunchX, targetY, 0);

            yield return null;
        }

        // Once the loop naturally finishes out, process the end of the shot
        ProcessShotResult();
    }

    void CheckPuckArrival()
    {
        if (transform.localScale.x <= 0.001f)
        {
            ProcessShotResult();
        }
    }

    void LateUpdate()
    {
        // Only handle standard puck arrival in LateUpdate
        if (puckTypeChance < 10)
        {
            if (transform.localScale.x <= 0.001f)
            {
                ProcessShotResult();
            }
        }
    }

    void ProcessShotResult()
    {
        Goalie goalieScript = goalie.GetComponent<Goalie>();

        // Get active hitbox from Goalie (or use default fallback)
        Goalie.HitboxSize goalieBox = (goalieScript != null)
            ? goalieScript.CurrentHitbox
            : new Goalie.HitboxSize { width = 2.2f, height = 2.4f, offset = Vector2.zero };

        // Calculate Goalie's active hitbox center location
        Vector2 goalieCenter = (Vector2)goalie.transform.position + goalieBox.offset;

        // Center-to-center distance check
        float diffX = Mathf.Abs(goalieCenter.x - targetX);
        float diffY = Mathf.Abs(goalieCenter.y - targetY);

        // Precise pixel-match check (Puck lands inside the Goalie's active box)
        bool hitX = diffX <= (goalieBox.width / 2f);
        bool hitY = diffY <= (goalieBox.height / 2f);

        Debug.Log($"[SHOT END] Goalie Box Size: ({goalieBox.width}, {goalieBox.height}) | X Diff: {diffX:F2} <= {goalieBox.width / 2f:F2} | Y Diff: {diffY:F2} <= {goalieBox.height / 2f:F2}");

        if (hitX && hitY)
        {
            // SAVE MADE!
            onSave.Invoke();

            if (puckPrefab != null)
            {
                Instantiate(puckPrefab);
            }
        }
        else
        {
            // GOAL SCORED!
            Debug.Log($"GOAL! Missed save. X diff: {diffX:F2} (Max: {goalieBox.width / 2f}), Y diff: {diffY:F2}");
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        // Draw the target zone box at the puck's destination
        Gizmos.color = Color.green;

        // Creates a visual box based on your hit margins (Width = 1.3 * 2, Height = 1.1 * 2)
        Vector3 targetCenter = new Vector3(targetX, targetY, 0);
        Vector3 boxSize = new Vector3(1.3f * 2f, 1.1f * 2f, 0.1f);

        Gizmos.DrawWireCube(targetCenter, boxSize);

        // If goalie is assigned, draw a line connecting goalie to the target spot
        if (goalie != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(goalie.transform.position, targetCenter);
        }
    }
}