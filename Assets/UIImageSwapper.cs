using UnityEngine;
using UnityEngine.UI;

public class UIImageSwapper : MonoBehaviour
{
    public Image imageComponent;
    public Sprite secondarySprite;
    public float currentFadeDuration = 0.35f;
    public float fadeDuration = 1.0f;

    private Image secondaryImage;
    private bool isSwapping = false;
    private float fadeTimer = 0f;

    private void Start()
    {
        secondaryImage = transform.GetChild(1).GetComponent<Image>(); // Assumes the secondary image is the first child

        imageComponent.color = new Color(1f, 1f, 1f, 1f);
        secondaryImage.color = new Color(1f, 1f, 1f, 0f);
        secondaryImage.sprite = secondarySprite;
    }

    private void Update()
    {
        if (isSwapping)
        {
            fadeTimer += Time.deltaTime;

            // Calculate the alpha values for cross-fading
            float currentAlpha = Mathf.Lerp(1f, 0f, fadeTimer / currentFadeDuration);
            float secondaryAlpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);

            imageComponent.color = new Color(1f, 1f, 1f, currentAlpha);
            secondaryImage.color = new Color(1f, 1f, 1f, secondaryAlpha);

            // When the timer reaches the fade duration, complete the swap
            if (fadeTimer >= fadeDuration)
            {
                isSwapping = false;
                fadeTimer = 0f;

                // Swap images
                Sprite tempSprite = imageComponent.sprite;
                imageComponent.sprite = secondarySprite;
                secondarySprite = tempSprite;
            }
        }
    }

    public void SwapImagesWithFade()
    {
        if (!isSwapping)
        {
            isSwapping = true;
        }
    }
}