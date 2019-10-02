using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

enum voiceCommand { Left, Right, Null}

public class Player : MonoBehaviour
{
    [SerializeField] private float moveMultiply; //The amount to multiply the player movement input by.
    [SerializeField] private float moveSpeed;
    private Transform TF;//The Transform for the attached object, set at start.

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> atctions = new Dictionary<string, Action>();
    private voiceCommand vCommand = voiceCommand.Null;

    // Start is called before the first frame update
    void Start()
    {
        TF = gameObject.GetComponent<Transform>(); //Get attached objects Transform.

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
        if (vCommand == voiceCommand.Left) TF.position += new Vector3((moveSpeed * moveMultiply) * -1, 0, 0);
        if (vCommand == voiceCommand.Right) TF.position += new Vector3(moveSpeed * moveMultiply, 0, 0);

        if (Input.GetAxis("Horizontal") != 0) TF.position += new Vector3(Input.GetAxis("Horizontal") * moveMultiply, 0, 0);
        if (Input.GetAxis("Vertical") != 0) TF.position += new Vector3(0, 0, Input.GetAxis("Vertical") * moveMultiply);
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
        Debug.Log(speach.text);
        atctions[speach.text].Invoke();
    }
}