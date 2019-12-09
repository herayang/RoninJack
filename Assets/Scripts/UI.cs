using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UI : MonoBehaviour
{
    static public UI SUI;

    [SerializeField] private GameObject CV = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private GameObject screenEndGame = null;
    [SerializeField] private Text scoreEndGame = null;
    [SerializeField] private int score = 0;
    [SerializeField] private Text[] highScoresText = new Text[9];
    [SerializeField] private string[] highScores = new string[9];

    private void Awake()
    {
        if (SUI == null) SUI = this;
        else gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        score = (int)Time.time;
        scoreText.text = "Score: " + score;
    }

    public void EndLevel()
    {
        GameObject.Destroy(CV);
        screenEndGame.SetActive(true);
        scoreEndGame.text = "Your Score Is: " + score;

        LoadHighScores();
        for(int i = 0; i <= 8; i++)
        {
            if(score > int.Parse(highScores[i]))
            {
                for(int ii = 8; ii > i; ii--)
                {
                    highScores[ii] = highScores[ii - 1];
                }
                highScores[i] = "" + score;
                SaveHighScores();
                break;
            }
        }
        UpdateHighScores();
    }

    public void ButtonMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetHighScores()
    {
        string path = "Assets/HighScores.txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("600");
        writer.WriteLine("480");
        writer.WriteLine("360");
        writer.WriteLine("240");
        writer.WriteLine("180");
        writer.WriteLine("120");
        writer.WriteLine("60");
        writer.WriteLine("30");
        writer.Write("10");
        writer.Close();
    }

    public void SaveHighScores()
    {
        string path = "Assets/HighScores.txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(highScores[0]);
        writer.WriteLine(highScores[1]);
        writer.WriteLine(highScores[2]);
        writer.WriteLine(highScores[3]);
        writer.WriteLine(highScores[4]);
        writer.WriteLine(highScores[5]);
        writer.WriteLine(highScores[6]);
        writer.WriteLine(highScores[7]);
        writer.Write(highScores[8]);
        writer.Close();
    }

    public void LoadHighScores()
    {
        string path = "Assets/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        //Debug.Log(reader.ReadToEnd());
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = reader.ReadLine();
        }
        reader.Close();
    }

    public void UpdateHighScores()
    {
        for(int i = 0; i < highScoresText.Length; i++)
        {
            highScoresText[i].text = (i + 1) + ": " + highScores[i];
        }
    }
}