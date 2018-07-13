using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour {

    public void LoadDemoLevel()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    public void LoadMainMenu()
    {
//        Debug.Log("MainMenuLoad");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
