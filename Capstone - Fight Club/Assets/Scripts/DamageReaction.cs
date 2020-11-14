//Zachary Brennan; 11/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class DamageReaction : NetworkBehaviour {

    private NetworkAnimator netAnim;
    [SyncVar]
    public float damageReceived;
    [SyncVar]
    public float deaths;
    private Rigidbody rb;
    private Transform stats;
    private GameObject canvas;
    private bool iFrame;
 
    //Sets variables
    void Awake()
    {

        canvas = GameObject.Find("/PlayerStats");
        deaths = 0;    
        netAnim = GetComponent<NetworkAnimator>();
        rb = this.GetComponent<Rigidbody>();
        damageReceived = 0;
    }
    //Changes the stats parent
    private void Start()
    {
        stats = transform.Find("Stats");
        stats.GetComponent<CharacterExist>().player = this.gameObject;
        stats.transform.SetParent(canvas.transform);

    }
    private void Update()
    {
        //Checks if their is a fire ability and changes the parent
        if (transform.Find("Fire(Clone)"))
        {
            transform.Find("Fire(Clone)").transform.SetParent(GameObject.Find("/TempParent").transform);
        }
        if (isLocalPlayer)
        {
            //Changes the death value and damage value for the local player
            //CmdSync(damageReceived, deaths);
            stats.transform.Find("Damage").GetComponent<Text>().text = "Damage: " + (int)damageReceived;
            stats.transform.Find("Deaths").GetComponent<Text>().text = "Deaths: " + (int)deaths;
        }
        else
        {
            //Destroys the other players ui for you
            stats.transform.gameObject.SetActive(false);
        }
        


    }
    private void OnTriggerEnter(Collider other)
    {
      //Does different things for different damage sources
        if (other.gameObject.tag == "DamageSource" && other.gameObject.transform.IsChildOf(this.transform) == false && iFrame == false)
        {
            if (other.gameObject.name == "Punch(Clone)")
            {
                //Animation
                netAnim.SetTrigger("isHit");
                //Damage
                damageReceived += Random.Range(10.0f, 17.0f);
                //Launch
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageReceived, 20 * damageReceived));
                //Destroy attack
                Destroy(other.gameObject.GetComponent<BoxCollider>());
            }
            if ( other.gameObject.name == "Spear(Clone)")
            {
                //Same as above
                netAnim.SetTrigger("isHit");
                damageReceived += Random.Range(6.0f, 15.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageReceived, 20 * damageReceived));
                Destroy(other.gameObject.GetComponent<BoxCollider>());
            }
            if (other.gameObject.name == "cactusattacktus(Clone)")
            {
                //Same as above
                netAnim.SetTrigger("isHit");
                damageReceived += Random.Range(25.0f, 35.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageReceived, 20 * damageReceived));
                Destroy(other.gameObject.GetComponent<SphereCollider>());
            }

            if (other.gameObject.name == "Fireball")
            {
                //Same as above
                netAnim.SetTrigger("isHit");
                damageReceived += Random.Range(20.0f, 25.0f);
                rb.AddForce(new Vector3(Random.Range(-20.0f, 20.0f) * damageReceived, 20 * damageReceived));
                Destroy(other.gameObject.GetComponent<SphereCollider>());
            }
            //Iframes
            iFrame = true;
            StartCoroutine("ImmunityFrame");
        }
    }

    IEnumerator ImmunityFrame()
    {
        //Iframes
        yield return new WaitForSeconds(1.0f);
        iFrame = false;
        yield return null;
    }
    
    //Sets damage, deaths and their getters
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

       //Command and RPC
   [Command]
    void CmdSync(float currentDamage, float currentDeaths)
    {
        deaths = currentDeaths;
        damageReceived = currentDamage;
        RpcSync(currentDamage, currentDeaths);
    }
    [ClientRpc]
    void RpcSync(float currentDamage, float currentDeaths)
    {
        deaths = currentDeaths;
        damageReceived = currentDamage;
    }

}
