using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraPushEnemy : MonoBehaviour
{
    public PoderesInimigo poderes;
    public bool isBarreira;
    public bool spaceCheck;
    // Start is called before the first frame update
    void Start()
    {
        isBarreira = false;
        spaceCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 5") && ((poderes.power == "SDD") || (poderes.power == "DSD") || (poderes.power == "DDS")))
        {
            spaceCheck = true;
        }

        else if (Input.GetKeyUp("joystick button 5"))
        {
            spaceCheck = false;
        }
    }

    IEnumerator OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        yield return new WaitForFixedUpdate();
        if (collision.transform.tag == "BarreiraPush")
        {
            isBarreira = true;
        }
        else
        {
            isBarreira = false;
        }

        if (isBarreira && poderes.isGrounded)
        {
            if (spaceCheck && poderes.isLookingLeft)
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(poderes.speedBarreira / 2, 0));
            }
            else if (spaceCheck && !poderes.isLookingLeft)
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-poderes.speedBarreira / 2, 0));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "BarreiraPush")
        {
            isBarreira = false;
        }
    }
}
