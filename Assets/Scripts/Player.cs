using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

enum voiceCommand { Left, Right, Null}

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Transform camTran = null;
    private float[] playerBounds = new float[2] { -9, 9};//X bounds min/max

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> atctions = new Dictionary<string, Action>();
    private voiceCommand vCommand = voiceCommand.Null;

    // Start is called before the first frame update.
    void Start()
    {
        //Seting up voiceCommands.
        atctions.Add("left", MoveLeft);
        atctions.Add("right", MoveRight);
        atctions.Add("stop", MoveStop);
        keywordRecognizer = new KeywordRecognizer(atctions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedKeyword;
        keywordRecognizer.Start();
    }

    //Movement should be done in FixedUpdate to look proper.
    private void FixedUpdate()
    {
        PlayerMovement();

        camTran.position += new Vector3(0, 0, moveSpeed) * Time.deltaTime;
    }

    private void PlayerMovement()
    {
        //Voice Movement.
        if (vCommand == voiceCommand.Left && transform.position.x > playerBounds[0])
        {
            transform.Translate(new Vector3(moveSpeed * -1, 0, moveSpeed) * Time.deltaTime);
        }
        else if (vCommand == voiceCommand.Right && transform.position.x < playerBounds[1])
        {
            transform.Translate(new Vector3(moveSpeed, 0, moveSpeed) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime);
        }

        //Keyboard Input, should be commented out after testing is done.
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime);
        }
    }

    private void MoveLeft()
    {
        vCommand = voiceCommand.Left;
    }
    private void MoveRight()
    {
        vCommand = voiceCommand.Right;
    }
    private void MoveStop()
    {
        vCommand = voiceCommand.Null;
    }

    private void RecognizedKeyword(PhraseRecognizedEventArgs speach)
    {
        Debug.Log(speach.text); //For testing to know that the game heard you talk.
        atctions[speach.text].Invoke();
    }
}