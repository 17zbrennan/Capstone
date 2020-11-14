//Zachary Brennan; 11/2020
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
        //Looks for the local player, then calls attack.
        if (isLocalPlayer)
        {
            Attack();
        }
	}

    [Client]
    void Attack()
    {
        //Does input and then the sends it to the command
        if (Input.GetKeyDown(KeyCode.LeftArrow) && attack == false)
        {
            CmdAttack(new Vector3(transform.position.x, transform.position.y, transform.position.z), this.gameObject);
        }
    }

    [Command]
    void CmdAttack(Vector3 spawn, GameObject parent)
    {
        //Instantiates the ability and does changes the parent
        temp = (GameObject)Instantiate(cactusAttacktus, spawn, Quaternion.identity);
        temp.transform.parent = parent.transform;
        //Spawns it.
        NetworkServer.Spawn(temp);
        //Calls RPC
        RpcFix(parent, temp);
    }

    [ClientRpc]
    void RpcFix(GameObject p, GameObject t)
    {
        //Changes the parent on both clients
        t.transform.parent = p.transform;
    }
}
