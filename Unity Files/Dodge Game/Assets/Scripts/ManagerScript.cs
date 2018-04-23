using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour {

    // the manager script will be used to check if all players are out
    // and reset if on team is out
    public int gameRound = 1;
    public int maxRounds = 7;

    public int team1Score = 0;
    public int team2Score = 0;

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

        if(GetComponent<UIManager>())
        {
        GetComponent<UIManager>().SetBlueTeamNum(team1Score);
        GetComponent<UIManager>().SetRedTeamNum(team2Score);
        GetComponent<UIManager>().SetRoundNum(gameRound);
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

        for (int i = 0; i < totalTeamMembers; i++)
        {
            if(team1Players[i].GetComponent<PlayerMovement>().isOut)
                teamMemebersOut++;

            if(teamMemebersOut >= totalTeamMembers)
            {
                // appear congratulations to the winning team, increase score, reset
                canCheck = false;
                Debug.Log("Team2 Wins Round " + gameRound + "!");

                if (GetComponent<UIManager>())
                {
                    GetComponent<UIManager>().EnableRoundEndText("Team2 Wins Round " + gameRound + "!");
                }

                team2Score++;
                Debug.Log("Team 1 score: " + team1Score + " vs " + "Team 2 score: " + team2Score);
                StartCoroutine(WaitInBetweenMatch());
            }
        }
    }

    void CheckTeamTwo()
    {
        int teamMemebersOut = 0;
        int totalTeamMembers = sizeOfTeam2;

        for (int i = 0; i < totalTeamMembers; i++)
        {
            if (team2Players[i].GetComponent<PlayerMovement>().isOut)
                teamMemebersOut++;

            if (teamMemebersOut >= totalTeamMembers)
            {
                // appear congratulations to the winning team, increase score, reset
                canCheck = false;
                Debug.Log("Team1 Wins Round " + gameRound + "!");

                if (GetComponent<UIManager>())
                {
                    GetComponent<UIManager>().EnableRoundEndText("Blue Team Wins Round " + gameRound + "!");
                }

                team1Score++;
                Debug.Log("Team 1 score: " + team1Score + " vs " + "Team 2 score: " + team2Score);
                StartCoroutine(WaitInBetweenMatch());
            }
        }
    }

    IEnumerator WaitInBetweenMatch()
    {
        yield return new WaitForSeconds(3.5f);

        if (GetComponent<UIManager>())
        {
            GetComponent<UIManager>().DisableRoundEndText();
        }

        ResetMatch();
    }

    void ResetMatch()
    {
        gameRound++;

        if(gameRound > maxRounds)
        {
            ResetGame();
        }
        else
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

    void ResetGame()
    {
        if(team1Score > team2Score)
        {
            Debug.Log("Team1 Wins " + team1Score + " to " + team2Score + "!");

            if (GetComponent<UIManager>())
            {
                GetComponent<UIManager>().EnableRoundEndText("Team1 Wins " + team1Score + " to " + team2Score + "!");
            }

        }
        else
        {
            Debug.Log("Team2 Wins " + team2Score + " to " + team1Score + "!");

            if (GetComponent<UIManager>())
            {
                GetComponent<UIManager>().EnableRoundEndText("Team2 Wins " + team2Score + " to " + team1Score + "!");
            }
        }

        StartCoroutine(GoBackToMenu());
    }

    IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(4);
        // go back to the menu
        SceneManager.LoadScene("End_Scene");
    }
}
