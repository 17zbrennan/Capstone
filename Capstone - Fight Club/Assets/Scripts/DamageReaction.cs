using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DamageReaction : NetworkBehaviour {

    private NetworkAnimator netAnim;
    private NetworkIdentity netIdentity;
    private float damageRecieved;
    private Rigidbody rb;

    void Start()
    {
        netIdentity = GetComponent<NetworkIdentity>();
        netAnim = GetComponent<NetworkAnimator>();
        rb = this.GetComponent<Rigidbody>();
        damageRecieved = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageSource")
        {
            if (other.gameObject.name == "Punch(Clone)")
            {
                Debug.Log(damageRecieved);
                netAnim.SetTrigger("isHit");
                damageRecieved += Random.Range(6.0f, 15.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageRecieved/3, 20 * damageRecieved/3));
 
            }
        }
    }

}
