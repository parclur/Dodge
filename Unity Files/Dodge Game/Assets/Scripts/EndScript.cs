using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {

	public void RestartGame()
    {
        SceneManager.LoadScene("Start_Menu");
    }
}
