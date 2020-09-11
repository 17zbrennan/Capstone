using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour {

    public void SwitchScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void EndGame()
    {
        Application.Quit();
    }
	
}
