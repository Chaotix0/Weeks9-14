using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puck : MonoBehaviour
{
    public float maxXPosition = 2f;
    public float minXPosition = -2f;
    public float maxYPosition = 0f;
    public float minYPosition = -2f;

    public AnimationCurve size;
    public AnimationCurve crazy;

    public GameObject goalie;
    public GameObject puckPrefab;

    public GameObject score;
    public GameObject textControls;

    private int puckTypeChance = 0;
    private float timeElapsed;
    private float targetX;
    private float targetY;

    private UnityEvent onSave;

    void Start()
    {
        targetX = Random.Range(minXPosition, maxXPosition);
        targetY = Random.Range(minYPosition, maxYPosition);

        transform.position = new Vector3(targetX, targetY, 0);

        onSave = new UnityEvent();
        if (score != null) onSave.AddListener(score.GetComponent<Score>().save);
        if (textControls != null) onSave.AddListener(textControls.GetComponent<Controls>().save);

        puckTypeChance = Random.Range(0, 11);

        if (puckTypeChance == 10)
        {
            StartCoroutine(CrazyPuckRoutine());
        }
    }

    void Update()
    {
        if (puckTypeChance < 10)
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector2.one * size.Evaluate(timeElapsed);

            if (transform.localScale.x <= 0.001f)
            {
                ProcessShotResult();
            }
        }
    }

    public IEnumerator CrazyPuckRoutine()
    {
        while (transform.localScale.x > 0.001f)
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector2.one * size.Evaluate(timeElapsed);

            float crunchX = Mathf.Lerp(targetX - 1f, targetX, crazy.Evaluate(timeElapsed));
            transform.position = new Vector3(crunchX, targetY, 0);

            yield return null;
        }

        ProcessShotResult();
    }

    void ProcessShotResult()
    {
        // Check ALL colliders touching the puck location
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        bool isSave = false;

        foreach (Collider2D col in hits)
        {
            // Ignore the puck itself or other background objects
            if (col.gameObject != gameObject && col.CompareTag("Goalie"))
            {
                isSave = true;
                break;
            }
        }

        if (isSave)
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
            Debug.Log("GOAL!");
        }

        Destroy(gameObject);
    }
}