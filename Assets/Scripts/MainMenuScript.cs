using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//there are two buttons in main menu this script will control that buttons
public class MainMenuScript : MonoBehaviour
{
    public string gameScene;
    public void onClick()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void FunForQuit(){
        Debug.Log("Please quit");
        Application.Quit();
    }
}
