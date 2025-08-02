using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speedMult;
    public float speedFast;
    public float speedNorm;
    public GameObject gun;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedMult = 3f;
        speedNorm = speedMult;
        speedFast = speedMult * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //Sprint Key
        if(Input.GetKey(KeyCode.LeftShift))     //Hold to sprint
        {
            if(speedMult != speedFast)
            {
                speedMult = speedFast;
            }
        }
        else
        {
            speedMult = speedNorm;              //If not sprinting, return to normal
        }

        //Up Key
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += (transform.forward * Time.deltaTime) * speedMult;
        }

        //Left Key
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * Time.deltaTime * speedMult;
        }

        //Right Key
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (transform.right * Time.deltaTime) * speedMult;
        }

        //Down Key
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * Time.deltaTime * speedMult;
        }
    }
}