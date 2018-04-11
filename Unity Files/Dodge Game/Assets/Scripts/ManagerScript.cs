using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

    // the manager script will be used to check if all players are out
    // and reset if on team is out
    public int gameScore = 0;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    List<GameObject> team1Players = new List<GameObject>();
    int sizeOfTeam1 = 0;

    List<GameObject> team2Players = new List<GameObject>();
    int sizeOfTeam2 = 0;

    List<GameObject> ballList = new List<GameObject>();
    int sizeOfBalls = 0;

    bool canCheck = true;

    // Use this for initialization
    void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player3 = GameObject.FindGameObjectWithTag("Player3");
        player4 = GameObject.FindGameObjectWithTag("Player4");

        SetTeams();
    }

    // Update is called once per frame
    void Update () {
        if(canCheck)
        {
            CheckTeamOne();
            CheckTeamTwo();
        }
	}

    void SetTeams()
    {

        if (player1.GetComponent<PlayerMovement>().team == 1)
        {
            team1Players.Add(player1);
            sizeOfTeam1++;
        }
        else
        {
            team2Players.Add(player1);
            sizeOfTeam2++;
        }

        if (player2.GetComponent<PlayerMovement>().team == 1)
        {
            team1Players.Add(player2);
            sizeOfTeam1++;
        }
        else
        {
            team2Players.Add(player2);
            sizeOfTeam2++;
        }

        if (player3.GetComponent<PlayerMovement>().team == 1)
        {
            team1Players.Add(player3);
            sizeOfTeam1++;
        }
        else
        {
            team2Players.Add(player3);
            sizeOfTeam2++;
        }

        if (player4.GetComponent<PlayerMovement>().team == 1)
        {
            team1Players.Add(player4);
            sizeOfTeam1++;
        }
        else
        {
            team2Players.Add(player4);
            sizeOfTeam2++;
        }

    }

    void CheckTeamOne()
    {
        int teamMemebersOut = 0;
        int totalTeamMembers = sizeOfTeam1;
        Debug.Log("Team 1 amount: " + totalTeamMembers);

        for (int i = 0; i < totalTeamMembers; i++)
        {
            if(team1Players[i].GetComponent<PlayerMovement>().isOut)
                teamMemebersOut++;

            if(teamMemebersOut >= totalTeamMembers)
            {
                // appear congratulations to the winning team, increase score, reset
                canCheck = false;
                Debug.Log("Team 2 Wins!");
                StartCoroutine(WaitTime());
            }
        }
    }

    void CheckTeamTwo()
    {
        int teamMemebersOut = 0;
        int totalTeamMembers = sizeOfTeam2;
        Debug.Log("Team 2 amount: " + totalTeamMembers);

        for (int i = 0; i < totalTeamMembers; i++)
        {
            if (team2Players[i].GetComponent<PlayerMovement>().isOut)
                teamMemebersOut++;

            if (teamMemebersOut >= totalTeamMembers)
            {
                // appear congratulations to the winning team, increase score, reset
                canCheck = false;
                Debug.Log("Team1 Wins!");
                StartCoroutine(WaitTime());
            }
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(3.5f);
        ResetGame();
    }

    void ResetGame()
    {
        for (int i = 0; i < sizeOfTeam1; i++)
        {
            // set the positions
            team1Players[i].GetComponent<PlayerMovement>().ResetPlayer();
        }

        for (int i = 0; i < sizeOfTeam2; i++)
        {
            team2Players[i].GetComponent<PlayerMovement>().ResetPlayer();
        }

        int amountOfBalls = 4;

        for (int i = 1; i <= amountOfBalls; i++)
        {
            GameObject.Find("Ball" + i).GetComponent<BallScript>().ResetPos();
        }

        canCheck = true;
    }
}
