using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DamageReaction : NetworkBehaviour {

    private NetworkAnimator netAnim;
    private float damageRecieved;
    private float deaths;
    private Rigidbody rb;

    void Start()
    {
        deaths = 0;    
        netAnim = GetComponent<NetworkAnimator>();
        rb = this.GetComponent<Rigidbody>();
        damageRecieved = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "DamageSource" && other.gameObject.transform.IsChildOf(this.transform) == false)
        {
            if (other.gameObject.name == "Punch(Clone)")
            {
                netAnim.SetTrigger("isHit");
                damageRecieved += Random.Range(6.0f, 15.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageRecieved/3, 20 * damageRecieved/3));
 
            }

            if(other.gameObject.name == "cactusattacktus(Clone)")
            {
                netAnim.SetTrigger("isHit");
                damageRecieved += Random.Range(20.0f, 35.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageRecieved / 3, 20 * damageRecieved / 3));
            }
        }
    }

    public void SetDamage(float d)
    {
        damageRecieved = d;
    }
    public void IncrementDeaths()
    {
        deaths++;
    }
    public float GetDamage()
    {
        return damageRecieved;
    }
    public float GetDeaths()
    {
        return deaths;
    }
}
