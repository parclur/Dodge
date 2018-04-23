using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    int redTeamScore;
    int blueTeamScore;
    int roundNum;

    public Text redTeamScoreText;
    public Text blueTeamScoreText;
    public Text roundNumText;
    public Text roundEndText;

    // Use this for initialization
    void Start () {
        DisableRoundEndText();
        roundNum = 1;
        redTeamScore = 0;
        blueTeamScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTexts();
	}

    void UpdateTexts()
    {
        redTeamScoreText.text = redTeamScore.ToString();
        blueTeamScoreText.text = blueTeamScore.ToString();
        roundNumText.text = roundNum.ToString();
    }

    public void SetRedTeamNum(int newRedNum)
    {
        redTeamScore = newRedNum;
    }

    public void SetBlueTeamNum(int newBlueNum)
    {
        blueTeamScore = newBlueNum;
    }

    public void SetRoundNum(int newRoundNum)
    {
        roundNum = newRoundNum;
    }

    public void EnableRoundEndText(string newText)
    {
        roundEndText.text = newText;
    }

    public void DisableRoundEndText()
    {
        roundEndText.text = "";
    }
}
