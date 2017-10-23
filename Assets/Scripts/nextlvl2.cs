using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextlvl2 : MonoBehaviour
{

    float originalWdth = 1920.0f;
    float originalHeight = 1080.0f;
    Vector3 scale;
    public GUIStyle text;
    public Texture2D bg;
    public GameObject[] enemiesOnMap;
    FindObjects findOb;
    PlayerHealth ph;
    SceneManager sm;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (enemiesOnMap.Length == 0)
        {
            Debug.Log("Go next level");
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene("scene3");
            }
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

        if (enemiesOnMap.Length == 0)
        {
            Rect posForStart = new Rect(100, originalHeight - 200, 500, 150);
            GUI.DrawTexture(posForStart, bg);
            GUI.Box(posForStart, "Press G to next level", text);

        }
        GUI.matrix = svMat;
    }
}
