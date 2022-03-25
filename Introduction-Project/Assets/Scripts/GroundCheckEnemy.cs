using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckEnemy : MonoBehaviour
{
    public PoderesInimigo player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Barreira")
        {
            player.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Barreira")
        {
            player.isGrounded = false;
        }
    }
}
