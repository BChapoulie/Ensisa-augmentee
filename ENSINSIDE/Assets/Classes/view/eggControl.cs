using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggControl : MonoBehaviour
{
     SpriteRenderer rendRed1,rendRed2,rendRed3,rendRed4,rendRed5,rendRed6,rendRed7,rendRed8,rendGreen,rendRed;
    
    void Start()
    {
        rendRed1 = GameObject.Find("plateformeRouge").GetComponent<SpriteRenderer>();
        rendRed2 = GameObject.Find("plateforme Rouge").GetComponent<SpriteRenderer>();
        rendRed3 = GameObject.Find("plateformeRouge2").GetComponent<SpriteRenderer>();
        rendRed4 = GameObject.Find("plateforme Rouge2").GetComponent<SpriteRenderer>();
        rendRed5 = GameObject.Find("plateformeRouge3").GetComponent<SpriteRenderer>();
        rendRed6 = GameObject.Find("plateforme Rouge3").GetComponent<SpriteRenderer>();
        rendRed7 = GameObject.Find("plateformeRouge4").GetComponent<SpriteRenderer>();
        rendRed8 = GameObject.Find("plateforme Rouge4").GetComponent<SpriteRenderer>();
        rendRed = GameObject.Find("plateforme_red").GetComponent<SpriteRenderer>();
        rendGreen = GameObject.Find("plateformeVert").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "vote1")
        {
            rendRed2.sprite = rendRed.sprite;
            rendRed4.sprite = rendRed.sprite;
            rendRed6.sprite = rendRed.sprite;
            rendRed8.sprite = rendRed.sprite;
            rendRed1.sprite = rendGreen.sprite;
            rendRed3.sprite = rendGreen.sprite;
            rendRed5.sprite = rendGreen.sprite;
            rendRed7.sprite = rendGreen.sprite;

        }
        if (col.gameObject.tag == "vote2")
        {
            rendRed2.sprite = rendGreen.sprite;
            rendRed4.sprite = rendGreen.sprite;
            rendRed6.sprite = rendGreen.sprite;
            rendRed8.sprite = rendGreen.sprite;
            rendRed1.sprite = rendRed.sprite;
            rendRed3.sprite = rendRed.sprite;
            rendRed5.sprite = rendRed.sprite;
            rendRed7.sprite = rendRed.sprite;
        }
    }
}
