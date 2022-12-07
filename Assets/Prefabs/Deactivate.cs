using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{

    bool inactiveSet = false;

    private void OnCollisionExit(Collision collision)
    {
        if (PlayerController.isDead) return;
        if(collision.gameObject.tag=="Player" && !inactiveSet)
        {
            Invoke("SetInactive", 4.0f);
            inactiveSet = true;
        }
    }
    void SetInactive()
    {
        this.gameObject.SetActive(false);
        inactiveSet = false;
    }

}
