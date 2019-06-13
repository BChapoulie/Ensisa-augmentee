using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControlsScene1 : MonoBehaviour
{

    public float speed = 10, jumpVelocity = 5;
   
    public Transform myTransform;
    Rigidbody2D myBody;
    private bool isGrounded = false;
    public Transform groundCheckUp;
    public Transform groundCheckBottom;
    public Transform groundCheckRight;
    public Transform groundCheckLeft;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool leftGround;
    private bool rightGround;
    private bool upGround;
    private bool downGround;
    float hInput = 0;
    private bool updateOn = true;
    public int textnumber;
    public int textnumberBackScene;
    
    public Text myText;
    public Text myText_bis;
    SpriteRenderer rend;
    SpriteRenderer rendPlayer;
    public SpriteRenderer smiley_vert;
    public SpriteRenderer smiley_rouge;

    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        myTransform = this.transform;
        myText.text = null; ;
        textnumber = 0;
        textnumberBackScene = 0;
        rend = GameObject.Find("plateforme_smol_easy").GetComponent<SpriteRenderer>();
        rendPlayer = this.GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leftGround = Physics2D.OverlapCircle(groundCheckLeft.position, groundCheckRadius, whatIsGround);
        rightGround = Physics2D.OverlapCircle(groundCheckRight.position, groundCheckRadius, whatIsGround);
        upGround = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, whatIsGround);
        downGround = Physics2D.OverlapCircle(groundCheckBottom.position, groundCheckRadius, whatIsGround);
      
        if (updateOn)
        {
            Move(hInput);
            if (Input.GetButtonDown("Jump"))
            {

                Jump();
            }
        }


    }
    public void Move(float horizontalInput)
    {
        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity = moveVel;
    }
    public void StartMoving(float horizontalInput)
    {
        hInput = horizontalInput;

    }

    public void Jump()
    {
        if ((leftGround || rightGround || upGround || downGround))
            myBody.velocity += jumpVelocity * Vector2.up;
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "ded")
        {
            //SceneManager.GetActiveScene().buildIndex
            SceneManager.LoadScene("JeuIntro#2");
        }
        if (col.gameObject.tag == "pushBack")
        {
            /*var magnitude = 2500;

    var force = transform.position - col.transform.position;

    force.Normalize();*/
            myBody.AddForce(Vector2.left * 4f, ForceMode2D.Impulse);
            StartCoroutine(updateOff(1f));

        }
        if (col.gameObject.tag == "BigJump")
        {
            myBody.AddForce(new Vector2(0.5f, 0.8f) * 30f, ForceMode2D.Impulse);
            StartCoroutine(updateOff(2f));
        }
    }
    IEnumerator updateOff(float k)
    {
        updateOn = false;
        yield return new WaitForSeconds(k);
        updateOn = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {   
        
        if (col.gameObject.tag == "changetext1")
        {
           

            Debug.Log(textnumber);
            switch (textnumber)
            {
                case 0:
                    Destroy(GameObject.Find("ChangeTextTrigger0"));
                    myText.text = "Bienvenue sur le tutoriel de L'ENS'INSIDE\nPour une prise en main\n\tludique et intuitive!";
                    break;
                case 1:
                    rendPlayer.sprite = smiley_vert.sprite;
                    Destroy(GameObject.Find("ChangeTextTrigger1"));
                    myText.text = "Moi c'est paint.jpg\n Je vais t'apprendre à utiliser l'ENS'INSIDE";
                    break;
                case 2:
                    rendPlayer.sprite = smiley_vert.sprite;
                    Destroy(GameObject.Find("ChangeTextTrigger2"));
                    myText.text = "Je suis capable de franchir les obstacles";
                    break;
                case 3:
                    Destroy(GameObject.Find("ChangeTextTrigger3"));
                    myText.text = "Je suis capable de franchir les obstacles\net je donne d'excellents conseils";
                    break;                case 4:
                    Destroy(GameObject.Find("ChangeTextTrigger4"));
                    myText.text = "Je suis capable de franchir les obstacles\net je donne d'excellents conseils\nIl faut donc faire ce que je dis.";
                    break;
                case 5:

                    myText.text = "Appuie sur l'ecran pour me faire sauter! ";
                    break;
                case 7:
                    myText.text = "Tu peux le faire! ";
                    break;
                case 8:
                    myText_bis.text = "je crois en toi!";
                    break;
                case 10:
                    myText_bis.text = null;
                    myText.text = "Bloqué au tutorial.\nJe vois, laisse moi ajuster la difficulté. ";
                    break;
                case 11:
                   

                    Destroy(GameObject.Find("plateforme_smol_dur"));
                    Destroy(GameObject.Find("pushback_dur"));
                    rend.enabled = !rend.enabled;
                    break;
            }
            textnumber++;


        }
        if (col.gameObject.tag == "changetext2")
        {
            myText.text = "Félicitations!\nNous voilà dans le Grand-Amphi";
        }
        if (col.gameObject.tag == "changetext3")
        {
            myText.text = "Allons vite découvrir les fonctionnalités!!\nNe t'inquiètes pas tu n'as qu'à sauter!";
        }
        if (col.gameObject.tag == "changetext-1")
        {
            switch (textnumberBackScene)
            {
                case 0:
                    Destroy(GameObject.Find("ChangeTextTrigger-1"));
                    rendPlayer.sprite = smiley_rouge.sprite;
                    myText.text = "Il faut suivre les flèches, il n'y a RIEN dans cette direction";
                    break;
                case 1:
                    Destroy(GameObject.Find("ChangeTextTrigger-2"));
                    rendPlayer.sprite = smiley_vert.sprite;
                    myText.text = null;
                    break;
            }

            textnumberBackScene++;
        }
       
        if (col.gameObject.tag == "portal_bl")
        {
            myTransform.position = new Vector2(-114, 18.5f);
        }



    }
}
