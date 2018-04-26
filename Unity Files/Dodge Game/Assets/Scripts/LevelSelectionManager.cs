using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour {

    public GameObject levelSelectionCanvas;
    public GameObject roundSelectionCanvas;

    public static int numberOfRounds = 1;
    public Text numberOfRoundsText;

    public GameObject level1Button;
    public UnityEngine.UI.Button level1SelectionButton;

    void Start()
    {
        levelSelectionCanvas.SetActive(false);
        roundSelectionCanvas.SetActive(true);
    }

    void Update()
    {
        numberOfRoundsText.text = numberOfRounds.ToString();
        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetMaxRounds(numberOfRounds);

    }

    public void MoreRounds()
    {
        numberOfRounds++;

        if (numberOfRounds > 15)
        {
            numberOfRounds = 1;
        }

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetMaxRounds(numberOfRounds);
    }

    public void LessRounds()
    {
        numberOfRounds--;

        if (numberOfRounds < 1)
        {
            numberOfRounds = 15;
        }

        GameObject.Find("GameManager").GetComponent<ManagerScript>().SetMaxRounds(numberOfRounds);
    }

    public void Next()
    {
        levelSelectionCanvas.SetActive(true);
        roundSelectionCanvas.SetActive(false);
        level1SelectionButton.Select();
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Test_Level_2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Test_Level_3");
    }
}
