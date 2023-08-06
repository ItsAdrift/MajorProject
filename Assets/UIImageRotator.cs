using UnityEngine;
using UnityEngine.UI;

public class UIImageRotator : MonoBehaviour
{
    public Image imageComponent;
    public Sprite[] sprites;
    public float fadeDuration = 1.0f;

    private int currentIndex = 0;
    private Image currentImage;
    private Image nextImage;
    private float fadeTimer = 0f;

    private void Start()
    {
        if (imageComponent == null)
            imageComponent = GetComponent<Image>();

        // Set the initial sprite
        imageComponent.sprite = sprites[currentIndex];

        // Create a clone of the UI Image for cross-fading
        currentImage = Instantiate(imageComponent, imageComponent.transform);
        currentImage.color = new Color(1f, 1f, 1f, 1f);
        currentImage.transform.SetAsFirstSibling();

        // Create another clone for the next image
        nextImage = Instantiate(imageComponent, imageComponent.transform);
        nextImage.color = new Color(1f, 1f, 1f, 0f);

        UpdateImages();
    }

    private void Update()
    {
        fadeTimer += Time.deltaTime;

        // Cross-fade when the timer reaches the fade duration
        if (fadeTimer >= fadeDuration)
        {
            currentIndex = (currentIndex + 1) % sprites.Length;
            UpdateImages();
            fadeTimer = 0f;
        }

        // Calculate the alpha values for cross-fading
        float currentAlpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
        float nextAlpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);

        currentImage.color = new Color(1f, 1f, 1f, currentAlpha);
        nextImage.color = new Color(1f, 1f, 1f, nextAlpha);
    }

    private void UpdateImages()
    {
        currentImage.sprite = imageComponent.sprite;
        nextImage.sprite = sprites[currentIndex];
    }
}
