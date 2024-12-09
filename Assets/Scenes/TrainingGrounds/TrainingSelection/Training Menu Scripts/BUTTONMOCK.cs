using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // This method could be called when the player presses a "Start Game" button
    public void StartGame()
    {
        // Transition to MockupBattle scene
        SceneManager.LoadScene("MockupBattle");
    }
}
