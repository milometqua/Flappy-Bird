using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float jumpForce;
    private bool levelStart;
    public GameObject gameController;
    public GameObject message;
    private int score;
    public Text scoreText;
    private void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        levelStart = false;
        rigidbody.gravityScale = 0;
        score = 0;
        message.GetComponent<SpriteRenderer>().enabled = true;
        scoreText.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SoundController.instance.PlayThisSound("wing", 0.5f);
            if(!levelStart){
                scoreText.enabled = true;
                levelStart = true;
                rigidbody.gravityScale = 3;
                gameController.GetComponent<PipeGenerate>().enableGeneratePipe = true;
                message.GetComponent<SpriteRenderer>().enabled = false;
            }
            BirdMoveUp();
            transform.rotation = Quaternion.Euler(0f, 0f, 20f);
        }
    }
    private void BirdMoveUp()
    {
        rigidbody.velocity = Vector2.up * jumpForce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReloadScene();
        score = 0;
        SoundController.instance.PlayThisSound("hit", 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        score += 1;
        scoreText.text = score.ToString();
        SoundController.instance.PlayThisSound("point", 0.5f);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("_gameplay");
    }
}
