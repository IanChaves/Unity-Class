using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPedra : MonoBehaviour
{
    private float currentTime;
    public float deleteTime;
    public bool isTrigger;
    private Animator pedraAnimator;
    private bool isBarreira;
    public Vector3 velocity;



    // Start is called before the first frame update
    void Start()
    {
        isBarreira = false;
        currentTime = 0f;
        pedraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pedraAnimator.SetBool("isBarreira", isBarreira);
        isTrigger = true;
        currentTime += Time.deltaTime;
        if (currentTime >= deleteTime)
        {
            Destroy(transform.gameObject);
        }
        velocity = transform.GetComponent<Rigidbody2D>().velocity/5f;
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.transform.tag == "Barreira")
        {
            StartCoroutine(DestroyPedraAPedra());
            isBarreira = true;

            transform.GetComponent<Rigidbody2D>().velocity = velocity;


        }
    }

    public IEnumerator DestroyPedraAPedra()
    {
        yield return new WaitForFixedUpdate();
        transform.GetComponentInParent<Collider2D>().enabled = false;
    }

}
