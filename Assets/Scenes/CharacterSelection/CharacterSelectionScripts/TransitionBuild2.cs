using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition5 : MonoBehaviour
{
    public void LoadSkillSelectionScene()
    {
        SceneManager.LoadScene("PowerSkillSelection"); // Or use the index number if you prefer
    }
}
