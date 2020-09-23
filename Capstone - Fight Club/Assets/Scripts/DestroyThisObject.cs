using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DestroyThisObject : NetworkBehaviour {

    private GameObject parent;
	// Use this for initialization
	void Start () {
        parent = this.transform.parent.gameObject;
        StartCoroutine("Punch");
	}

    IEnumerator Punch()
    {
        PlayerController p = parent.GetComponent<PlayerController>();
        p.SetPunch(false);
        yield return new WaitForSeconds(1.4f);
        NetworkServer.Destroy(this.gameObject);
        yield return null;

    }
}
