using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Player : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    [SerializeField] private float jumpHight = 3.0f;
    [SerializeField] private float jumpSpeed = 0.25f;
    private bool canJump = true;
    private bool canAnimate = true;

    private Rigidbody RB;
    [HideInInspector] public Animation ANIM;
    private CamaraFollower CF;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> atctions = new Dictionary<string, Action>();

    public AudioSource[] audioSources;

    void Start()
    {
        if(mainMenu.gameSpeed != 0)
        {
            moveSpeed = mainMenu.gameSpeed;
        }
        else
        {
            moveSpeed = 6;
        }
        RB = gameObject.GetComponent<Rigidbody>();
        ANIM = gameObject.GetComponent<Animation>();
        CF = transform.parent.gameObject.GetComponent<CamaraFollower>();
        CF.SetUp(this, moveSpeed);

        audioSources = transform.parent.gameObject.GetComponents<AudioSource>();
        //atctions.Add("slide", Slide);
        atctions.Add("hit", Attack);
        atctions.Add("jump", Jump);

        //if(System.)
        keywordRecognizer = new KeywordRecognizer(atctions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedKeyword;
        keywordRecognizer.Start();
    }

    //Movement should be done in FixedUpdate to look proper.
    private void FixedUpdate()
    {
        CF.UpdateCamara(transform.position.y);
        KeyboardCommands();
        if(transform.localPosition.z < -3)
        {
            Die();
        }
    }

    private void KeyboardCommands()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) Slide();
        if (Input.GetKeyDown(KeyCode.Z)) Attack();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        //if (Input.GetAxis("Horizontal") != 0) transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        //if (Input.GetAxis("Vertical") != 0) transform.position += new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    private void Slide()
    {
        if (canAnimate)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    private void Attack()
    {
        if (canAnimate)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void Jump()
    {
        if (canAnimate && canJump)
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    private void RecognizedKeyword(PhraseRecognizedEventArgs speach)
    {
        atctions[speach.text].Invoke();
    }

    private IEnumerator SlideCoroutine()
    {
        canAnimate = false;
        yield return new WaitForSeconds(2);
        canAnimate = true;
    }

    private IEnumerator JumpCoroutine()
    {
        canJump = false;
        canAnimate = false;
        audioSources[2].Play();
        ANIM.Play("BetterJump");
        ANIM.CrossFadeQueued("Run", 0.5f, QueueMode.CompleteOthers);
        RB.useGravity = false;
        float playersY = transform.position.y;
        while (transform.position.y < (playersY + jumpHight))
        {
            transform.Translate(new Vector3(0, jumpSpeed, 0));
            yield return null;
        }
        RB.useGravity = true;
        canAnimate = true;
    }

    private IEnumerator AttackCoroutine()
    {
        canAnimate = false;
        audioSources[1].Play();
        ANIM.Play("RunningAttack");
        ANIM.CrossFadeQueued("Run", 0.5f, QueueMode.CompleteOthers);
        yield return new WaitForSeconds(1);
        canAnimate = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Die();
        }

        if (canJump == false & collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Turn")
        {
            CF.Turn(collision.gameObject.GetComponent<Turn>().turnTargetRotation, collision.gameObject.GetComponent<Turn>().turnSpeed);
        }
        else if (collision.gameObject.tag == "End")
        {
            CF.enabled = false;
            UI.SUI.EndLevel();
            gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        audioSources[0].Play();
        CF.enabled = false;
        UI.SUI.EndLevel();
        gameObject.SetActive(false);
    }
}