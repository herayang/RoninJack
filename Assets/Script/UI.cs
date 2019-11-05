using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    static public UI SUI;

    [SerializeField] private Text score = null;
    [SerializeField] private GameObject screenEndGame = null;
    [SerializeField] private GameObject textWinGame = null;
    [SerializeField] private GameObject textEndGame = null;

    private void Awake()
    {
        if (SUI == null) SUI = this;
        else gameObject.SetActive(false);
    }

    public void UpdateScore(int amount)
    {
        score.text = "Score: " + amount;
    }

    public void EndLevel(bool win)
    {
        screenEndGame.SetActive(true);

        if (win)
        {
            textWinGame.SetActive(true);
        }
        else
        {
            textEndGame.SetActive(true);
        }
    }
}