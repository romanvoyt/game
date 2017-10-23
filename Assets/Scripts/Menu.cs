using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button lvl1;
    public Button lvl2;
    public Button lvl3;
    public Button control;
    public Button exitBtn;
    
	// Use this for initialization
	void Start () {
        lvl1.onClick.AddListener(() => { SceneManager.LoadScene("scene");  });
        lvl2.onClick.AddListener(() => { SceneManager.LoadScene("scene2"); });
        lvl3.onClick.AddListener(() => { SceneManager.LoadScene("scene3"); });
        control.onClick.AddListener(() => { SceneManager.LoadScene("Options"); });
        exitBtn.onClick.AddListener(() => { Application.Quit(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
