using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DestroyThisObject : NetworkBehaviour {
    public float time;
    private GameObject parent;
	// Use this for initialization
	void Start () {
        parent = this.transform.parent.gameObject;
        StartCoroutine("Attack");
	}

    IEnumerator Attack()
    {
        PlayerController p = parent.GetComponent<PlayerController>();
        if (this.gameObject.name == "Punch(Clone)")
        {
            p.SetPunch(false);
        }
        else if (this.gameObject.name == "cactusattacktus(Clone)")
        {
            p.SetAttack();
        }
        yield return new WaitForSeconds(time);
        NetworkServer.Destroy(this.gameObject);
        yield return null;

    }
}
