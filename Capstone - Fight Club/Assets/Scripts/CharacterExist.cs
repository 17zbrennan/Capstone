//Zachary Brennan; 11/20

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterExist : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        this.transform.localScale = new Vector3(-1, 1, 0);
        this.transform.position = new Vector2(7, 15);
    }
    // Update is called once per frame
    void Update() {
        if (!player)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.position = new Vector2(player.transform.position.x + 7, this.transform.position.y);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
     }
 }

