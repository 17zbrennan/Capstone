using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private float hInput;
    private float jumpAmount;
    private float punchDirection;
    private bool punch;
    private GameObject temp;

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
        transform.rotation = Quaternion.Euler(new Vector3(0, 140, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
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

    void Punching()
    {
        if (Input.GetKeyDown(KeyCode.Space) && punch == false)
        {
            temp = Instantiate(punchBox, new Vector3(transform.position.x + punchDirection, transform.position.y + 2, transform.position.z), Quaternion.identity);
            temp.transform.parent = this.transform;
            anim.SetTrigger("isPunching");
            punch = true;
            StopCoroutine("Punch");
            StartCoroutine("Punch");

        }

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
        if (hInput < 0) //Left
        {
            punchDirection = -1.3f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 220, 0));
        }
        else if (hInput > 0) //Right
        {
            punchDirection = 1.3f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 140, 0));
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpAmount = 3;
        }
    }

   
    IEnumerator Punch()
    {
        yield return new WaitForSeconds(1.4f);
        punch = false;
        Destroy(temp);
        yield return null;

    }
}
