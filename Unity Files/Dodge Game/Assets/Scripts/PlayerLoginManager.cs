using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerLoginManager : MonoBehaviour {

    public static int numberOfPlayers = 0;
    public static int p1CharacterClass = 0;

    public GameObject playerNumCanvas;
    public GameObject playerLoginCanvas;

    public GameObject player1Panel;
    public bool p1IsStriker;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    public GameObject p1CharacterRightSelectButton;
    public GameObject p1CharacterLeftSelectButton;
    public UnityEngine.UI.Button p1CharacterSelectButton;
    public GameObject p1NextButton;
    public UnityEngine.UI.Button p1NextScreenButton;

    public GameObject player2Panel;
    public GameObject player3Panel;
    public GameObject player4Panel;

    void Start()
    {
        numberOfPlayers = 0;
        playerNumCanvas.SetActive(true);
        playerLoginCanvas.SetActive(false);
        p1IsStriker = true;
        p1CharacterClass = 0;
    }

    public void SinglePlayer()
    {
        numberOfPlayers = 1;
        playerNumCanvas.SetActive(false);
        playerLoginCanvas.SetActive(true);
        player1Panel.SetActive(true);
        player2Panel.SetActive(false);
        player3Panel.SetActive(false);
        player4Panel.SetActive(false);

        p1CharacterSelectButton.Select();
        p1CharacterSelectButton.OnSelect(null);
        p1StrikerCharacter.SetActive(true);
        p1BlockerCharacter.SetActive(false);
        p1NextButton.SetActive(false);
    }

    public void Player1CharacterSelect()
    {
        if (p1IsStriker)
        {
            p1IsStriker = false;
            p1CharacterClass = 0;
            p1StrikerCharacter.SetActive(false);
            p1BlockerCharacter.SetActive(true);
        }

        else
        {
            p1IsStriker = true;
            p1CharacterClass = 1;
            p1StrikerCharacter.SetActive(true);
            p1BlockerCharacter.SetActive(false);
        }
    }

    public void Player1Ready()
    {
        p1CharacterRightSelectButton.SetActive(false);
        p1CharacterLeftSelectButton.SetActive(false);
        p1NextButton.SetActive(true);
        p1NextScreenButton.Select();
    }

    public void TwoPlayer()
    {
        numberOfPlayers = 2;
        playerNumCanvas.SetActive(false);
        playerLoginCanvas.SetActive(true);
        player1Panel.SetActive(true);
        player2Panel.SetActive(true);
        player3Panel.SetActive(false);
        player4Panel.SetActive(false);

        p1CharacterSelectButton.Select();
        p1CharacterSelectButton.OnSelect(null);
        p1StrikerCharacter.SetActive(true);
        p1BlockerCharacter.SetActive(false);
        p1NextButton.SetActive(false);
    }

    public void ThreePlayer()
    {
        numberOfPlayers = 3;
        playerNumCanvas.SetActive(false);
        playerLoginCanvas.SetActive(true);
        player1Panel.SetActive(true);
        player2Panel.SetActive(true);
        player3Panel.SetActive(true);
        player4Panel.SetActive(false);

        p1CharacterSelectButton.Select();
        p1CharacterSelectButton.OnSelect(null);
        p1StrikerCharacter.SetActive(true);
        p1BlockerCharacter.SetActive(false);
        p1NextButton.SetActive(false);
    }

    public void FourPlayer()
    {
        numberOfPlayers = 4;
        playerNumCanvas.SetActive(false);
        playerLoginCanvas.SetActive(true);
        player1Panel.SetActive(true);
        player2Panel.SetActive(true);
        player3Panel.SetActive(true);
        player4Panel.SetActive(true);

        p1CharacterSelectButton.Select();
        p1CharacterSelectButton.OnSelect(null);
        p1StrikerCharacter.SetActive(true);
        p1BlockerCharacter.SetActive(false);
        p1NextButton.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log(numberOfPlayers);
        if (numberOfPlayers == 1)
        {
            SceneManager.LoadScene("Level_Select");
        }

        if (numberOfPlayers >= 2)
        {
            SceneManager.LoadScene("Player_2_Login_Scene");
        }
    }
}
