using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira : MonoBehaviour
{
    public Poderes poderes;
    public string testePower;
    public bool isBarreira;
    public Transform barreiraCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        testePower = poderes.power;
    }

    public void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collision.transform.tag == "Barreira" && poderes.power == "DDD" && poderes.isGrounded)
        {
            if (poderes.isLookingLeft)
            {
                collision.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-poderes.speedBarreira, 0));
            }
            else
            {
                collision.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(poderes.speedBarreira, 0));
            }
        }
    }

}
