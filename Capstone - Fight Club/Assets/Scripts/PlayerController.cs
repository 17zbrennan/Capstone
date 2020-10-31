//Zachary Brennan; 11/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private NetworkAnimator netAnim;
    private float hInput;
    private float jumpAmount;
    private bool punch;
    private GameObject temp;
    private bool attack;
    private Vector3 spawn;
    private float gravity;
    private bool outOfBounds;

    [SerializeField]
    public GameObject specialAttack;
    [SerializeField]
    public GameObject punchBox;
    public float jumpHeight;
    public float speed;
    public float punchDirection;
    // Use this for initialization
    void Start()
    {
        outOfBounds = false;
        gravity = 9.81f;
        spawn = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        attack = false;
        punchDirection = 1.5f;
        jumpAmount = 3;
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        netAnim = this.GetComponent<NetworkAnimator>();
        transform.rotation = Quaternion.Euler(new Vector3(0, 140, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (isLocalPlayer)
        {
            if (outOfBounds == false)
            {
                rb.AddForce(new Vector3(0, -1 * gravity));
                //Movement inputs
                hInput = Input.GetAxis("Horizontal");
                ScaleFlip();
                //Vector 3 of the movement and moving the controller.
                Vector3 movement = new Vector3(hInput, 0);
                rb.AddForce(movement * speed);

                if (hInput > 0 || hInput < 0)
                {
                    anim.SetBool("isWalking", true);
                }
                else
                {
                    anim.SetBool("isWalking", false);
                }
                Jumping();
                Attack();
                Punching();
            }
        }
    }
    [Client]
    void Punching()
    {
        if (Input.GetKeyDown(KeyCode.Space) && punch == false)
        {
                netAnim.SetTrigger("isPunching");
                punch = true;
                CmdPunch(new Vector3(transform.position.x + punchDirection, transform.position.y + 2, transform.position.z), this.gameObject);
        }
    }
    [Client]
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && attack == false)
        {
            attack = true;
            CmdAttack(new Vector3(transform.position.x, transform.position.y+2, transform.position.z), this.gameObject);
        }
    }
    public void SetPunch(bool p)
    {
        punch = p;
    }
    public void SetAttack(int c)
    {
        StartCoroutine(Cooldown(c));
        //StopCoroutine("Cooldown");
    }
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpAmount > 0)
        {
            rb.AddForce(new Vector3(hInput, 20) * jumpHeight);
            jumpAmount--;
        }
    }

    void ScaleFlip()
    {
        NetworkTransform t = this.GetComponent<NetworkTransform>();
        if (hInput < 0) //Left
        {
            punchDirection = -1.3f;
            t.transform.rotation = Quaternion.Euler(new Vector3(0, 220, 0));
           
        }
        else if (hInput > 0) //Right
        {
            punchDirection = 1.3f;
            t.transform.rotation = Quaternion.Euler(new Vector3(0, 140, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayArea")
        {
            StartCoroutine("Respawn");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpAmount = 3;
        }
    }
    IEnumerator Cooldown(int c)
    {
        yield return new WaitForSeconds(10.0f);
        attack = false;
        yield return null;
    }

    IEnumerator Respawn()
    {
        outOfBounds = true;
        yield return new WaitForSeconds(2.0f);
        outOfBounds = false;
        this.gameObject.transform.position = spawn;
        DamageReaction damage = this.GetComponent<DamageReaction>();
        damage.SetDamage(0.0f);
        damage.IncrementDeaths();
        yield return null;
    }   

    [Command]
    void CmdAttack(Vector3 spawn, GameObject parent)
    {
        temp = (GameObject)Instantiate(specialAttack, spawn, Quaternion.identity);
        temp.transform.parent = parent.transform;
        NetworkServer.Spawn(temp);
        RpcFix(parent, temp);
    }

    [Command]
    void CmdPunch(Vector3 spawn, GameObject parent)
    {
        temp = (GameObject)Instantiate(punchBox,spawn, Quaternion.identity);
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
