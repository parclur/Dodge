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

    public int player1Kills;
    public int player2Kills;
    public int player3Kills;
    public int player4Kills;

    public int player1Deaths;
    public int player2Deaths;
    public int player3Deaths;
    public int player4Deaths;

    int player1Class;
    int player2Class;
    int player3Class;
    int player4Class;

    List<GameObject> team1Players = new List<GameObject>();
    int sizeOfTeam1 = 0;

    List<GameObject> team2Players = new List<GameObject>();
    int sizeOfTeam2 = 0;

    List<GameObject> ballList = new List<GameObject>();
    int sizeOfBalls = 0;

    bool canCheck = true;

    bool onLevel = false;
    bool endofRound = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        CheckPlayer();

    }

    // Update is called once per frame
    void Update () {

        CheckPlayer();

        if (SceneManager.GetActiveScene().name == "End_Scene")
        {
            canCheck = false;
            Debug.Log("setting player stats");
            Debug.Log("Player1 kills: " + player1Kills);
            GameObject.Find("End_Manager").GetComponent<EndSceneUIData>().SetKills(player1Kills, player2Kills, player3Kills, player4Kills);
            GameObject.Find("End_Manager").GetComponent<EndSceneUIData>().SetDeaths(player1Deaths, player2Deaths, player3Deaths, player4Deaths);

            if (team1Score > team2Score)
            {
                GameObject.Find("End_Manager").GetComponent<EndSceneUIData>().SetWinningText("Blue Team Wins " + team1Score + " to " + team2Score + "!");
            }
            else
            {
                GameObject.Find("End_Manager").GetComponent<EndSceneUIData>().SetWinningText("Red Team Wins " + team2Score + " to " + team1Score + "!");
            }
        }

        if (canCheck)
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

    void CheckPlayer()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Test_Level_2"
            || SceneManager.GetActiveScene().name == "Test_Level_3" || SceneManager.GetActiveScene().name == "Test_Level_4")
        {
            player1 = GameObject.FindGameObjectWithTag("Player1");
            player2 = GameObject.FindGameObjectWithTag("Player2");
            player3 = GameObject.FindGameObjectWithTag("Player3");
            player4 = GameObject.FindGameObjectWithTag("Player4");

            if (GetComponent<UIManager>() && !endofRound )
            {
                GetComponent<UIManager>().EnableRoundEndText("");
            }

            if (!onLevel)
            {
                SetTeams();
                onLevel = true;
            }
        }
    }

    public void SetPlayerClass(string player, int playerClass)
    {
        if (player == "Player1")
        {
            player1Class = playerClass;
            //player1Class = PlayerLoginManager.p1CharacterClass;
            Debug.Log("Set Player1: " + player1Class);
        }

        if (player == "Player2")
        {
            player2Class = playerClass;
            Debug.Log("Set Player2: " + player2Class);
        }

        if (player == "Player3")
        {
            player3Class = playerClass;
            Debug.Log("Set Player3: " + player3Class);
        }

        if (player == "Player4")
        {
            player4Class = playerClass;
            Debug.Log("Set Player4: " + player4Class);
        }
    }

    public int GetPlayerClass(string tag)
    {
        if (tag == "Player1")
        {
            return player1Class;
        }

        else if (tag == "Player2")
        {
            return player2Class;
        }

        else if(tag == "Player3")
        {
            return player3Class;
        }

        else if(tag == "Player4")
        {
            return player4Class;
        }

        else
        {
            return 0;
        }
    }

    public void SetMaxRounds(int newMax)
    {
        maxRounds = newMax;
    }

    void SetTeams()
    {
        team1Players.Add(player1);
        sizeOfTeam1++;

        team2Players.Add(player2);
        sizeOfTeam2++;

        team1Players.Add(player3);
        sizeOfTeam1++;

        team2Players.Add(player4);
        sizeOfTeam2++;
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
                endofRound = true;
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
                endofRound = true;

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
            endofRound = false;
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

    public void IncrementPlayerKills(string player)
    {
        if(player == "Player1")
        {
            player1Kills++;
        }
        else if (player == "Player2")
        {
            player2Kills++;
        }
        else if (player == "Player3")
        {
            player3Kills++;
        }
        else if (player == "Player4")
        {
            player4Kills++;
        }
    }

    public void IncrementPlayerDeaths(string player)
    {
        if (player == "Player1")
        {
            player1Deaths++;
        }
        else if (player == "Player2")
        {
            player2Deaths++;

        }
        else if (player == "Player3")
        {
            player3Deaths++;
        }
        else if (player == "Player4")
        {
            player4Deaths++;
        }
    }
}

