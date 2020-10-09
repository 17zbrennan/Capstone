using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterExist : MonoBehaviour
{
  //  public int damage;
   // public int deaths;
    public GameObject player;
    private void Start()
    {
        this.transform.localScale = new Vector3(-1, 1, 0);
        this.transform.position = new Vector2(7, 10);
    }
    // Update is called once per frame
    void Update() {
        if (!player)
        {
            Destroy(this.gameObject);
        }
        else

            //transform.Find("Damage").GetComponent<Text>().text = "Damage: " + (int)damage;
            //transform.Find("Deaths").GetComponent<Text>().text = "Deaths: " + (int)deaths;
            this.transform.position = new Vector2(player.transform.position.x + 7, this.transform.position.y);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }

