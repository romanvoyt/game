using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public static bool dead = false;
    float originalWdth = 1920.0f;
    float originalHeight = 1080.0f;
    Vector3 scale;
    public GUIStyle text;
    public Texture2D bg;
    public Sprite deadSpr;
    SceneManager sc = new SceneManager();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        if (dead == true)
        {
            KillPlayer();
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Restart");
                revivePlayer();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}
    void KillPlayer()
    {
        if(this.GetComponent<PlayerAnimate>().enabled == true)
        {
            PlayerAnimate pa = this.GetComponent<PlayerAnimate>();
            playermovement pm = this.GetComponent<playermovement>();
            rotateToCursor rot = this.GetComponent<rotateToCursor>();
            WeaponAttack wa = this.GetComponent<WeaponAttack>();
            LegsDirection legDir = this.GetComponentInChildren<LegsDirection>();
            wa.dropWeapon();
            pa.legs.sprite = null;
            pa.legs.enabled = false;
            legDir.enabled = false;

            pa.torso.sprite = deadSpr;
            pa.enabled = false;
            rot.enabled = false;
            wa.enabled = false;
            pm.enabled = false;
            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            col.enabled = false;
        }
    }

    public void revivePlayer()
    {
        PlayerAnimate pa = this.GetComponent<PlayerAnimate>();
        playermovement pm = this.GetComponent<playermovement>();
        rotateToCursor rot = this.GetComponent<rotateToCursor>();
        WeaponAttack wa = this.GetComponent<WeaponAttack>();
        LegsDirection legDir = this.GetComponentInChildren<LegsDirection>();

        wa.dropWeapon();
        pa.legs.enabled = true;
        legDir.enabled = true;
        
        pa.enabled = true;
        rot.enabled = true;
        wa.enabled = true;
        pm.enabled = true;
        dead = false;

    }
    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWdth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;

        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        if(dead == true)
        {
            Rect posForStart = new Rect(100, originalHeight - 200, 500, 150);
            GUI.DrawTexture(posForStart, bg);
            GUI.Box(posForStart, "Press R to restart", text);
        }
        GUI.matrix = svMat;
    }
}
