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
        if(thisUI.name == "CharacterUI")
        {
            CustomNetwork n = netManager.GetComponent<CustomNetwork>(); ;
            string name = this.gameObject.name;
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
        otherUI.SetActive(true);
        thisUI.SetActive(false);
        
    }
}
