using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // grabbing transform
    private Transform target;

    // interpolation rate of camera
    public float smoothFactor = 16f;

    // 1-0 interpolation between the coordinates (0,0) and the player
    public float fromCenter = 0.2f;

    // camera offset to be modular
    public Vector3 cameraOffset = new Vector3(0,8,0);

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        float extraYPosition;
        float centerCalc = 1 / fromCenter;
        if (newPos.y > 0)
        {
            extraYPosition = newPos.y;
            fromCenter = 0.4f;
            cameraOffset = new Vector3(0, -6, 0);
        }
        else 
        extraYPosition = 0;

            transform.position = 
                Vector3.Lerp(transform.position, 
                new Vector3(newPos.x / centerCalc, extraYPosition + newPos.y / centerCalc, newPos.z) + cameraOffset, 
                Time.deltaTime * smoothFactor);
    }
}
