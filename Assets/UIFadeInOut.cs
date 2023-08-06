using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeInOut : MonoBehaviour
{
    public TMP_Text textElement;
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f;
    public float fadeInDelay = 1.0f;
    public float fadeOutDelay = 1.0f;

    private float fadeTimer = 0f;
    private float delayTimer = 0f;
    private bool isFadingIn = true;

    private void Start()
    {
        if (textElement == null)
        {
            textElement = GetComponent<TMP_Text>();
        }
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= (isFadingIn ? fadeInDelay : fadeOutDelay))
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / (isFadingIn ? fadeInDuration : fadeOutDuration));
            if (!isFadingIn)
            {
                alpha = 1 - alpha;
            }

            Color newColor = textElement.color;
            newColor.a = alpha;
            textElement.color = newColor;

            if (fadeTimer >= (isFadingIn ? fadeInDuration : fadeOutDuration))
            {
                fadeTimer = 0f;
                delayTimer = 0f;
                isFadingIn = !isFadingIn;
            }
        }
    }
}
