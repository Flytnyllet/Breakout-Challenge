using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ScoreMultiplier")]
public class PowerupScoreMultiplier : PowerupEffect
{
    public int amount;

    public override void Apply(GameObject target)
    {
        UIManager.Instance.scoreMultiplier *= amount;
    }
}
