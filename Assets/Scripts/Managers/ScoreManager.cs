using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;

    public static ScoreManager Instance { get { return instance; } }
    ScoreManager()
    {
        Init();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }


    public Scrollbar HealthBar;

    public enum ScoringItemsEnum
    {
        bulletsText = 0,
        goodiesText,
        highScoreText,
        scoreText,
        healthText,
        gameExit,
    }

    [System.Serializable]
    public class ScoreMapping
    {
        public ScoreManager.ScoringItemsEnum index;
        public int score;
        public Text text;
    }

    public ScoreMapping[] scoreMappingForType;
    Dictionary<ScoringItemsEnum, ScoreMapping> scoreMapDictionary;


    private void Init()
    {
        if (scoreMappingForType == null || scoreMappingForType.Length == 0)
        {
            int enumLen = Enum.GetNames(typeof(ScoringItemsEnum)).Length;
            scoreMappingForType = new ScoreMapping[enumLen];

            for (int i = 0; i < enumLen; ++i)
            {
                scoreMappingForType[i] = new ScoreMapping();
                scoreMappingForType[i].index = (ScoringItemsEnum)i;
            }
        }
    }

    void Start() {

        scoreMapDictionary = new Dictionary<ScoringItemsEnum, ScoreMapping>();

        foreach (var scoreMap in scoreMappingForType)
        {
            scoreMapDictionary.Add(scoreMap.index, scoreMap);
        }

        StartCoroutine("UpdatePlayerUI");

    }


    IEnumerator UpdatePlayerUI()
    {
        yield return new WaitForSeconds(2);
        if (PlayerProperties.Instance != null)
        {
            UpdateBullets(PlayerProperties.Instance.bullets);
            UpdateHealth(PlayerProperties.Instance.health);
            UpdateGoodies(PlayerProperties.Instance.goodies);
        }
        else {
            StartCoroutine("UpdatePlayerUI");
        }
    }

    // Use this for initialization

    public void SaveHighScore()
    {
        int currentScore = CalculateScore();
        if (currentScore > GetHighScore())
        {
            PlayerPrefs.SetInt(GetTextForScoringItems(ScoringItemsEnum.highScoreText), currentScore);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(GetTextForScoringItems(ScoringItemsEnum.highScoreText));
    }

    public void UpdateHealth(int val)
    {
        scoreMapDictionary[ScoringItemsEnum.healthText].text.text = "" + val;
        HealthBar.size = (float)((float)val / 100);
    }

    public void UpdateBullets(int val)
    {
        scoreMapDictionary[ScoringItemsEnum.bulletsText].text.text = "" + val;
    }

    public void UpdateGoodies(int val)
    {
        scoreMapDictionary[ScoringItemsEnum.goodiesText].text.text = "" + val;
    }

    public void UpdateGameExitText(string val)
    {
        scoreMapDictionary[ScoringItemsEnum.gameExit].text.text = val;
    }

    public void UpdateScore()
    {
        scoreMapDictionary[ScoringItemsEnum.scoreText].text.text = "" + CalculateScore();
    }

    public void UpdateHighScore()
    {
        scoreMapDictionary[ScoringItemsEnum.highScoreText].text.text = "" + GetHighScore();
    }
    

    public void UpdateGameExitText(bool bWon)
    {
        scoreMapDictionary[ScoringItemsEnum.gameExit].text.text = bWon ? "You Won!!!" : "Better Luck next time!!";
    }

    string GetTextForScoringItems(ScoringItemsEnum scoringItemsEnum)
    {
        switch (scoringItemsEnum)
        {
            case ScoringItemsEnum.highScoreText:    return "HighScore";
            case ScoringItemsEnum.goodiesText:      return "Goodies";
            default:                                return "None";
        }
    }

    public int CalculateScore()
    {
        return (PlayerProperties.Instance.bullets + 1) *
            (PlayerProperties.Instance.goodies + 1);
    }
}
