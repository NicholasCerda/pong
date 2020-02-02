using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float movex, movez,spawn;
    public Vector3 rand;
    public int lscore, rscore, win, count;
    public GameObject p1, p2;
    public Text LScore, RScore,lwin,rwin;
    public string[] scores = new string[] { "nulla", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
        spawn = 1.0f;
        win = 0;
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (win == 0)
        {
            count = 0;
            rigidbody.velocity = new Vector3(0, 0, 0);
            rand = new Vector3(movex, 0, movez);
            rigidbody.velocity = rand;
        }
        else if (win == -1 || win==1)
        {
            StartCoroutine(mywait());
        }
    }
    void reset()
    {
        //reset score;
        lscore = 0;
        rscore = 0;
        LScore.text = scores[lscore];
        RScore.text = scores[rscore];
        respawn();
    }
    void respawn()
    {
        //spawn in center of game
        transform.position = new Vector3(0.0f,0.0f,0.0f);
        movex = Random.Range(1.0f, 10.0f)*spawn;
        movez = Random.Range(-10.0f, 10.0f);
        //ensure vertical movement
        while (movez == 0)
        {
            movez = Random.Range(-10.0f, -10.0f);
        }
        //ensure some speed to vertical movement
        while (Mathf.Abs(movez) < 1)
        {
            movez *= 1.1f;
        }
        rand = new Vector3(movex, 0, movez);
        p1.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        p2.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);

    }
    IEnumerator mywait()
    {
        if (win == -1)
        {
            win = 9;
            for (int i = 0; i < 5; i++)
            {
                lwin.transform.position = new Vector3(290, 250, 0);
                yield return new WaitForSeconds(1);
                lwin.transform.position = new Vector3(0, 0, -1000);
                yield return new WaitForSeconds(1);
            }
            lwin.transform.position = new Vector3(290, 250, 0);
            yield return new WaitForSeconds(4);
            lwin.transform.position = new Vector3(0, 0, -1000);
        }else if (win == 1)
        {
            win = 9;
            for (int i = 0; i < 5; i++)
            {
                rwin.transform.position = new Vector3(290, 250, 0);
                yield return new WaitForSeconds(1);
                rwin.transform.position = new Vector3(0, 0, -1000);
                yield return new WaitForSeconds(1);
            }

            rwin.transform.position = new Vector3(290, 250, 0);
            yield return new WaitForSeconds(4);
            rwin.transform.position = new Vector3(0, 0, -1000);
        }
        win = 0;
        reset();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            movez *= -1;
        }
        if (collision.gameObject.tag == "Player1")
        {
            movex = Mathf.Abs(movex)+0.5f;
            collision.gameObject.transform.localScale -= new Vector3(0.0f, 0.0f, 1.0f);
        }
        else if (collision.gameObject.tag == "Player2")
        {
            movex = -Mathf.Abs(movex)-0.5f;
            collision.gameObject.transform.localScale -= new Vector3(0.0f, 0.0f, 1.0f);
        }
        if (collision.gameObject.tag == "Goal")
        {
            if (gameObject.transform.position.x < 0)
            {
                rscore += 1;
                Debug.Log("Player1 scored against Player2, the score is (P1)" +lscore+" to (P2)"+rscore+"\n");
                spawn = -1.0f;
            }else{
                lscore += 1;
                Debug.Log("Player2 scored against Player1, the score is (P1)" + lscore + " to (P2)" + rscore + "\n");
                spawn = 1.0f;
            }
            if (lscore == 11)
            {
                //put player1 wins text
                win = -1;
                transform.position = new Vector3(0, -1000, 0);
                Debug.Log("Game Over, Left Paddle Wins");
                return;
            }
            else if (rscore == 11)//dont forget to add other stuff to reset them.
            {
                //put player2 wins text
                win = 1;
                transform.position = new Vector3(0, -1000, 0);
                Debug.Log("Game Over, Left Paddle Wins");
                return;
            }

            LScore.text = scores[lscore];
            RScore.text = scores[rscore];
            respawn();
        }
        
    }
}
