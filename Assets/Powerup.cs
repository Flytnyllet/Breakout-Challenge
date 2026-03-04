using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    [SerializeField] private float fallSpeed = -3f;

    private void Update()
    {
        transform.Translate(Vector2.up * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            powerupEffect.Apply(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
