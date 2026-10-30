using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject playerBall;
    public Rigidbody2D playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigid.constraints = RigidbodyConstraints2D.None;
            playerRigid.WakeUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("game over oat");

            playerBall.transform.position = new Vector2(0, 7);
            playerRigid.constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }
}
