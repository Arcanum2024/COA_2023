using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Panel;
    public string gameSceneName = "GameScene";
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }
    public void ExitReg()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
