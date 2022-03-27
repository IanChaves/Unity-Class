using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Poderes : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public Transform groundCheck;
    public Transform barreiraCheck;
    public GameObject atackPedra;
    public GameObject buraco;
    public GameObject enemyPlayer;
    public GameObject atackPedraGrande;
    public GameObject barreiraPedra;
    public GameObject barreiraPushPedra;
    public GameObject jumpPedra;
    public Text pontuacaoEnemy;
    public Text time;
    public PoderesInimigo poderesInimigo;

    public float speedRock;
    public float speedBarreira;
    public float speed;
    public float jumpForce;
    public float jumpRockForce;
    public float currentTime;

    public float cooldownPower1;
    public float cooldownPower2;
    public float cooldownPower3;
    public float cooldownPower4;
    public float cooldownPower5;
    public float cooldownPower6;
    public float cooldownPower7;
    public float cooldownPower10;

    public bool isLookingLeft;
    public bool isGrounded;
    public bool isBarreira;
    private bool isSecondJump;
    public bool isTimeCheck;
    public bool isRespawn;

    public string power;
    private int count;
    private int pontos;
    private int saltos;

    private bool isAvailablePower1;
    private bool isAvailablePower2;
    private bool isAvailablePower3;
    private bool isAvailablePower4;
    private bool isAvailablePower5;
    private bool isAvailablePower6;
    private bool isAvailablePower7;
    public bool isAvailablePower10;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        speedRock *= 30;
        speedBarreira *= 30;

        pontos = 0;
        saltos = 0;
        isAvailablePower1 = true;
        isAvailablePower2 = true;
        isAvailablePower3 = true;
        isAvailablePower4 = true;
        isAvailablePower5 = true;
        isAvailablePower6 = true;
        isAvailablePower7 = true;
        isAvailablePower10 = false;
        isSecondJump = false;
        isTimeCheck = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(isRespawn || poderesInimigo.isRespawn)
        {
            isRespawn = false;
            poderesInimigo.isRespawn = false;
            currentTime = 0f;
            poderesInimigo.currentTime = 0f;
        }
        currentTime += Time.deltaTime;
        if (currentTime >= cooldownPower10 && isTimeCheck)
        {
            isAvailablePower10 = true;
            isTimeCheck = false;
        }
        time.text = Mathf.RoundToInt(currentTime).ToString();
        pontuacaoEnemy.text = pontos.ToString();
        float h = Input.GetAxisRaw("HorizontalPlayer");
        if (h > 0 && isLookingLeft == true)
        {
            Flip();
        }
        else if (h < 0 && !isLookingLeft)
        {
            Flip();
        }

        if (isGrounded)
        {
            saltos = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && saltos == 0)
        {
            isSecondJump = !isSecondJump;
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
            saltos = 1;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && isSecondJump && saltos == 1)
        {
            isSecondJump = !isSecondJump;
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
            saltos = 2;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && saltos == 0 && !isGrounded)
        {
            isSecondJump = !isSecondJump;
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
            saltos = 2;
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
                    if(isLookingLeft && isAvailablePower1)
                    {
                        var tempPrefabRock = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x - 0.65f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefabRock.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock, 0));
                        StartCoroutine(StartCooldownPower1());
                    }
                    else if (!isLookingLeft && isAvailablePower1)
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 0.65f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock, 0));
                        StartCoroutine(StartCooldownPower1());
                    }
                    
                    break;

                case "AAS":
                case "ASA":
                case "SAA":
                    if (isLookingLeft && isGrounded && isAvailablePower2)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPedra, new Vector3(transform.position.x - 0.75f, transform.position.y +1.3f, -1), Quaternion.identity);
                        if (isLookingLeft) {
                            float x = tempPrefabBarreira.transform.localScale.x * -1;
                            tempPrefabBarreira.transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
                        }
                        StartCoroutine(StartCooldownPower2());
                    }
                    else if(!isLookingLeft && isGrounded && isAvailablePower2)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPedra, new Vector3(transform.position.x + 0.75f, transform.position.y + 1.3f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower2());
                    }
                    break;

                case "ASS":
                case "SAS":
                case "SSA":
                    if (isGrounded && isAvailablePower3 && poderesInimigo.isGrounded && (transform.position.x - poderesInimigo.transform.position.x > 0))
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPushPedra, new Vector3(poderesInimigo.transform.position.x - 0.75f, poderesInimigo.transform.position.y + 1.30f, -1), Quaternion.identity);
                        tempPrefabBarreira.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        StartCoroutine(StartCooldownPower3());
                    }
                    else if(isGrounded && isAvailablePower3 && poderesInimigo.isGrounded && (transform.position.x - poderesInimigo.transform.position.x < 0))
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(barreiraPushPedra, new Vector3(poderesInimigo.transform.position.x + 0.75f, poderesInimigo.transform.position.y + 1.30f, -1), Quaternion.identity);
                        tempPrefabBarreira.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        StartCoroutine(StartCooldownPower3());
                    }
                    break;

                case "AAD":
                case "ADA":
                case "DAA":
                    if (isGrounded && isAvailablePower4)
                    {   
                        var tempPrefabRock = Instantiate<GameObject>(jumpPedra, new Vector3(transform.position.x - 0.062f, transform.position.y + 0.15f, -1), Quaternion.identity);
                        playerRigidBody.AddForce(new Vector2(0, jumpRockForce));
                        StartCoroutine(StartCooldownPower4());
                    }
                    break;

                case "ADD":
                case "DAD":
                case "DDA":
                    if (isLookingLeft && isAvailablePower5)
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x - 0.65f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.5f, speedRock / 1.5f));
                        tempPrefab.transform.eulerAngles = new Vector3(0, 0, -45);
                        StartCoroutine(StartCooldownPower5());
                    }
                    else if (!isLookingLeft && isAvailablePower5)
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 0.65f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.5f, speedRock / 1.5f));
                        tempPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                        StartCoroutine(StartCooldownPower5());
                    }
                    break;

                case "SSS":
                    if (isLookingLeft && isAvailablePower6 && !isGrounded)
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x - 0.65f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.5f, -speedRock / 1.5f));
                        tempPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                        StartCoroutine(StartCooldownPower6());
                    }
                    else if (!isLookingLeft && isAvailablePower6 && !isGrounded)
                    {
                        var tempPrefab = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 0.65f, transform.position.y + 1f, -1), Quaternion.identity);
                        tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.5f, -speedRock / 1.5f));
                        tempPrefab.transform.eulerAngles = new Vector3(0, 0, -45);
                        StartCoroutine(StartCooldownPower6());
                    }
                    break;

                case "SSD":
                case "SDS":
                case "DSS":
                    if (isGrounded && isAvailablePower7 && poderesInimigo.isLookingLeft && poderesInimigo.isGrounded)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(buraco, new Vector3(poderesInimigo.transform.position.x - 0.75f, poderesInimigo.transform.position.y + 0.1f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower7());
                    }
                    else if (isGrounded && isAvailablePower7 && !poderesInimigo.isLookingLeft && poderesInimigo.isGrounded)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(buraco, new Vector3(poderesInimigo.transform.position.x + 0.75f, poderesInimigo.transform.position.y + 0.1f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower7());
                    }
                    else if (isGrounded && isAvailablePower7 && poderesInimigo.isLookingLeft && !poderesInimigo.isGrounded)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(buraco, new Vector3(poderesInimigo.transform.position.x - 0.75f, -3.115f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower7());
                    }
                    else if (isGrounded && isAvailablePower7 && !poderesInimigo.isLookingLeft && !poderesInimigo.isGrounded)
                    {
                        var tempPrefabBarreira = Instantiate<GameObject>(buraco, new Vector3(poderesInimigo.transform.position.x + 0.75f, -3.115f, -1), Quaternion.identity);
                        StartCoroutine(StartCooldownPower7());
                    }
                    break;

                case "ASD":
                case "ADS":
                case "SAD":
                case "SDA":
                case "DSA":
                case "DAS":
                    if (isLookingLeft && isAvailablePower10)
                    {
                        var tempPrefab1 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x, transform.position.y + 7, -1), Quaternion.identity);
                        tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.5f, -speedRock / 1.5f));
                        tempPrefab1.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab2 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 1f, transform.position.y + 9, -1), Quaternion.identity);
                        tempPrefab2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.6f, -speedRock / 1.6f));
                        tempPrefab2.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab3 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 1.5f, transform.position.y + 10, -1), Quaternion.identity);
                        tempPrefab3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.4f, -speedRock / 1.4f));
                        tempPrefab3.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab4 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 1.5f, transform.position.y + 12, -1), Quaternion.identity);
                        tempPrefab4.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.7f, -speedRock / 1.7f));
                        tempPrefab4.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab5 = Instantiate<GameObject>(atackPedraGrande, new Vector3(transform.position.x - 2f, transform.position.y + 15, -1), Quaternion.identity);
                        tempPrefab5.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.7f, -speedRock / 1.7f));
                        tempPrefab5.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab6 = Instantiate<GameObject>(atackPedraGrande, new Vector3(transform.position.x, transform.position.y + 14, -1), Quaternion.identity);
                        tempPrefab6.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.3f, -speedRock / 1.3f));
                        tempPrefab6.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab7 = Instantiate<GameObject>(atackPedraGrande, new Vector3(transform.position.x + 2, transform.position.y + 12, -1), Quaternion.identity);
                        tempPrefab7.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedRock / 1.3f, -speedRock / 1.3f));
                        tempPrefab7.transform.eulerAngles = new Vector3(0, 0, 45);
                        StartCoroutine(StartCooldownPower10());
                    }
                    else if (!isLookingLeft && isAvailablePower10)
                    {
                        var tempPrefab1 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x, transform.position.y + 7, -1), Quaternion.identity);
                        tempPrefab1.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.5f, -speedRock / 1.5f));
                        tempPrefab1.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab2 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 1f, transform.position.y + 9, -1), Quaternion.identity);
                        tempPrefab2.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.6f, -speedRock / 1.6f));
                        tempPrefab2.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab3 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 1.5f, transform.position.y + 10, -1), Quaternion.identity);
                        tempPrefab3.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.4f, -speedRock / 1.4f));
                        tempPrefab3.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab4 = Instantiate<GameObject>(atackPedra, new Vector3(transform.position.x + 1.5f, transform.position.y + 12, -1), Quaternion.identity);
                        tempPrefab4.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.7f, -speedRock / 1.7f));
                        tempPrefab4.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab5 = Instantiate<GameObject>(atackPedraGrande, new Vector3(transform.position.x - 2, transform.position.y + 15, -1), Quaternion.identity);
                        tempPrefab5.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.3f, -speedRock / 1.3f));
                        tempPrefab5.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab6 = Instantiate<GameObject>(atackPedraGrande, new Vector3(transform.position.x, transform.position.y + 14, -1), Quaternion.identity);
                        tempPrefab6.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.3f, -speedRock / 1.3f));
                        tempPrefab6.transform.eulerAngles = new Vector3(0, 0, 45);
                        var tempPrefab7 = Instantiate<GameObject>(atackPedraGrande, new Vector3(transform.position.x + 2, transform.position.y + 12, -1), Quaternion.identity);
                        tempPrefab7.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedRock / 1.3f, -speedRock / 1.3f));
                        tempPrefab7.transform.eulerAngles = new Vector3(0, 0, 45);
                        StartCoroutine(StartCooldownPower10());
                    }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            isRespawn = true;
            isAvailablePower10 = false;
            poderesInimigo.isAvailablePower10 = false;
            isTimeCheck = true;
            poderesInimigo.isTimeCheck = true;
            transform.position = new Vector2(-4.84f, 1.77f);
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            enemyPlayer.transform.position = new Vector2(5.43f, 1.77f);
            enemyPlayer.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            pontos += 1;

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
    public IEnumerator StartCooldownPower6()
    {
        isAvailablePower6 = false;
        yield return new WaitForSeconds(cooldownPower6);
        isAvailablePower6 = true;
    }
    public IEnumerator StartCooldownPower7()
    {
        isAvailablePower7 = false;
        yield return new WaitForSeconds(cooldownPower7);
        isAvailablePower7 = true;
    }
    public IEnumerator StartCooldownPower10()
    {
        isAvailablePower10 = false;
        yield return new WaitForSeconds(cooldownPower10);
         if (isTimeCheck)
        {
            isAvailablePower10 = false;
        }
        else
        {
            isAvailablePower10 = true;
        }
    }

}
