using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;
    public static bool isDead = false;
    Rigidbody rb;

    public GameObject magic;
    public Transform magicStartPos;
    Rigidbody magicRb; 

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fire" || collision.gameObject.tag == "Wall")
        {
            anim.SetTrigger("isDead");
            isDead = true;
        }
        else
        {
            currentPlatform = collision.gameObject;
        }
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
        player = this.gameObject;
        GenerateWorld.RunRunner();
        startPosition = player.transform.position;
        rb = this.GetComponent<Rigidbody>();
        magicRb = magic.GetComponent<Rigidbody>();
    }

    void CastSpell()
    {
        magic.transform.position = magicStartPos.position;
        magic.SetActive(true);
        magicRb.AddForce(this.transform.forward * 4000);
        Invoke("killMagic", 1);

    }
    void KillMagic()
    {
        magic.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is BoxCollider  && GenerateWorld.lastPlatform.tag != "platformTSection")
        {
            GenerateWorld.RunRunner();
        }
       
        if(other is SphereCollider)
        {
            canTurn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other is SphereCollider)
        {
            canTurn = false;
        }
    }

    public void StopJump()
    {
        anim.SetBool("isJumping", false);
    }

    public void StopMagic()
    {
        anim.SetBool("isMagic", false);
    }

    void Update()
    {
        if (PlayerController.isDead) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 200);
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isMagic", true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.runner.transform.forward = -this.transform.forward;
            GenerateWorld.RunRunner();

            if(GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunRunner();
            

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.runner.transform.forward = -this.transform.forward;
            GenerateWorld.RunRunner();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunRunner();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-1f, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(1f, 0, 0);
        }
    }
}
