using UnityEngine;
using UnityEngine.UI;

public class HubController : MonoBehaviour
{
    // Reference to the SceneTransitionManager
    [SerializeField] private SceneTransitionManager sceneTransitionManager;

    // Reference to the Button
    [SerializeField] private Button sceneTransitionButton;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the button has an onClick listener
        if (sceneTransitionButton != null)
        {
            sceneTransitionButton.onClick.AddListener(OnSceneTransitionButtonClick);
        }
        else
        {
            Debug.LogError("Scene Transition Button is not assigned in HubController!");
        }
    }

    // Method to load the CharacterBuild scene with a smooth transition
    private void OnSceneTransitionButtonClick()
    {
        // Use the SceneTransitionManager to transition to the CharacterBuild scene
        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.TransitionToScene("CharacterBuild");
        }
        else
        {
            Debug.LogError("SceneTransitionManager not assigned in HubController!");
        }
    }
}
