using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbody;
    public float xval,numHits;
    public GameObject player;
    //public KeyCode LPaddleC{ get; set; }
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        xval = gameObject.transform.position.x;
        if (gameObject.tag=="Player1")
            player = GameObject.FindGameObjectWithTag("Player1");
        else if (gameObject.tag=="Player2")
            player = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {

        //paddle movement
        if (player.tag == "Player1")
        {
            if (Input.GetButton("LPaddleC") && Input.GetAxisRaw("LPaddleC")>0)
            {
                rigidbody.AddForce(0, 0, 2.5f, ForceMode.Impulse);
            }
            else if (Input.GetButton("LPaddleC") && Input.GetAxisRaw("LPaddleC")<0)
            {
                rigidbody.AddForce(0, 0, -2.5f, ForceMode.Impulse);
            }
            else
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
            }

        }else if(player.tag == "Player2")
        {
            if (Input.GetButton("RPaddleC") && Input.GetAxisRaw("RPaddleC") > 0)
            {
                rigidbody.AddForce(0, 0, 2.5f, ForceMode.Impulse);
            }
            else if (Input.GetButton("RPaddleC") && Input.GetAxisRaw("RPaddleC") < 0)
            {
                rigidbody.AddForce(0, 0, -2.5f, ForceMode.Impulse);
            }
            else
            {
                rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
        //wall
        if (gameObject.transform.position.z < -5.5f - numHits / 2.0f)
        {
            transform.position = new Vector3(xval, 0.0f, -5.5f - numHits / 2.0f);
        }else if (gameObject.transform.position.z > 5.5f + numHits / 2.0f)
        {
            transform.position = new Vector3(xval, 0.0f, 5.5f + numHits / 2.0f);
        }
        //paddle size
        if (gameObject.transform.localScale.z < 1)
        {
            gameObject.transform.localScale += new Vector3(0.0f, 0.0f, 0.5f);
        }
        if (gameObject.transform.localScale.z == 5)
        {
            numHits = 0;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && numHits <5)
        {
            numHits += 1.0f;
        }
        if (collision.gameObject.tag == "Wall")
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            
        }
    }
}
