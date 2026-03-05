using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool startCameraShake = false;
    private Vector3 cameraStartPos;

    private IEnumerator screenShakeCoroutine;
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve curve;

    private void Awake()
    {
        cameraStartPos = transform.position;
    }

    private void Update()
    {
        if (startCameraShake)
        {
            if (screenShakeCoroutine != null)
            {
                StopCoroutine(screenShakeCoroutine);
                transform.position = cameraStartPos;
            }

            screenShakeCoroutine = ScreenShakeCoroutine();
            StartCoroutine(screenShakeCoroutine);
            startCameraShake = false;
        }
    }

    IEnumerator ScreenShakeCoroutine()
    {
        float elapsedtime = 0f;

        while (elapsedtime < duration)
        {
            elapsedtime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedtime / duration);
            transform.position = cameraStartPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = cameraStartPos;
    }
}
