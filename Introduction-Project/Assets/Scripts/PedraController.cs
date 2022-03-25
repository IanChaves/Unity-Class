using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PedraController : MonoBehaviour
{
    private Animator pedraAnimator;
    public bool isBarreira;

    // Start is called before the first frame update
    void Start()
    {
        isBarreira = false;
        pedraAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pedraAnimator.SetBool("isBarreira", isBarreira);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Barreira")
        {
            StartCoroutine(DestroyPedra());
            isBarreira = true;
            transform.GetComponentInParent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }

    public IEnumerator DestroyPedra()
    {
        yield return new WaitForFixedUpdate();
        transform.GetComponentInParent<Collider2D>().enabled = false;
    }
}
