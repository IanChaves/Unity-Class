using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poderes : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public Transform groundCheck;

    public float speedRock;
    public float speed;
    public float jumpForce;
    public bool isLookingLeft;
    public bool isGrounded;

    public string power;
    private int count;
    public GameObject atackPedra;
    public GameObject barreiraPedra;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        speedRock *= 30;

    }

    // Update is called once per frame

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0 && isLookingLeft == true)
        {
            Flip();
        }
        else if (h < 0 && isLookingLeft == false)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
        }

        float speedY = playerRigidBody.velocity.y;

        playerRigidBody.velocity = new Vector2(h * speed, speedY);

        playerAnimator.SetInteger("h", (int)h);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedY", speedY);

        if (Input.GetKeyDown(KeyCode.A))
        {
            power += "A";
            count++;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            power += "S";
            count++;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            power += "D";
            count++;
        }

        if (count == 4)
        {
            power = power.Remove(0, 1);
            count--;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (power)
            {
                case "AAA":
                    if(isLookingLeft == true)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(atackPedra, new Vector2(transform.position.x - 0.7f, transform.position.y + 0.7f), Quaternion.identity);
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock, 0));
                    }
                    else
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector2(transform.position.x + 0.7f, transform.position.y + 0.7f), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock, 0));
                    }
                    
                    break;

                case "AAS":
                case "ASA":
                case "SAA":
                    if (isLookingLeft == true && isGrounded == true)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPedra, new Vector2(transform.position.x - 0.75f, transform.position.y + 0.7f), Quaternion.identity);
                    }
                    else if(isLookingLeft == false && isGrounded == true)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPedra, new Vector2(transform.position.x + 0.75f, transform.position.y + 0.7f), Quaternion.identity);
                    }
                    break;

                case "ASS":
                case "SAS":
                case "SSA":
                    print("poder 3");
                    break;

                case "AAD":
                case "ADA":
                case "DAA":
                    print("poder 4");
                    break;

                case "ADD":
                case "DAD":
                case "DDA":
                    print("poder 5");
                    break;

                case "SSS":
                    print("poder 6");
                    break;

                case "SSD":
                case "SDS":
                case "DSS":
                    print("poder 7");
                    break;

                case "SDD":
                case "DSD":
                case "DDS":
                    print("poder 8");
                    break;

                case "DDD":
                    print("poder 9");
                    break;

                case "ASD":
                case "ADS":
                case "SAD":
                case "SDA":
                case "DSA":
                case "DAS":
                    print("poder 10");
                    break;
            }
        }
    }

    private void Flip()
    {
        isLookingLeft = !isLookingLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x,transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }
}
