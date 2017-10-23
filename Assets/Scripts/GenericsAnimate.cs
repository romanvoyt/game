using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericsAnimate : MonoBehaviour {
    public Sprite[] sprites; 
    public bool loop, destroyOnFinish, whileNear, needsActivating;
    bool Activated = false;
    SpriteRenderer sr;
    float timer = 0.5f;
    public float distance = 5.0f;
    public float timerReset = 0.05f;
    GameObject player;
    int counter = 0;
    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (whileNear == true)
        {
            animateWhileNear();
        }
        else if (needsActivating == true && Activated == false)
        {
            if(Input.GetKeyDown(KeyCode.E) && Vector3.Distance (player.transform.position, this.transform.position) < 2.0f)
            {
                Activated = true;
            }
        }
        else if (needsActivating == true && Activated == true)
        {
            normalAnimate();
            if (Input.GetKeyDown (KeyCode.E) && Vector3.Distance(player.transform.position, this.transform.position) < 2.0f)
            {
                Activated = false;
            }
        }
        else
        {
            normalAnimate();
        }
	}
    void animateWhileNear()
    {
        sr.sprite = sprites[counter];
        if(Vector3.Distance (player.transform.position, this.transform.position) < distance)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if(counter < sprites.Length - 1)
                {
                    counter++;
                }
                else
                {
                    if (loop == true)
                    {
                        counter = 0;
                    }
                    else if(destroyOnFinish == true)
                    {
                        Destroy(this.gameObject);
                    }
                }
                timer = timerReset;
            }
        }
    }
    void normalAnimate()
    {
        sr.sprite = sprites[counter];
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(counter < sprites.Length - 1)
            {
                counter++;
            }
            else
            {
                if (loop == true)
                {
                    counter = 0;
                }
                else if (destroyOnFinish == true)
                {
                    Destroy(this.gameObject);
                }
            }
            timer = timerReset;

        }
    }
}
