//Zachary Brennan; 11/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisconnectButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            if (this.gameObject.transform.Find("Button").gameObject.activeSelf == false)
            {
                this.gameObject.transform.Find("Button").gameObject.SetActive(true);
            }
        }
    }
}
