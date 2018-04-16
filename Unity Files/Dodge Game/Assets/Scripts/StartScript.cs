using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Player_Login_Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
