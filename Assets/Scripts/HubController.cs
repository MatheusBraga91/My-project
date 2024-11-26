using UnityEngine;
using UnityEngine.SceneManagement;

public class HubController : MonoBehaviour
{
    // Method to load the CharacterBuild scene
    public void LoadCharacterBuildScene()
    {
        SceneManager.LoadScene("CharacterBuild");
    }
}
