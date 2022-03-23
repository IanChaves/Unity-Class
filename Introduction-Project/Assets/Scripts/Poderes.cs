using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poderes : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public Transform groundCheck;
    public Transform barreiraCheck;
    public GameObject atackPedra;
    public GameObject atackTreeRocks;
    public GameObject barreiraPedra;
    public GameObject jumpPedra;

    public float speedRock;
    public float speedBarreira;
    public float speed;
    public float jumpForce;
    public float jumpRockForce;

    public float cooldownPower1;
    public float cooldownPower2;
    public float cooldownPower3;
    public float cooldownPower4;
    public float cooldownPower5;
    //public float cooldownPower6;
    //public float cooldownPower7;
    //public float cooldownPower8;
    //public float cooldownPower9;
    //public float cooldownPower10;

    private bool isLookingLeft;
    private bool isGrounded;

    public string power;
    private int count;

    private bool isAvailablePower1 = true;
    private bool isAvailablePower2 = true;
    private bool isAvailablePower3 = true;
    private bool isAvailablePower4 = true;
    private bool isAvailablePower5 = true;
    //private bool isAvailablePower6 = true;
    //private bool isAvailablePower7 = true;
    //private bool isAvailablePower8 = true;
    //private bool isAvailablePower9 = true;
    //private bool isAvailablePower10 = true;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        speedRock *= 30;
        speedBarreira *= 30;

    }

    // Update is called once per frame

    void Update()
    {
        float h = Input.GetAxisRaw("HorizontalPlayer");
        if (h > 0 && isLookingLeft == true)
        {
            Flip();
        }
        else if (h < 0 && !isLookingLeft)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
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
                    if(isLookingLeft && isGrounded && isAvailablePower1)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x - 0.7f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock, 0));
                        StartCoroutine(StartCooldownPower1());
                    }
                    else if (!isLookingLeft && isGrounded && isAvailablePower1)
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 0.7f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock, 0));
                        StartCoroutine(StartCooldownPower1());
                    }
                    
                    break;

                case "AAS":
                case "ASA":
                case "SAA":
                    if (isLookingLeft && isGrounded && isAvailablePower2)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPedra, new Vector3(transform.position.x - 0.75f, transform.position.y + 0.7f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower2());
                    }
                    else if(!isLookingLeft && isGrounded && isAvailablePower2)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPedra, new Vector3(transform.position.x + 0.75f, transform.position.y + 0.7f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower2());
                    }
                    break;

                case "ASS":
                case "SAS":
                case "SSA":
                    if (isLookingLeft && isGrounded && isAvailablePower3)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(barreiraPedra, new Vector3(transform.position.x - 0.7f, transform.position.y + 0.7f, -1), Quaternion.identity);
                        tempPrefabRock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedBarreira, 0));
                        StartCoroutine(StartCooldownPower3());
                    }
                    else if (!isLookingLeft && isGrounded)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(barreiraPedra, new Vector3(transform.position.x + 0.7f, transform.position.y + 0.7f, -1), Quaternion.identity);
                        tempPrefabRock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedBarreira, 0));
                        StartCoroutine(StartCooldownPower3());
                    }
                    break;

                case "AAD":
                case "ADA":
                case "DAA":
                    if (isGrounded && isAvailablePower4)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(jumpPedra, new Vector3(transform.position.x - 0.062f, transform.position.y + 0.1273f, -1), Quaternion.identity);
                        playerRigidBody.AddForce(new Vector2(0, jumpRockForce));
                        StartCoroutine(StartCooldownPower4());
                    }
                    break;

                case "ADD":
                case "DAD":
                case "DDA":
                    if (isLookingLeft && isGrounded && isAvailablePower5)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(atackTreeRocks, new Vector3(transform.position.x - 2.7f, transform.position.y -0.8f, -1), new Quaternion(0f, 0f,0.5f, 0f));
                        tempPrefabRock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedBarreira, 0));
                        StartCoroutine(StartCooldownPower5());
                    }
                    else if (!isLookingLeft && isGrounded && isAvailablePower5)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(atackTreeRocks, new Vector3(transform.position.x + 2.7f, transform.position.y + 2.3f, -1), Quaternion.identity);
                        tempPrefabRock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedBarreira, 0));
                        StartCoroutine(StartCooldownPower5());
                    }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            transform.position = new Vector2(-3f, -2.35f);
        }
    }

    public IEnumerator StartCooldownPower1()
    {
        isAvailablePower1 = false;
        yield return new WaitForSeconds(cooldownPower1);
        isAvailablePower1 = true;
    }
    public IEnumerator StartCooldownPower2()
    {
        isAvailablePower2 = false;
        yield return new WaitForSeconds(cooldownPower2);
        isAvailablePower2 = true;
    }
    public IEnumerator StartCooldownPower3()
    {
        isAvailablePower3 = false;
        yield return new WaitForSeconds(cooldownPower3);
        isAvailablePower3 = true;
    }
    public IEnumerator StartCooldownPower4()
    {
        isAvailablePower4 = false;
        yield return new WaitForSeconds(cooldownPower4);
        isAvailablePower4 = true;
    }
    public IEnumerator StartCooldownPower5()
    {
        isAvailablePower5 = false;
        yield return new WaitForSeconds(cooldownPower5);
        isAvailablePower5 = true;
    }
    //public IEnumerator StartCooldownPower6()
    //{
    //    isAvailablePower6 = false;
    //    yield return new WaitForSeconds(cooldownPower6);
    //    isAvailablePower6 = true;
    //}
    //public IEnumerator StartCooldownPower7()
    //{
    //    isAvailablePower7 = false;
    //    yield return new WaitForSeconds(cooldownPower7);
    //    isAvailablePower7 = true;
    //}
    //public IEnumerator StartCooldownPower8()
    //{
    //    isAvailablePower8 = false;
    //    yield return new WaitForSeconds(cooldownPower8);
    //    isAvailablePower8 = true;
    //}
    //public IEnumerator StartCooldownPower9()
    //{
    //    isAvailablePower9 = false;
    //    yield return new WaitForSeconds(cooldownPower9);
    //    isAvailablePower9 = true;
    //}
    //public IEnumerator StartCooldownPower10()
    //{
    //    isAvailablePower10 = false;
    //    yield return new WaitForSeconds(cooldownPower10);
    //    isAvailablePower10 = true;
    //}

}
