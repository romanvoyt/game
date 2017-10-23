using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour {
    
    public GameObject [] enemies;
    public static bool dead = false;
    float originalWdth = 1920.0f;
    float originalHeight = 1080.0f;
    Vector3 scale;
    public GUIStyle text;
    public Texture2D bg;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Restart");
                SceneManager.LoadScene("Menu");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWdth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;

        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        if (enemies.Length == 0)
        {
            Rect posForStart = new Rect(400, originalHeight - 1000, 1200, 500);
            GUI.DrawTexture(posForStart, bg);
            GUI.Box(posForStart, "You have finished all levels, congratulations!\n" + " Press 'G' to go main menu", text);

        }
        GUI.matrix = svMat;
    }

}

