using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesManager : MonoBehaviour
{

    [SerializeField] private CharacterMenu characterMenu;
    public void StartGame()
    {
        ResetTimeScale();
        characterMenu.SendCustomInfo();
        SceneManager.LoadScene(1);
        GameManager.instance.UpdateGameState(GameState.Gameplay);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(int index)
    {
        ResetTimeScale();
        SceneManager.LoadScene(index);
    }

    public void Restart()
    {
        ResetTimeScale();
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
        GameManager.instance.ResetTrackingStats();
    }
    public void LoadMenu()
    {
        ResetTimeScale();
        SceneManager.LoadScene(0);
        GameManager.instance.UpdateGameState(GameState.MainMenu);
    }
    private void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
    private void FreezTime()
    {
        Time.timeScale = 0;
    }

}

