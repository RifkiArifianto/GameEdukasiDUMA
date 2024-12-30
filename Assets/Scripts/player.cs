using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed, jumpscale;

    Rigidbody2D body;

    SpriteRenderer sprite;

    Animator animator;

    public bool isground, iswall;
    public GameObject soal, gameover, finish;

    public AudioSource jump_audio, soal_audio, walk_audio, fall_audio, wrong_audio, finish_audio;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow)){
            runright();
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            runleft();
        }
        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)){
            stop();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            jump();
        }

        if(transform.localPosition.y <-10){
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }

        updateanimation();
    }

    int soalaktif (){
        return soal.GetComponent<soalmanager>().nomor();
    }
    public void runright(){
        if(soalaktif() == -1 && !gameover.activeSelf && !finish.activeSelf && !iswall){
            body.velocity = new Vector2(speed,body.velocity.y);
            sprite.flipX = false;
        }
    }
    public void runleft(){
        if(soalaktif() == -1 && !gameover.activeSelf && !finish.activeSelf && !iswall){
            body.velocity = new Vector2(-speed,body.velocity.y);
            sprite.flipX = true;
        }
    }

    public void stop(){
        if(soalaktif() == -1 && !gameover.activeSelf && !finish.activeSelf){
            body.velocity = new Vector2(0,body.velocity.y);
        }
    }

    public void jump(){
        if(soalaktif() == -1 && !gameover.activeSelf && !finish.activeSelf  && isground){
            body.velocity = new Vector2(body.velocity.x,jumpscale);
            if(Time.timeScale == 1){
                jump_audio.Play();
            }
        }
    }

    void updateanimation(){
        if(body.velocity.y>0.01f){
            animator.SetInteger("state",2);
            walk_audio.Stop();
        } else if(body.velocity.y<-0.01f){
            animator.SetInteger("state",3);
            walk_audio.Stop();
        } else {
            if(body.velocity.x>speed/2f || body.velocity.x<-speed/2f){
                animator.SetInteger("state",1);
                if(!walk_audio.isPlaying && isground){
                    if(Time.timeScale == 1){
                        walk_audio.Play();
                    }
                } 
            } else {
                animator.SetInteger("state",0);
                walk_audio.Stop();
            }
        }
    }

    bool water = false;

    void OnTriggerEnter2D (Collider2D obj){
        if(obj.name == "Water" && !water){
            water = true;
            transform.Find("Main Camera").parent = null;
            fall_audio.Play();
            StartCoroutine(gameovershow());
        }
        if(obj.tag == "pos"){
            soal_audio.Play();
            soal.transform.GetChild(obj.transform.GetSiblingIndex()).gameObject.SetActive(true);
            obj.GetComponent<SpriteRenderer>().enabled = false;
            obj.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.GetChild(0).gameObject.SetActive(false);
        }
        if(obj.name == "Finish"){
            if(soal.GetComponent<soalmanager>().soalterjawab == 20){
                finish.SetActive(true);
                finish_audio.Play();
            }
        }
    }

    IEnumerator gameovershow(){
        yield return new WaitForSeconds(0.75f);
        gameover.SetActive(true);
        wrong_audio.Play();
    }
}
