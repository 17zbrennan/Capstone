using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fire: MonoBehaviour {
    public string direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        
            if (direction == "right")
            {
                rb.AddForce(new Vector2(1, 0) * 5);
            }
            else
            {
                rb.AddForce(new Vector2(-1, 0) * 5);
            }
    }
}
