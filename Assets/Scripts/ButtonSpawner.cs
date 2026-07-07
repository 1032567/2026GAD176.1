using UnityEngine;
using UnityEngine.UI; // Required for the Button component
using TMPro;          // Required if your button uses TextMeshPro text

public class ButtonSpawner : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button buttonPrefab; // Drag your button prefab here
    [SerializeField] private Transform buttonParent; // Drag your Canvas or Layout Group here

    void Start()
    {
        // Example: Spawning 5 dynamic buttons
        for (int i = 1; i <= 5; i++)
        {
            CreateDynamicButton(i);
        }
    }

    void CreateDynamicButton(int index)
    {
        // 1. Instantiate the button prefab
        Button newButton = Instantiate(buttonPrefab);

        // 2. Set parent to your UI container without modifying world positions
        newButton.transform.SetParent(buttonParent, false);

        // 3. Optional: Change the button's label text
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = "Button #" + index;
        }

        // 4. Capture the current loop variable to avoid closure issues
        int capturedIndex = index;

        // 5. Assign functionality dynamically
        newButton.onClick.AddListener(() => OnDynamicButtonClick(capturedIndex));
    }

    void OnDynamicButtonClick(int id)
    {
        Debug.Log("You clicked button number: " + id);
    }
}
