using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraInimigo : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Space)
            && (poderes.power != "AAA"
            && poderes.power != "AAS"
            && poderes.power != "ASA"
            && poderes.power != "SAA"
            && poderes.power != "ASS"
            && poderes.power != "SAS"
            && poderes.power != "SSA"
            && poderes.power != "AAD"
            && poderes.power != "ADA"
            && poderes.power != "DAA"
            && poderes.power != "ADD"
            && poderes.power != "DAD"
            && poderes.power != "DDA"
            && poderes.power != "SSS"
            && poderes.power != "SSD"
            && poderes.power != "SDS"
            && poderes.power != "DSS"
            && poderes.power != "SDD"
            && poderes.power != "DSD"
            && poderes.power != "DDS"
            && poderes.power != "ASD"
            && poderes.power != "ADS"
            && poderes.power != "SAD"
            && poderes.power != "SDA"
            && poderes.power != "DSA"
            && poderes.power != "DAS")) { spaceCheck = true; }

        else if (Input.GetKeyUp(KeyCode.Space)
            && (poderes.power != "AAA"
            && poderes.power != "AAS"
            && poderes.power != "ASA"
            && poderes.power != "SAA"
            && poderes.power != "ASS"
            && poderes.power != "SAS"
            && poderes.power != "SSA"
            && poderes.power != "AAD"
            && poderes.power != "ADA"
            && poderes.power != "DAA"
            && poderes.power != "ADD"
            && poderes.power != "DAD"
            && poderes.power != "DDA"
            && poderes.power != "SSS"
            && poderes.power != "SSD"
            && poderes.power != "SDS"
            && poderes.power != "DSS"
            && poderes.power != "SDD"
            && poderes.power != "DSD"
            && poderes.power != "DDS"
            && poderes.power != "ASD"
            && poderes.power != "ADS"
            && poderes.power != "SAD"
            && poderes.power != "SDA"
            && poderes.power != "DSA"
            && poderes.power != "DAS")) { spaceCheck = false; }
    }

    IEnumerator OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        yield return new WaitForFixedUpdate();
        if (collision.transform.tag == "Barreira")
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
                collision.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-poderes.speedBarreira / 2, 0));
            }
            else if (spaceCheck && !poderes.isLookingLeft)
            {
                collision.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(poderes.speedBarreira / 2, 0));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Barreira")
        {
            isBarreira = false;
        }
        else
        {
            isBarreira = true;
        }
    }

}
