using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

    public GameObject titleCanvas;
    public GameObject playerLoginCanvas;

    void Start()
    {
        titleCanvas.SetActive(true);
        //playerLoginCanvas.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Player_Load_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
