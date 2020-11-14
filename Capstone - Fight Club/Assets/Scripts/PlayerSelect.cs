//Zachary Brennan; 11/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSelect : MonoBehaviour {

    public GameObject thisUI;
    public GameObject netManager;
    public GameObject otherUI;
    public GameObject player;

    public void ChangeUI()
    {
        //Sets character for the main character 
        if(thisUI.name == "CharacterUI")
        {
            CustomNetwork n = netManager.GetComponent<CustomNetwork>(); ;
            string name = this.gameObject.name;
            //Sends the character they picked
            switch (name)
            {
                case "CactusMan":
                    n.playerCharacter = "Cactus";
                    break;
                case "DevilMan":
                    n.playerCharacter = "Devil";

                    break;
            }
        }
        //changes the menu 
        otherUI.SetActive(true);
        thisUI.SetActive(false);
        
    }
}
