using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReleaser : MonoBehaviour
{
    public bool shooting;
    public bool firstShoot;
    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        firstShoot = false;
    }

    public void Fire() {
        if(shooting == false) {
            firstShoot = true;
        } else {
            firstShoot = false;
        }
        shooting = true;
    }

    public void StopFire() {
        shooting = false;
    }
}
