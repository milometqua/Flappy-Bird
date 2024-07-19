using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float jumpForce;
    private bool levelStart;
    private bool wing;
    public GameObject gameController;
    public GameObject message;
    private int score;
    public Text scoreText;
    public Text scoreTextfn;
    public Text highScoreText;
    private float targetz;
    private void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        levelStart = false;
        wing = true;
        rigidbody.gravityScale = 0;
        score = 0;
        message.GetComponent<SpriteRenderer>().enabled = true;
        scoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("", 0).ToString();
        scoreText.enabled = false;
        scoreTextfn.enabled = false;
        highScoreText.enabled = false;
        targetz = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wing && Input.GetKeyDown(KeyCode.Space)){
            SoundController.instance.PlayThisSound("wing", 0.5f);
            if(!levelStart){
                scoreText.enabled = true;
                levelStart = true;
                rigidbody.gravityScale = 3;
                gameController.GetComponent<PipeGenerate>().enableGeneratePipe = true;
                message.GetComponent<SpriteRenderer>().enabled = false;
            }
            BirdMoveUp();
        }
        else if(levelStart && wing)
        {
            BirdMoveDown();
        }
    }
    private void BirdMoveUp()
    {
        rigidbody.velocity = Vector2.up * jumpForce;
        while(targetz <= 20f)
        {
            targetz += 5f;
            transform.rotation = Quaternion.Euler(0f, 0f, targetz);
        }
    }
    private void BirdMoveDown()
    {
        if (targetz > -50f)
        {
            targetz -= 0.8f;
            transform.rotation = Quaternion.Euler(0f, 0f, targetz);
        }
    }    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        wing = false;
        GameManager.instance.GameOver();
        scoreText.enabled = false;
        scoreTextfn.enabled = true;
        highScoreText.enabled = true;
        SoundController.instance.PlayThisSound("hit", 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpdateScore();
    }
    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt(""))
        {
            PlayerPrefs.SetInt("", score);
            highScoreText.text = score.ToString();
        }
    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        scoreTextfn.text = score.ToString();
        SoundController.instance.PlayThisSound("point", 0.5f);
        UpdateHighScore();
    }
}
