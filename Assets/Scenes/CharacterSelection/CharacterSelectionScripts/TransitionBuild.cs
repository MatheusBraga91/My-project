using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition1 : MonoBehaviour
{
    public void LoadSkillSelectionScene()
    {
        SceneManager.LoadScene("SkillSelection"); // Or use the index number if you prefer
    }
}
