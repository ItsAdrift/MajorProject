using UnityEngine;
using UnityEngine.UI;

public class UIImageSwapper : MonoBehaviour
{
    public Image imageComponent;
    public Sprite secondarySprite;
    public float fadeDuration = 1.0f;

    private Image secondaryImage;
    private float fadeTimer = 0f;

    private void Start()
    {
        secondaryImage = Instantiate(imageComponent, imageComponent.transform);
        secondaryImage.color = new Color(1f, 1f, 1f, 0f);
        secondaryImage.sprite = secondarySprite;
        secondaryImage.transform.SetParent(imageComponent.transform, false);

        imageComponent.color = new Color(1f, 1f, 1f, 1f);
        secondaryImage.color = new Color(1f, 1f, 1f, 0f);
    }

    private void Update()
    {
        fadeTimer += Time.deltaTime;

        // Calculate the alpha values for cross-fading
        float currentAlpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
        float secondaryAlpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);

        imageComponent.color = new Color(1f, 1f, 1f, currentAlpha);
        secondaryImage.color = new Color(1f, 1f, 1f, secondaryAlpha);

        // When the timer reaches the fade duration, swap the images
        /*if (fadeTimer >= fadeDuration)
        {
            SwapImages();
            fadeTimer = 0f;
        }
         */
    }

    public void SwapImagesWithFade()
    {
        Image temp = imageComponent;
        imageComponent = secondaryImage;
        secondaryImage = temp;
    }
}
