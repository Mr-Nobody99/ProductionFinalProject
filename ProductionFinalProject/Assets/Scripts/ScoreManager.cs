using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text highScoreText;
    public int highScore = 0;
    public Text currentScoreText;
    public int currentScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScore.GetCurrentScore() > PlayerScore.GetHighScore())
        {
            PlayerScore.UpdateHighScore();
        }
        highScore = PlayerScore.GetHighScore();
        highScoreText.text = highScore.ToString();
        currentScore = PlayerScore.GetCurrentScore();
        currentScoreText.text = currentScore.ToString();
    }
}
