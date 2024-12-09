using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour
{
    public void LoadCharacterBuildScene()
    {
        SceneManager.LoadScene("CharacterBuild"); // Or use the index number if you prefer
    }
}
