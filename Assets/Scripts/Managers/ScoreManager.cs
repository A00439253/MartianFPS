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

 // int bullets = 0;
 // int health = 0;
 // int goodies = 0;
 // int scoreStep = 2;
 // int highScore = 0;
 // float scoreMultiplier = 1;

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

    /*
    // Use this for initialization

    public void SaveHighScore()
    {
            PlayerPrefs.SetInt("Score", Score);
            if (Score >= HighScore)
            {
                highScore = Score;
                PlayerPrefs.SetInt(GetTextForScoringItems(ScoringItemsEnum.bulletsText), bullets);
            }
    }


    public IEnumerator UpdateScore()
    {
        yield return new WaitForSeconds(1);
        if (!GameManager.Instance.bIsGameOver)
        {
            score += scoreStep * (int)scoreMultiplier;
            scoreMultiplier += 0.1f;
        }
        if (!GameManager.Instance.bIsGameOver)
        {
            StartCoroutine(UpdateScore());
        }
    }

    public void AddScore(ScoringItemsEnum value)
    {
        foreach (ScoreMapping pair in scoreMappingForType)
        {
            if (pair.index == value)
            {
                score += pair.score;
            }
        }
    }
     */

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

    string GetTextForScoringItems(ScoringItemsEnum scoringItemsEnum)
    {
        switch (scoringItemsEnum)
        {
            case ScoringItemsEnum.bulletsText:  return "Bullets";
            case ScoringItemsEnum.goodiesText:  return "Goodies";
            default:                            return "None";
        }
    }


}
