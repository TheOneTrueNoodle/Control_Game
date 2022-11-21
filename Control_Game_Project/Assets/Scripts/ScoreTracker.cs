using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public string HighScoreName;
    [HideInInspector] public int Score;

    public string MainMenuScene;
    public Animator transition;

    [Header("ScoreDisplayValues")]
    public byte ScoreDisplaySpeed;
    public float DisplayedScore;
    public TMP_Text scoreText;
    private int ScoreGainRate = 100;

    private void Update()
    {
        DisplayedScore = Mathf.MoveTowards(DisplayedScore, Score, ScoreDisplaySpeed * Time.deltaTime);
        UpdateScoreDisplay();
    }

    private void FixedUpdate()
    {
        IncreaseScore(ScoreGainRate);
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = string.Format("Score: {0:00000}", DisplayedScore);
    }

    public IEnumerator MainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (transition != null) { transition.SetTrigger("Start"); }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(MainMenuScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        FindObjectOfType<HighScores>().AddNewScore(HighScoreName, Score);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
