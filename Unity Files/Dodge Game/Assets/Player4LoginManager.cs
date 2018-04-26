using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player4LoginManager : MonoBehaviour {

    int numberOfPlayers;

    public GameObject player4Panel;
    public bool p4IsStriker;
    public GameObject p4StrikerCharacter;
    public GameObject p4BlockerCharacter;
    public GameObject p4CharacterRightSelectButton;
    public GameObject p4CharacterLeftSelectButton;
    public UnityEngine.UI.Button p4CharacterSelectButton;
    public GameObject p4NextButton;
    public UnityEngine.UI.Button p4NextScreenButton;

    void Start()
    {
        numberOfPlayers = PlayerLoginManager.numberOfPlayers;
        p4IsStriker = true;

        player4Panel.SetActive(true);

        p4CharacterSelectButton.Select();
        p4CharacterSelectButton.OnSelect(null);
        p4StrikerCharacter.SetActive(true);
        p4BlockerCharacter.SetActive(false);
        p4NextButton.SetActive(false);
    }

    public void Player4CharacterSelect()
    {
        if (p4IsStriker)
        {
            p4IsStriker = false;
            p4StrikerCharacter.SetActive(false);
            p4BlockerCharacter.SetActive(true);
        }

        else
        {
            p4IsStriker = true;
            p4StrikerCharacter.SetActive(true);
            p4BlockerCharacter.SetActive(false);
        }
    }

    public void Player4Ready()
    {
        p4CharacterRightSelectButton.SetActive(false);
        p4CharacterLeftSelectButton.SetActive(false);
        p4NextButton.SetActive(true);
        p4NextScreenButton.Select();
    }

    public void StartGame()
    {
        Debug.Log(numberOfPlayers);
        SceneManager.LoadScene("Level1");
    }
}
