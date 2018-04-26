using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2LoginManager : MonoBehaviour {

    int numberOfPlayers;

    public GameObject player2Panel;
    public bool p2IsStriker;
    public GameObject p2StrikerCharacter;
    public GameObject p2BlockerCharacter;
    public GameObject p2CharacterRightSelectButton;
    public GameObject p2CharacterLeftSelectButton;
    public UnityEngine.UI.Button p2CharacterSelectButton;
    public GameObject p2NextButton;
    public UnityEngine.UI.Button p2NextScreenButton;

    void Start()
    {
        numberOfPlayers = PlayerLoginManager.numberOfPlayers;
        p2IsStriker = true;

        player2Panel.SetActive(true);

        p2CharacterSelectButton.Select();
        p2CharacterSelectButton.OnSelect(null);
        p2StrikerCharacter.SetActive(true);
        p2BlockerCharacter.SetActive(false);
        p2NextButton.SetActive(false);
    }

    public void Player2CharacterSelect()
    {
        if (p2IsStriker)
        {
            p2IsStriker = false;
            p2StrikerCharacter.SetActive(false);
            p2BlockerCharacter.SetActive(true);
        }

        else
        {
            p2IsStriker = true;
            p2StrikerCharacter.SetActive(true);
            p2BlockerCharacter.SetActive(false);
        }
    }

    public void Player2Ready()
    {
        p2CharacterRightSelectButton.SetActive(false);
        p2CharacterLeftSelectButton.SetActive(false);
        p2NextButton.SetActive(true);
        p2NextScreenButton.Select();
    }
    
    public void StartGame()
    {
        Debug.Log(numberOfPlayers);
        if (numberOfPlayers == 2)
        {
            SceneManager.LoadScene("Level1");
        }

        if (numberOfPlayers > 2)
        {
            SceneManager.LoadScene("Player_3_Login_Scene");
        }
    }
}
