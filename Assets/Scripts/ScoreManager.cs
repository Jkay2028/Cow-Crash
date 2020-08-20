using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Text endScreenScoreText;
    public Text scoreText;
    public static ScoreManager instance;
    public Text highScoreText;
    public float score = 0;

    private string highScoreString = "High Score";
    private float highScore = 0;
    private bool running = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Debug.LogError("Already an Instance of a score manager class");

        }


        HideEndScreenText();
        HideText();

        if (PlayerPrefs.HasKey(highScoreString))
        {
            highScore = PlayerPrefs.GetFloat(highScoreString);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator IncreaseScore()
    {
        yield return new WaitForSeconds(1);
        if (!PlayerController.instance.isDead)
        {
            score++;
            scoreText.text = "Score: " + score;
            StartCoroutine(IncreaseScore());
        }

    }
    public void StopGame()
    {
        running = false;
        scoreText.gameObject.SetActive(false);
        ShowEndScreenText();
        endScreenScoreText.text = "Score: " + score;
    }
    private void ShowEndScreenText()
    {
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetFloat(highScoreString, highScore);
        }
        endScreenScoreText.gameObject.SetActive(true);

        highScoreText.gameObject.SetActive(true);
        highScoreText.text = "High Score: " + highScore;
    }
    public void HideEndScreenText()
    {
        endScreenScoreText.gameObject.SetActive(false);

        highScoreText.gameObject.SetActive(false);
    }
    public void ShowText()
    {
        scoreText.gameObject.SetActive(true);
    }
    public void HideText()
    {
        scoreText.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        running = true;
        HideEndScreenText();
        score = 0;
        scoreText.text = "Score: " + score;
        ShowText();
        StartCoroutine(IncreaseScore());
    }


}

