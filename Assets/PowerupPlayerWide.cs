using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PlayerWide")]
public class PowerupPlayerWide : PowerupEffect
{
    [SerializeField] float scaleIncreaseInX;

    public override void Apply(GameObject target)
    {
        Debug.Log("Wide Powerup triggered");
        var sr = target.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale;
        sr.Set(sr.x * scaleIncreaseInX, sr.y, sr.z);
        target.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = sr;
    }
}
