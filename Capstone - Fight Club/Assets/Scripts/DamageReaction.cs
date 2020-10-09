using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class DamageReaction : NetworkBehaviour {

    private NetworkAnimator netAnim;
    [SyncVar]
    private float damageReceived;
    [SyncVar]
    private float deaths;
    private Rigidbody rb;
    private Transform stats;
    private GameObject canvas;
    private bool iFrame;
 
    void Awake()
    {

        canvas = GameObject.Find("/PlayerStats");
        stats = transform.Find("Stats");
        stats.GetComponent<CharacterExist>().player = this.gameObject;
        stats.transform.SetParent(canvas.transform);
        deaths = 0;    
        netAnim = GetComponent<NetworkAnimator>();
        rb = this.GetComponent<Rigidbody>();
        damageReceived = 0;
    }
    
    private void Update()
    {
        stats.transform.Find("Damage").GetComponent<Text>().text = "Damage: " + (int)damageReceived;
        stats.transform.Find("Deaths").GetComponent<Text>().text = "Deaths: " + (int)deaths;

    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "DamageSource" && other.gameObject.transform.IsChildOf(this.transform) == false && iFrame == false)
        {
            if (other.gameObject.name == "Punch(Clone)")
            {
                netAnim.SetTrigger("isHit");
                damageReceived += Random.Range(6.0f, 15.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageReceived/3, 20 * damageReceived/3));
                Destroy(other.gameObject.GetComponent<BoxCollider>());
            }

            if(other.gameObject.name == "cactusattacktus(Clone)")
            {
                netAnim.SetTrigger("isHit");
                damageReceived += Random.Range(20.0f, 35.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageReceived / 3, 20 * damageReceived / 3));
                Destroy(other.gameObject.GetComponent<SphereCollider>());
            }
            iFrame = true;
            StartCoroutine("ImmunityFrame");
        }
    }

    IEnumerator ImmunityFrame()
    {
        yield return new WaitForSeconds(1.0f);
        iFrame = false;
        yield return null;
    }
    
    public void SetDamage(float d)
    {
        damageReceived = d;
    }
    public void IncrementDeaths()
    {
        deaths++;
    }
    public float GetDamage()
    {
        return damageReceived;
    }
    public float GetDeaths()
    {
        return deaths;
    }
}
