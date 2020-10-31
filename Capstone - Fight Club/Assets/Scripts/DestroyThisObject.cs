//Zachary Brennan; 11/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DestroyThisObject : NetworkBehaviour {
    public float time;
    private GameObject parentObject;
	// Use this for initialization
	void Start () {
        parentObject = this.transform.parent.gameObject;
        StartCoroutine("Attack");
    }

    //Attack based on the object
    IEnumerator Attack()
    {
        PlayerController p = parentObject.GetComponent<PlayerController>();
        if (this.gameObject.name == "Punch(Clone)" || this.gameObject.name == "Spear(Clone)")
        {
            p.SetPunch(false);
        }
        else if (this.gameObject.name == "cactusattacktus(Clone)")
        {
            p.SetAttack(10);
        }
        else if (this.gameObject.name == "Fire(Clone)")
        {
            
            p.SetAttack(8);
        }
        yield return new WaitForSeconds(time);
        NetworkServer.Destroy(this.gameObject);
        yield return null;

    }
}
