//Zachary Brennan; 11/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterExist : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        //Change locations
        this.transform.localScale = new Vector3(-1, 1, 0);
        this.transform.position = new Vector2(7, 15);
    }
    // Update is called once per frame
    void Update() {
        if (!player)
        {
            //If not the player destroy
            Destroy(this.gameObject);
        }
        else
        {
            //Changes the location to follow the player
            this.transform.position = new Vector2(player.transform.position.x + 7, this.transform.position.y);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
     }
 }

