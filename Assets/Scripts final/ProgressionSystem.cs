using UnityEngine;

// shared foundation - both progression types inherit from this
public abstract class ProgressionBase
{
    protected float currentXP;
    protected float target;
    protected int   level;

    public abstract void    GainXP(float amount);
    protected abstract void LevelUp();
}

public class LinearProgression : ProgressionBase
{
    float[] thresholds;

    public LinearProgression(float[] thresholds)
    {
        this.thresholds = thresholds;
        level           = 1;
        target          = thresholds[0]; // set first target on startup
    }

    public override void GainXP(float amount)
    {
        currentXP += amount;

        // loop handles multiple level-ups from one XP gain
        while (currentXP >= target)
        {
            currentXP -= target;
            LevelUp();
        }
    }

    public int   GetLevel()     => level;
    public float GetCurrentXP() => currentXP;
    public float GetTarget()    => target;

    protected override void LevelUp()
    {
        level++;
        int i  = level - 1;
        target = i < thresholds.Length ? thresholds[i] : thresholds[thresholds.Length - 1];
        Debug.Log("Linear level up: " + level);
    }
}

public class ProgressionSystem : MonoBehaviour
{
    public float[] xpList = { 100, 200, 350, 500, 700 }; // Inspector: one value per level

    LinearProgression linear;

    void Awake()
    {
        // build pool at startup
        linear = new LinearProgression(xpList);
    }

    public void  GainXP(float amount) => linear.GainXP(amount);
    public int   GetLevel()           => linear.GetLevel();
    public float GetCurrentXP()       => linear.GetCurrentXP();
    public float GetTarget()          => linear.GetTarget();
}
