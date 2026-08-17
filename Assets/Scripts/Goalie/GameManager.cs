using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject instructionsPanel;
    public GameObject countdownPanel;
    public TMPro.TMP_Text countdownText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip goalSound;
    public AudioClip gameOverSound;

    [Header("Gameplay Objects")]
    public GameObject puckSpawnerPrefab;
    private bool isGameActive = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ShowStartScreen();
    }

    public void ShowStartScreen()
    {
        if (startPanel != null) startPanel.SetActive(true);
        if (instructionsPanel != null) instructionsPanel.SetActive(false);
        if (countdownPanel != null) countdownPanel.SetActive(false);
        Time.timeScale = 0f; // Freeze game logic during menus
    }

    public void ShowInstructions()
    {
        if (startPanel != null) startPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(true);
    }

    public void StartCountdown()
    {
        if (instructionsPanel != null) instructionsPanel.SetActive(false);
        if (startPanel != null) startPanel.SetActive(false);
        if (countdownPanel != null) countdownPanel.SetActive(true);
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        Time.timeScale = 1f;

        if (countdownText != null) countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        if (countdownText != null) countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        if (countdownPanel != null) countdownPanel.SetActive(false);
        StartGame();
    }

    private void StartGame()
    {
        isGameActive = true;

        if (puckSpawnerPrefab != null)
        {
            Instantiate(puckSpawnerPrefab);
        }
    }

    public void TriggerGoalScored()
    {
        if (!isGameActive) return;
        isGameActive = false;

        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        if (audioSource != null)
        {
            if (goalSound != null) audioSource.PlayOneShot(goalSound);
            if (gameOverSound != null) audioSource.PlayOneShot(gameOverSound);
        }

        yield return new WaitForSeconds(2f);

        // Reset scene back to Title Screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
