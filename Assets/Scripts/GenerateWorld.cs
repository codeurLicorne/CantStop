using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    static public GameObject runner;
    static public GameObject lastPlatform;

    private void Awake()
    {
        runner = new GameObject("runner");

    }

    public static void RunRunner()
    {
        GameObject p = Pool.singleton.GetRandom();
        if (p == null) return;

        if(lastPlatform != null)
        {
            if(lastPlatform.gameObject.tag == "platformTSection")
            {
                runner.transform.position = lastPlatform.transform.position + PlayerController.player.transform.forward * 20;
            }
            else
            {
                runner.transform.position = lastPlatform.transform.position + PlayerController.player.transform.forward * 10;

            }
            
            if(lastPlatform.tag == "stairsUp")
            {
                runner.transform.Translate(0, 5, 0);
            }
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = runner.transform.position;
        p.transform.rotation = runner.transform.rotation;

        if(p.tag == "stairsDown")
        {
            runner.transform.Translate(0, -5, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = runner.transform.position;
        }
    }

}




/*
public GameObject[] platforms;

private void Start()
{
    Vector3 pos = new Vector3(0, 0, 0);
    for (int i = 0; i < 20; i++)
    {
        int platformNumber = Random.Range(0, platforms.Length);
        GameObject p = Instantiate(platforms[platformNumber], pos, Quaternion.identity);


        if (platforms[platformNumber].tag == "stairsUp")
        {
            pos.y += 5;
        }
        if (platforms[platformNumber].tag == "stairsDown")
        {
            pos.y -= 5;
            p.transform.Rotate(new Vector3(0, 180, 0));
            p.transform.position = pos;
        }

        pos.z -= 10;
    }
}




 GameObject runner;

    private void Start()
    {
        runner = new GameObject("runner");
        
        for(int i = 0; i < 30; i++)
        {

            GameObject p = Pool.singleton.GetRandom();
            if(p==null)
            {
                return;
            }
            p.SetActive(true);
            p.transform.position = runner.transform.position;
            p.transform.rotation = runner.transform.rotation;

            if (p.tag == "stairsUp")
            {
                runner.transform.Translate(0, 5, 0);
            }
            if(p.tag == "stairsDown")
            {
                runner.transform.Translate(0, -5, 0);
                p.transform.Rotate(new Vector3(0, 180, 0));
                p.transform.position = runner.transform.position;
            }
            else if (p.tag == "platformTSection")
            {
                if(Random.Range(0,2)==0)
                {
                    runner.transform.Rotate(new Vector3(0, 90, 0));
                }
                else
                {
                    runner.transform.Rotate(new Vector3(0, -90, 0));
                }
                runner.transform.Translate(Vector3.forward * -10);
            }
            runner.transform.Translate(Vector3.forward * -10);
        }
    }
*/