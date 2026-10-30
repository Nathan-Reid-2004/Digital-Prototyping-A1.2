using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    public GameObject[] deathBalls = new GameObject[10];

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer = Time.time;

        if (timer > 0)
        {
            deathBalls[0].SetActive(true);
        }
        
        if (timer > 5)
        {
            deathBalls[1].SetActive(true);
        }

        if (timer > 10)
        {
            deathBalls[2].SetActive(true);
        }

        if (timer > 20)
        {
            deathBalls[3].SetActive(true);
        }

        if (timer > 20)
        {
            deathBalls[4].SetActive(true);
        }

        if (timer > 30)
        {
            deathBalls[5].SetActive(true);
        }

        if (timer > 35)
        {
            deathBalls[6].SetActive(true);
        }

        if (timer > 45)
        {
            deathBalls[7].SetActive(true);
        }

        if (timer > 55)
        {
            deathBalls[8].SetActive(true);
        }

        if (timer > 65)
        {
            deathBalls[9].SetActive(true);
            print("now survive crodie");
        }

    }


    
}
