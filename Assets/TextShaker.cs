using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextShaker : MonoBehaviour
{
    private TextMeshProUGUI text;

    public bool start = false;
    private Vector2 startPos;
    public float initialFontSize {  get; private set; }

    private IEnumerator shake;
    [SerializeField] public float constantShakeStrength = 0f;
    [SerializeField] public float shakeMultiplier = 1f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fontSizeIncrease = 1;
    [SerializeField] private AnimationCurve curve;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        initialFontSize = text.fontSize;
        startPos = transform.position;
    }

    private void Update()
    {
        if (start)
        {
            if (shake != null)
            {
                StopCoroutine(shake);
                transform.position = startPos;
            }

            shake = Shake();
            StartCoroutine(shake);
            start = false;
        }
    }

    IEnumerator Shake()
    {
        float elapsedtime = 0f;
        text.fontSize += fontSizeIncrease;

        while (elapsedtime < duration)
        {
            elapsedtime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedtime/duration);
            transform.position = startPos + Random.insideUnitCircle * strength * shakeMultiplier;
            yield return null;
        }
        transform.position = startPos;
        shakeMultiplier += 0.1f;
        constantShakeStrength += 0.005f;
    }
}
