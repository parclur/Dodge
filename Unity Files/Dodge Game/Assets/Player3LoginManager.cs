using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player3LoginManager : MonoBehaviour {

    int numberOfPlayers;

    public GameObject player3Panel;
    public bool p3IsStriker;
    public GameObject p3StrikerCharacter;
    public GameObject p3BlockerCharacter;
    public GameObject p3CharacterRightSelectButton;
    public GameObject p3CharacterLeftSelectButton;
    public UnityEngine.UI.Button p3CharacterSelectButton;
    public GameObject p3NextButton;
    public UnityEngine.UI.Button p3NextScreenButton;

    void Start()
    {
        numberOfPlayers = PlayerLoginManager.numberOfPlayers;
        p3IsStriker = true;

        player3Panel.SetActive(true);

        p3CharacterSelectButton.Select();
        p3CharacterSelectButton.OnSelect(null);
        p3StrikerCharacter.SetActive(true);
        p3BlockerCharacter.SetActive(false);
        p3NextButton.SetActive(false);
    }

    public void Player3CharacterSelect()
    {
        if (p3IsStriker)
        {
            p3IsStriker = false;
            p3StrikerCharacter.SetActive(false);
            p3BlockerCharacter.SetActive(true);
        }

        else
        {
            p3IsStriker = true;
            p3StrikerCharacter.SetActive(true);
            p3BlockerCharacter.SetActive(false);
        }
    }

    public void Player3Ready()
    {
        p3CharacterRightSelectButton.SetActive(false);
        p3CharacterLeftSelectButton.SetActive(false);
        p3NextButton.SetActive(true);
        p3NextScreenButton.Select();
    }

    public void StartGame()
    {
        Debug.Log(numberOfPlayers);
        if (numberOfPlayers == 3)
        {
            SceneManager.LoadScene("Level1");
        }

        if (numberOfPlayers > 3)
        {
            SceneManager.LoadScene("Player_4_Login_Scene");
        }
    }
}
