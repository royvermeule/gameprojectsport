using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
