//Zachary Brennan; 11/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private AudioSource music;
    void Awake()
    {
        //Makes sure this stays as a singleton 
        if (instance == null)
            instance = this;
        else
            //Destroys if it isnt 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        music = this.GetComponent<AudioSource>();
    }

    //Resets the network manager
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if(GameObject.Find("NetworkManager") == true)
            {
                Destroy(GameObject.Find("NetworkManager"));
            }
        }
    }
    // Changes the music when the music choice changes
    public void PlayMusic(string audioPath)
    {      
        AudioClip clip = Resources.Load<AudioClip>(audioPath);
        music.Stop();
        music.clip = clip;
        music.Play();
    }
}
