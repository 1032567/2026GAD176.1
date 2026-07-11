using UnityEngine;

public class XPButton : MonoBehaviour
{
    [Header("── DRAG IN: ProgressionManager from the Hierarchy ─────────────")]
    [Tooltip("The object that has the ProgressionSystem script on it")]
    public ProgressionSystem progression;

    [Header("── Settings ───────────────────────────────────────────────────")]
    [Tooltip("XP added each time this button is pressed")]
    public float xpPerClick = 25f;

    public void OnClick()
    {
        if (progression == null) return;
        progression.GainXP(xpPerClick);
    }
}
