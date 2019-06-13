using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControlScene2 : MonoBehaviour
{
    public float speed = 10, jumpVelocity = 5;
   
    public Transform myTransform;
    Rigidbody2D myBody;
    public bool isGrounded = false;
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
    SpriteRenderer rendPlayer;
    public SpriteRenderer smiley_vert;
    public SpriteRenderer smiley_rouge;
    public Text myText;
    public Text myText_bis;
    public Text myText_bottom;
    BoxCollider2D plat_inscription;
    BoxCollider2D plat_connexion;
    BoxCollider2D plat_side;
    CameraController camera_follow_player;
    GameObject respawn, goRespawn, plat_side2;
    GameObject blackgood, plateforme_sol, taphere, taphere1, taphere2, taphere3, side_menu, leMoins, cadreMoins, Image_cadre;
   
    SpriteRenderer boum1, boum2, boum3, boum4, Inscription, connexion, respawn_sprite;
    GameObject boutonCompris;
   
    // Start is called before the first frame update
    void Start()
    {
        camera_follow_player = GameObject.Find("Camera").GetComponent<CameraController>();
        camera_follow_player.enabled = false;
        myBody = this.GetComponent<Rigidbody2D>();
        myTransform = this.transform;
        myText.text = null;
        myText_bis.text = null;
        textnumber = 0;
        textnumberBackScene = 0;
        boum1 = GameObject.Find("boum1").GetComponent<SpriteRenderer>();
        boum2 = GameObject.Find("boum2").GetComponent<SpriteRenderer>();
        Inscription = GameObject.Find("Inscription").GetComponent<SpriteRenderer>();
        respawn = GameObject.Find("respawn");
        respawn_sprite = respawn.GetComponent<SpriteRenderer>();
        goRespawn = GameObject.Find("goRespawn");
        goRespawn.SetActive(false);
        blackgood = GameObject.Find("blackgood");
        blackgood.SetActive(false);
        connexion = GameObject.Find("connexion").GetComponent<SpriteRenderer>();
        boutonCompris = GameObject.Find("Compris");
        rendPlayer = this.GetComponent<SpriteRenderer>();
        plat_inscription = GameObject.Find("plateforme_7").GetComponent<BoxCollider2D>();
        plat_connexion = GameObject.Find("plateforme_1").GetComponent<BoxCollider2D>();
        plat_side = GameObject.Find("plateforme_side").GetComponent<BoxCollider2D>();
        plateforme_sol = GameObject.Find("plateforme_sol");
        plateforme_sol.SetActive(false);
        boutonCompris = GameObject.Find("Compris");
        boutonCompris.SetActive(false);
        taphere = GameObject.Find("taphere");
        taphere1 = GameObject.Find("taphere1");
        taphere2 = GameObject.Find("taphere2");
        taphere3 = GameObject.Find("taphere3");
        taphere.SetActive(false);
        taphere1.SetActive(false);
        taphere2.SetActive(false);
        taphere3.SetActive(false);
        plat_side2= GameObject.Find("plat_side2");
        plat_side2.SetActive(false);
       
        leMoins = GameObject.Find("boumi");
        leMoins.SetActive(false);

        side_menu = GameObject.Find("Capture2");
        side_menu.SetActive(false);

        cadreMoins= GameObject.Find("cadreMoins");
        cadreMoins.SetActive(false);

        Image_cadre = GameObject.Find("Image_cadre");
        Image_cadre.SetActive(false);
       

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
    public void setRespawn()
    {
        respawn.transform.position = myTransform.position;
    }
    void OnCollisionEnter2D(Collision2D col)
    {

    }
   public  void ClickCompris()
    {
        boum1.enabled = false;
        
        textnumber++;
        Debug.Log(textnumber);
        switch (textnumber)
        {
            case 1:
                boum2.enabled = !boum2.enabled;
                myText.text = "N'hésite pas à t'inscrire";
                myText_bis.text = "en utilisant ce bouton.";
                break;
            case 2:
                boum2.enabled = false;
                Inscription.enabled = !Inscription.enabled;
                myText.text = "Voilà la page d'inscription";
                myText_bis.text = "En 30 secondes c'est fait!";
                plat_connexion.enabled = !plat_connexion.enabled;
                plat_inscription.enabled = !plat_inscription.enabled;
                break;
            case 3:
                myTransform.position = new Vector2(-307, 114);
                connexion.enabled = false;
                plat_connexion.enabled = !plat_connexion.enabled;
                plat_inscription.enabled = !plat_inscription.enabled;
                Inscription.enabled = !Inscription.enabled;
                myText.text = null;
                myText_bis.text = null;
                myText_bottom.text = "Une fois inscris, tu peux te connecter";
                boutonCompris.SetActive(false);
                camera_follow_player.enabled = !camera_follow_player.enabled;
                plat_side.enabled = !plat_side.enabled;
               
                respawn_sprite.enabled = true;
                goRespawn.SetActive(true);
                blackgood.SetActive(true);
                plateforme_sol.SetActive(true);
                taphere.SetActive(true);
                taphere1.SetActive(true);
                taphere2.SetActive(true);
                taphere3.SetActive(true);
                break;
           
            case 11:
                myText.text = null;
                
                myText_bis.text = null;
                leMoins.SetActive(true);
                side_menu.SetActive(true);
                Image_cadre.SetActive(true);
                cadreMoins.SetActive(true);
                myText_bottom.text = "Le bouton en bas à droite ouvre ce beau menu.";
                break;
            case 12:
                Image_cadre.SetActive(false);
                myText_bottom.text = "Tu peux consulter et éditer ton profil!";
                break;
            case 13:
                myText_bottom.text = "Tu peux savoir dans quelle salle est un autre inscrit! Super! ";
                break;

            case 14:
                myText_bottom.text = "Tu peux même rechercher une salle sans te fatiguer!";
                break;
            case 15:
                myText_bottom.text = "Découvre vite toute ces fonctionnalités et bien d'autres!";
                
                break;
            
           
            default:
                SceneManager.LoadScene("ConnexionScene");
                break;

        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "changetext1")
        {
            Camera.main.orthographicSize = 16;
            myText.text = "Voilà le menu de connexion.";
            myText_bis.text = "C'est ici qu'on arrive lorsqu'on ouvre l'application.";
            boum1.enabled = !boum1.enabled;
            boutonCompris.SetActive(true);
            Destroy(GameObject.Find("phasetrigger1"));
        }
        if (col.gameObject.tag == "changetext2")
        {

            Destroy(GameObject.Find("phasetrigger2"));
            myText_bottom.text = "Une fois connecté, la caméra va se lancer.\nPas de panique ENS'INSIDE est\nune application de réalité augmentée!!";


        }
        if (col.gameObject.tag == "Player")
        {
            rendPlayer.sprite = smiley_rouge.sprite;
        }
        if (col.gameObject.tag == "changetext3")
        {
            plat_side2.SetActive(true);
            boutonCompris.SetActive(true);
            camera_follow_player.enabled = !camera_follow_player.enabled;
            myText.color = Color.black;
            myText_bis.color = Color.black;
            myText.text = "Tu peux scanner le QR-code devant ta salle.";
            myText_bis.text = "Consulte l'emploi du temps et les infos sur la salle\nen utilisant les boutons virtuels.";
            myText_bottom.text = "";
            GameObject.Find("Camera").transform.position = new Vector3(-258.1f, 96.1f, -10);
            Destroy(GameObject.Find("phasetrigger3"));
            setRespawn();
            respawn_sprite.enabled = !respawn_sprite.enabled;
            textnumber = 10;
            goRespawn.SetActive(false);

        }
        if (col.gameObject.tag == "portal_bl")
        {
            boum1.enabled = false;
            myTransform.position = new Vector2(-307, 114);
            textnumber++;
            Debug.Log(textnumber);
            switch (textnumber)
            {
                case 1:
                    boum2.enabled = !boum2.enabled;
                    myText.text = "N'hésite pas à t'inscrire";
                    myText_bis.text = "en utilisant ce bouton.";
                    break;
                case 2:
                    boum2.enabled = false;
                    Inscription.enabled = !Inscription.enabled;
                    myText.text = "Voilà la page d'inscription";
                    myText_bis.text = "En 30 secondes c'est fait!";
                    plat_connexion.enabled = !plat_connexion.enabled;
                    plat_inscription.enabled = !plat_inscription.enabled;
                    break;
                case 3:
                    connexion.enabled = false;
                    plat_connexion.enabled = !plat_connexion.enabled;
                    plat_inscription.enabled = !plat_inscription.enabled;
                    Inscription.enabled = !Inscription.enabled;
                    myText.text = null;
                    myText_bis.text = null;
                    myText_bottom.text = "Une fois inscris, tu peux te connecter!";
                    boutonCompris.SetActive(false);
                    camera_follow_player.enabled = !camera_follow_player.enabled;
                    plat_side.enabled = !plat_side.enabled;
                    respawn_sprite.enabled = true;
                    goRespawn.SetActive(true);
                    blackgood.SetActive(true);
                    plateforme_sol.SetActive(true);
                    taphere.SetActive(true);
                    taphere1.SetActive(true);
                    taphere2.SetActive(true);
                    taphere3.SetActive(true);
                    break;
                default:
                    SceneManager.LoadScene("ConnexionScene");
                    break;

            }
        }

        if (col.gameObject.tag == "ded")
        {
            rendPlayer.sprite = smiley_vert.sprite;
            myTransform.position = respawn.transform.position;
        }

    }
    
}
