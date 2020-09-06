using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReaction : MonoBehaviour {

    private float damageRecieved;
    private Rigidbody rb;
    private BoxCollider box;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        damageRecieved = 0;
        box = this.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageSource")
        {
            if (other.gameObject.name == "Punch(Clone)")
            {
                Debug.Log(damageRecieved);
                damageRecieved += Random.Range(4.0f, 10.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageRecieved/3, 20 * damageRecieved/3));
                box.enabled = false;
                StopCoroutine("Immunity");
                StartCoroutine("Immunity");

            }
        }
    }
    IEnumerator Immunity()
    {
        yield return new WaitForSeconds(.3f);
        box.enabled = true;
        yield return null;

    }
}
