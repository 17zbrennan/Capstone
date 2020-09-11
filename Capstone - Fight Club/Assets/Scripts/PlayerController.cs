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
    private float punchDirection;
    private bool punch;
    private GameObject temp;

    [SerializeField]
    public GameObject punchBox;
    public float jumpHeight;
    public float speed;
    // Use this for initialization
    void Start()
    {
 
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
            Punching();
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
    public void SetPunch(bool p)
    {
        punch = p;
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpAmount = 3;
        }
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
