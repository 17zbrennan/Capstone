using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject devilMan;
    public GameObject cactusMan;
    private GameObject player;
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
        player = null;
        music = this.GetComponent<AudioSource>();
    }
    public GameObject GetCharacter()
    {
        return player;
    }
    public void SetCharacter(string name)
    {
        if(name == "Cactus Man")
        {
            

        }
        else if (name == "Devil Man")
        {
           
  
        }
    }
    public void PlayMusic(string audioPath)
    {      
        AudioClip clip = Resources.Load<AudioClip>(audioPath);
        music.Stop();
        music.clip = clip;
        music.Play();
    }

}
