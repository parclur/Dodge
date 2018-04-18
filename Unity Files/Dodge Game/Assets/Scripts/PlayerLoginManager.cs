using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLoginManager : MonoBehaviour {
    /*
    public int playerId; // because I will always have 4 player select panels on the screen, I just assign the id in the inspector.
    //private Player player;

    bool mPlayer1Ready, mPlayer2Ready;

    void Start()
    {
        mPlayer1Ready = false;
        mPlayer2Ready = false;

        //player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (Input.GetButtonDown(playerId + "Submit"))
        {
            //  Do something cool
        }

        if (mPlayer1Ready && mPlayer2Ready)
        {

        }
    }

    //waitingToJoin
    //nameSelect
    //colorSelect
    //ready
    */

    public GameObject player1JoinButton;
    public bool player1Join;
    public GameObject player1Panel;
    public bool p1IsStriker;
    public GameObject p1StrikerCharacter;
    public GameObject p1BlockerCharacter;
    public UnityEngine.UI.Button p1CharacterSelectButton;

    string player2Hor;
    string player2Ver;
    string player2Confirm;

    public GameObject player2JoinButton;
    public bool player2Join;
    public GameObject player2Panel;
    public bool p2IsStriker;
    public GameObject p2StrikerCharacter;
    public GameObject p2BlockerCharacter;
    public UnityEngine.UI.Button p2JoinButton;
    public UnityEngine.UI.Button p2CharacterSelectButton;

    void Start()
    {
        player1Join = false;
        player1JoinButton.SetActive(true);
        player1Panel.SetActiveRecursively(false);
        p1IsStriker = true;

        player2Hor = "P2LSH";
        player2Ver = "P2LSV";
        player2Confirm = "P2A";

        p2JoinButton.Select();
        player2Join = false;
        player2JoinButton.SetActive(true);
        player2Panel.SetActiveRecursively(false);
        p2IsStriker = true;
    }

    void Update()
    {
        //player2Join = Input.GetAxis(player2Confirm);

        if (Input.GetButton(player2Confirm))
        {
            Player2Join();
        }
    }

    public void Player1Join()
    {
        player1Join = true;
        player1JoinButton.SetActive(false);
        player1Panel.SetActiveRecursively(true);
        p1CharacterSelectButton.Select();
        p1StrikerCharacter.SetActive(true);
        p1BlockerCharacter.SetActive(false);
    }

    public void Player1CharacterSelect()
    {
        if (p1IsStriker)
        {
            p1IsStriker = false;
            p1StrikerCharacter.SetActive(false);
            p1BlockerCharacter.SetActive(true);
        }

        else
        {
            p1IsStriker = true;
            p1StrikerCharacter.SetActive(true);
            p1BlockerCharacter.SetActive(false);
        }
    }

    public void Player2Join()
    {
        player2Join = true;
        player2JoinButton.SetActive(false);
        player2Panel.SetActiveRecursively(true);
        p2CharacterSelectButton.Select();
        p2StrikerCharacter.SetActive(true);
        p2BlockerCharacter.SetActive(false);
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

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
