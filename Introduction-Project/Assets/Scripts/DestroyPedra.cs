using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPedra : MonoBehaviour
{
    private float currentTime;
    public float deleteTime;
    public bool isTrigger;
    public bool isBarreira;



    // Start is called before the first frame update
    void Start()
    {
        isBarreira = false;
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        isTrigger = true;
        currentTime += Time.deltaTime;
        if (currentTime >= deleteTime)
        {
            Destroy(transform.gameObject);
        }
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.transform.tag == "Barreira")
        {
            StartCoroutine(DestroyPedraAPedra());
            isBarreira = true;
            transform.GetComponentInParent<Rigidbody2D>().velocity = new Vector3(10, 0, 0);
        }
    }

    public IEnumerator DestroyPedraAPedra()
    {
        yield return new WaitForFixedUpdate();
        transform.GetComponentInParent<Collider2D>().enabled = false;
    }

}
