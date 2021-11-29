using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip KoinSound;
    public AudioClip MatiSound;
    public AudioClip BackSou;
    AudioSource audio;
    bool Diam;
    public float moveSpeed = 0.5f;
    bool isSlide = false;
    bool isJump = true;
    bool isDead = false;
    int idMove = 0;
    public  Animator anim;
    internal object sprite;
    public CapsuleCollider2D regularColl;
    public BoxCollider2D slideColl;
    public GameObject YouWin;
    public GameObject YouLose;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Diam = false;
        YouWin.SetActive(false);
        YouLose.SetActive(false);
        anim = GetComponent<Animator>();
        Pendata.score = 0;
        audio.PlayOneShot(BackSou);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Diam)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Idle();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Idle();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Idle();
                regularColl.enabled = true;
                slideColl.enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                prefromSlide();
            }
            Move();

        }
        if(Pendata.score == 120)
        {
            YouWin.SetActive(true);
            YouLose.SetActive(false);
            Diam = true;
            Idle();

        }
    }
    public void Jump()
    {
        if (!isJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce
            (Vector2.up * 290f);
        }
    }
    public void Idle()
    {
        if (!isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
        idMove = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }
    public void MoveRight()
    {
        idMove = 1;
    }
    public void MoveLeft()
    {
        idMove = 2;
    }
    private void Move()
    {
        if (idMove == 1 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 5f, 0,
            0);
            transform.localScale = new Vector3(0.25f, 0.25f, 0.4f);
        }
        if (idMove == 2 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0
            , 0);
            transform.localScale = new Vector3(-0.25f, 0.25f, 0.4f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            //SceneManager.LoadScene("GameOver");
            isDead = true;
            audio.PlayOneShot(MatiSound);
            anim.SetTrigger("die");
            YouWin.SetActive(false);
            YouLose.SetActive(true);


        }
    }
    public void prefromSlide()
    {
        if (isSlide = true)
        {
            anim.SetTrigger("slide");
            regularColl.enabled = false;
            slideColl.enabled = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("coin"))
        {
            audio.PlayOneShot(KoinSound);
            Pendata.score += 10;
            Destroy(collision.gameObject);
        }
    }
}
