using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poderes : MonoBehaviour
{
    public string poder;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A)) 
        {
            poder += "A";
            count++;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            poder += "S";
            count++;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            poder += "D";
            count++;
        }

        if (count == 4)
        {
            poder = poder.Remove(0, 1);
            count--;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (poder)
            {
                case "AAA":
                    print("poder 1");
                    break;

                case "AAS":
                case "ASA": 
                case "SAA":
                    print("poder 2");
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
}
