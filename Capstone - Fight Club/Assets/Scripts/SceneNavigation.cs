//Zachary Brennan; 11/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
public class SceneNavigation : NetworkBehaviour {


    //Switches Scene

    public void SwitchScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    //Ends Game
    public void EndGame()
    {
        Application.Quit();
    }



}
