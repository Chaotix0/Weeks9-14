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

    void ProcessShotResult()
    {
        float goalieX = goalie.transform.position.x;
        float goalieY = goalie.transform.position.y;
        float puckX = transform.position.x;
        float puckY = transform.position.y;

        // prints out coordinates to the console window
        Debug.Log($"[HITBOX CHECK] Goalie Pos: ({goalieX}, {goalieY}) | Puck Target Pos: ({puckX}, {puckY})");

        bool hitX = (goalieX >= puckX - 1.2f && goalieX <= puckX + 1.2f);
        bool hitY = (goalieY >= puckY - 1f && goalieY <= puckY + 1f);

        Debug.Log($"Hit X status: {hitX} | Hit Y status: {hitY}");

        if (hitX && hitY)
        {
            Debug.Log("SAVE REGISTERED SUCCESSFULLY!");
            onSave.Invoke();
        }
        else
        {
            Debug.Log("GOAL! Goalie missed the puck target area.");
        }

        if (puckPrefab != null)
        {
            Instantiate(puckPrefab);
        }

        Destroy(gameObject);
    }
}