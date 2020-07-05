using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3f;

    private float bounceSpeed = 5f;

    private bool didFlap;

    public bool isAlive;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flap, coin, die;

    public int score;

    void Awake() {
        if (instance == null) {
            instance = this;
        }

        isAlive = true;
        score = 0;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapTheBird());

        SetCamerasX();
    }

    private void SetCamerasX() {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    private void FlapTheBird() {
        didFlap = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive) {

            //move bird forward
            Vector2 temp = transform.position;
            temp.x += forwardSpeed * Time.fixedDeltaTime;
            transform.position = temp;

            if (didFlap) {

                didFlap = false;
                myRigidBody.velocity = new Vector2(0,bounceSpeed);
                anim.SetTrigger("Flap");
                audioSource.PlayOneShot(flap);
            }

            if (myRigidBody.velocity.y >= 0) {
                transform.rotation = Quaternion.identity;
            } else {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public float GetPositionX() {
        return transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D target) {
        if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe") {
            if (isAlive) {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(die);
                GamePlayController.instance.PlayerDiedShowScore(score);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "PipeHolder") {
            audioSource.PlayOneShot(coin);
            score++;
            GamePlayController.instance.SetScore(score);
        }
    }
}
