using UnityEngine;
using UnityEngine.UI;

public class UIImageRotator : MonoBehaviour
{
    public Image imageComponent;
    public Sprite[] sprites;
    public float fadeDuration = 1.0f;
    public float displayDuration = 3.0f; // Time to display each image before cross-fading

    private int currentIndex = 0;
    private Image nextImage;
    private float fadeTimer = 0f;
    private float displayTimer = 0f;
    private bool isFading = false;

    private void Start()
    {
        nextImage = Instantiate(imageComponent, imageComponent.transform);
        nextImage.color = new Color(1f, 1f, 1f, 0f);
        nextImage.transform.SetParent(imageComponent.transform, false);

        UpdateImages();
    }

    private void Update()
    {
        if (!isFading)
        {
            displayTimer += Time.deltaTime;

            if (displayTimer >= displayDuration)
            {
                isFading = true;
                displayTimer = 0f;
            }
        }
        else
        {
            fadeTimer += Time.deltaTime;

            // Cross-fade when the timer reaches the fade duration
            if (fadeTimer >= fadeDuration)
            {
                currentIndex = (currentIndex + 1) % sprites.Length;
                UpdateImages();
                fadeTimer = 0f;
                isFading = false;
            }

            // Calculate the alpha values for cross-fading
            float nextAlpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);

            nextImage.color = new Color(1f, 1f, 1f, nextAlpha);
        }
    }

    private void UpdateImages()
    {
        imageComponent.sprite = sprites[currentIndex];
        nextImage.sprite = sprites[(currentIndex + 1) % sprites.Length];
        nextImage.color = new Color(1f, 1f, 1f, 0f);
    }
}
