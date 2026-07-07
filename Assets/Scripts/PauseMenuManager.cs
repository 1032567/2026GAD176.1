using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for keyboard/controller selection
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class AdvancedPauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private CanvasGroup canvasGroup;    // Drag PauseMenuPanel here
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button buttonPrefab;

    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.2f;

    private bool isPaused = false;
    private List<GameObject> activeButtons = new List<GameObject>();
    private Coroutine fadeCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) TogglePause(false);
            else TogglePause(true);
        }
    }

    public void TogglePause(bool pause)
    {
        isPaused = pause;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        if (isPaused)
        {
            pauseMenuPanel.SetActive(true);
            GeneratePauseButtons();
            fadeCoroutine = StartCoroutine(FadeMenu(0f, 1f, true));
        }
        else
        {
            fadeCoroutine = StartCoroutine(FadeMenu(1f, 0f, false));
        }
    }

    void GeneratePauseButtons()
    {
        ClearButtons();

        string[] menuOptions = { "Resume", "Options", "Restart Level", "Main Menu" };
        Button firstButton = null;

        for (int i = 0; i < menuOptions.Length; i++)
        {
            string optionName = menuOptions[i];
            Button newButton = Instantiate(buttonPrefab, buttonContainer, false);
            activeButtons.Add(newButton.gameObject);

            // Set Text
            TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
            if (textComponent != null) textComponent.text = optionName;

            // Set Action
            newButton.onClick.AddListener(() => OnMenuOptionSelected(optionName));

            // Track the very first button for keyboard selection
            if (i == 0) firstButton = newButton;
        }

        // CRUCIAL FOR ARROW KEYS: Tell the EventSystem to highlight the first button
        if (firstButton != null && EventSystem.current != null)
        {
            // Clear current selection first to ensure a fresh input focus
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }

    IEnumerator FadeMenu(float startAlpha, float endAlpha, bool pausing)
    {
        // If unpausing, resume time IMMEDIATELY so the code keeps running normally
        if (!pausing) Time.timeScale = 1f;

        float elapsedTime = 0f;
        canvasGroup.alpha = startAlpha;

        // Use unscaledDeltaTime because Time.timeScale will be 0 when paused!
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (pausing)
        {
            Time.timeScale = 0f; // Freeze game AFTER the fade completes
        }
        else
        {
            ClearButtons();
            pauseMenuPanel.SetActive(false); // Hide panel AFTER fade out completes
        }
    }

    void OnMenuOptionSelected(string option)
    {
        switch (option)
        {
            case "Resume":
                TogglePause(false);
                break;
            case "Restart Level":
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                break;
            case "Main Menu":
                Time.timeScale = 1f;
                break;
        }
    }

    void ClearButtons()
    {
        foreach (GameObject btn in activeButtons) Destroy(btn);
        activeButtons.Clear();
    }
}
