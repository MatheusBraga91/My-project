using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // Reference to the UI Image for the fade effect
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade effect

  
    /// <summary>
    /// Starts the transition to a new scene.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScenes(sceneName));
    }

    /// <summary>
    /// Fades out, switches the scene, and fades back in.
    /// </summary>
    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        // Fade out (fully opaque)
        yield return StartCoroutine(Fade(1f));

        // Load the new scene after fading out
        SceneManager.LoadScene(sceneName);

        // Wait until the new scene is loaded, then fade in
        yield return StartCoroutine(Fade(0f));
    }

    /// <summary>
    /// Fades the screen in or out by adjusting the alpha of the fadeImage.
    /// </summary>
    /// <param name="targetAlpha">The target alpha value (0 for transparent, 1 for opaque).</param>
    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image is not assigned in the SceneTransitionManager!");
            yield break;
        }

        Color color = fadeImage.color;
        float startAlpha = color.a;

        // Gradually adjust the alpha value over time
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the final alpha value is exactly as requested
        color.a = targetAlpha;
        fadeImage.color = color;
    }
}
