using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    [Header("Save/Loading")]
    public XMLManager xmlManager;
    [Header("Gameplay Values")]
    public HighScoreDisplay[] highScoreDisplayArray;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();

    public string GameScene;
    public Animator transition;

    public HighScoreEntry newScore;
    void Start()
    {
        // Adds some test data
        foreach(HighScoreEntry score in xmlManager.LoadScores())
        {
            AddNewScore(score.name, score.score);
        }

        if(newScore != null)
        {
            AddNewScore(newScore.name, newScore.score);
        }

        xmlManager.SaveScores(scores);
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));
        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }
    void AddNewScore(string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
    }

    void StartNewGame(string entryName)
    {
        StartCoroutine(LoadGame(entryName));
    }

    IEnumerator LoadGame(string entryName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (transition != null) { transition.SetTrigger("Start"); }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        FindObjectOfType<ScoreTracker>().HighScoreName = entryName;
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
