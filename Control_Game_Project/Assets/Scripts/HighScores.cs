using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HighScores : MonoBehaviour
{
    [Header("Save/Loading")]
    public XMLManager xmlManager;
    [Header("Gameplay Values")]
    public HighScoreDisplay[] highScoreDisplayArray;
    public List<HighScoreEntry> scores;

    public string GameScene;
    public Animator transition;

    public string newScoreName;
    public int newScore;
    [SerializeField] private TMP_InputField NameInput;

    private void Awake()
    {
        scores = xmlManager.LoadScores();
    }

    private void Start()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
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
    public void AddNewScore(string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
        UpdateDisplay();
    }

    public void StartNewGame()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        if(NameInput.text != "")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (transition != null) { transition.SetTrigger("Start"); }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameScene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            FindObjectOfType<ScoreTracker>().HighScoreName = NameInput.text;
            save();
            SceneManager.UnloadSceneAsync(currentScene);
        }
    }

    void save()
    {
        xmlManager.SaveScores(scores);
    }

    private void OnApplicationQuit()
    {
        save();
    }
}
