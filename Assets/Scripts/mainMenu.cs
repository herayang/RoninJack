using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class mainMenu : MonoBehaviour
{
    static public float gameSpeed;

    private void Start()
    {
        if (gameSpeed == 0.0f)
        {
            gameSpeed = 6;
        }
    }

    public void playGame() {
        SceneManager.LoadScene(1);
	}

	public void quitGame() {
		Debug.Log("Quit Game");
		Application.Quit();
	} 

    public void ButtonSpeed(float speed)
    {
        gameSpeed = speed;
        Debug.Log(gameSpeed);
    }

    public void ResetHighScores()
    {
        string path = "HighScores.txt";
        File.Delete(Application.dataPath + path);
    }
}
