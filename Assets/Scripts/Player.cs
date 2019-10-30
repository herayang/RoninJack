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
    private Rigidbody RB;
    public bool invariable = false;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> atctions = new Dictionary<string, Action>();
    private voiceCommand vCommand = voiceCommand.Null;

    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();

        atctions.Add("left", MoveLeft);
        atctions.Add("right", MoveRight);
        atctions.Add("stop", MoveStop);

        atctions.Add("slide", Slide);
        atctions.Add("attack ", Attack);
        atctions.Add("jump", Jump);

        keywordRecognizer = new KeywordRecognizer(atctions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedKeyword;
        keywordRecognizer.Start();
    }

    //Movement should be done in FixedUpdate to look proper.
    private void FixedUpdate()
    {
        transform.parent.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime, Space.World);
        KeyboardCommands();
        PlayerMovement();
    }

    private void KeyboardCommands()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) Slide();
        if (Input.GetKeyDown(KeyCode.Z)) Attack();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetAxis("Horizontal") != 0) transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetAxis("Vertical") != 0) transform.position += new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        //Voice Movement.
        if (vCommand == voiceCommand.Left)
        {
            transform.Translate(new Vector3(moveSpeed * -1, 0, 0) * Time.deltaTime);
        }
        else if (vCommand == voiceCommand.Right)
        {
            transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(0, 0, 0) * Time.deltaTime);
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

    private void Slide()
    {
        StartCoroutine(SlideCoroutine());
    }

    private void Attack()
    {
        //Nothing here for now;
    }

    private void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    private void RecognizedKeyword(PhraseRecognizedEventArgs speach)
    {
        atctions[speach.text].Invoke();
    }

    private IEnumerator SlideCoroutine()
    {
        invariable = true;
        yield return new WaitForSeconds(5);
        invariable = false;
    }

    private IEnumerator JumpCoroutine()
    {
        RB.useGravity = false;
        float playersY = transform.position.y;
        while (transform.position.y < (playersY + 3))
        {
            transform.Translate(new Vector3(0, 0.25f, 0));
            yield return null;
        }
        RB.useGravity = true;
    }
}