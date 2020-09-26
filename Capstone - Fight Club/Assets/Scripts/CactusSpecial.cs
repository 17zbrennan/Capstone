using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CactusSpecial : NetworkBehaviour {

    public GameObject cactusAttacktus;
    private bool attack;
    private GameObject temp;
	// Use this for initialization
	void Start () {
        attack = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            Attack();
        }
	}

    [Client]
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && attack == false)
        {
            CmdAttack(new Vector3(transform.position.x, transform.position.y, transform.position.z), this.gameObject);
        }
    }

    [Command]
    void CmdAttack(Vector3 spawn, GameObject parent)
    {
        temp = (GameObject)Instantiate(cactusAttacktus, spawn, Quaternion.identity);
        temp.transform.parent = parent.transform;
        NetworkServer.Spawn(temp);
        RpcFix(parent, temp);
    }

    [ClientRpc]
    void RpcFix(GameObject p, GameObject t)
    {
        t.transform.parent = p.transform;
    }
}
