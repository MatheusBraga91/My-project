using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition10 : MonoBehaviour
{
    public void LoadSkillSelectionScene()
    {
        SceneManager.LoadScene("SpeedSkillSelection"); // Or use the index number if you prefer
    }
}
