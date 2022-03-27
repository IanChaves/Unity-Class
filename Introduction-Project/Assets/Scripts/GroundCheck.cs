using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Poderes player;
    // Start is called before the first frame update
    void Start()
    {
        var collisor = player.transform.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" || collision.tag == "Barreira")
        {
            player.isGrounded = true;
        }
        if(collision.tag == "Buraco")
        {
            player.transform.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(LigaCollisor());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Barreira")
        {
            player.isGrounded = false;
        }
    }

    public IEnumerator LigaCollisor()
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.GetComponent<Collider2D>().enabled = true;
    }
}
