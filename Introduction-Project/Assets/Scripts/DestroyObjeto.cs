using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjeto : MonoBehaviour
{
    private float currentTime;
    public float deleteTime;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= deleteTime)
        {
            Destroy(transform.gameObject);
        }
    }
}
