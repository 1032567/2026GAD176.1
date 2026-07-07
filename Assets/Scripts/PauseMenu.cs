using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject buttonPrefab;   // drag MenuButtonPrefab here
    public Transform buttonContainer; // drag PausePanel here (parent for spawned buttons)

    private bool isPaused = false;
    private List<MenuButtonData> menuButtons;

    void Start()
    {
        // Define our menu here — the "list of buttons"
        menuButtons = new List<MenuButtonData>
        {
            new MenuButtonData { label = "Resume", onClick = ResumeGame },
            new MenuButtonData { label = "Options",  onClick = OpenOptions },
            new MenuButtonData { label = "Quit", onClick = QuitGame },
        };

        BuildMenu();
    }

    void BuildMenu()
    {
        foreach (MenuButtonData data in menuButtons)
        {
            // Create a copy of the prefab, parented to buttonContainer
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // Set its visible text
            newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = data.label;
            // If you're using TextMeshPro instead, use this line instead of the one above:
            // newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = data.label;

            // Hook up the click action
            newButton.GetComponent<Button>().onClick.AddListener(data.onClick);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenOptions()
    {
        Debug.Log("Open the options menu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
