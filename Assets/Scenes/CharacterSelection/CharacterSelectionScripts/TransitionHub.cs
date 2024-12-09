using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void LoadHubScene()
    {
        SceneManager.LoadScene("Hub"); // Or use the index number if you prefer
    }
}
