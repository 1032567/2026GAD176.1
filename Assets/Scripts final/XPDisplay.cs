using UnityEngine;
using TMPro;

public class XPDisplay : MonoBehaviour
{
    [Header("── DRAG IN: ProgressionManager from the Hierarchy ─────────────")]
    [Tooltip("The object that has the ProgressionSystem script on it")]
    public ProgressionSystem progression;

    [Header("── DRAG IN: TMP Text objects from your Canvas ──────────────────")]
    [Tooltip("Displays  Level: X")]
    public TMP_Text levelText;
    [Tooltip("Displays  currentXP / targetXP")]
    public TMP_Text xpText;
    [Tooltip("Blank during linear levels - shows overload info after the cap")]
    public TMP_Text overloadText;

    void Start()
    {
        if (progression == null) return;
        progression.onLevelUp.AddListener(OnLevelUp); // subscribe to the event
        Refresh();
    }

    void Update()
    {
        // Update so XP text changes on every click, not just on level-up
        if (progression != null)
            Refresh();
    }

    void OnLevelUp()
    {
        Debug.Log("UI received level-up event");
    }

    void Refresh()
    {
        if (levelText != null)
            levelText.text = "Level: " + progression.GetLevel();

        if (xpText != null)
            xpText.text =
                Mathf.RoundToInt(progression.GetCurrentXP()) + " / " +
                Mathf.RoundToInt(progression.GetTarget()) + " XP";

        if (overloadText != null)
            overloadText.text = progression.IsAtCap()
                ? "Overload " + progression.GetOverloadLevel() +
                  "  |  "     + Mathf.RoundToInt(progression.GetOverloadXP()) +
                  " / "       + Mathf.RoundToInt(progression.GetOverloadTarget()) + " XP"
                : "";
    }
}
