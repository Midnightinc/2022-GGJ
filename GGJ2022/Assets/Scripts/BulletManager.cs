using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Action BulletHit;
    private float bullet_ForwardForce = 125f;
    private float damage = 15f;

    public AttackController wep;
    public Rigidbody rb;

    Vector3 bulletSize;
    Vector3 boxSize;

    public void Bullets()
    {
        Upgrade(BasicAmmo);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletSize = GetComponent<BoxCollider>().size;
        boxSize = new Vector3(bulletSize.x, bulletSize.y, bulletSize.z);
    }

    public void Upgrade(Action Upgrade)
    {
        BulletHit += Upgrade;
    }

    private void BasicAmmo()
    {
        GameObject t_Bullet = Instantiate<GameObject>(wep.bullets, wep.gunBarrel.transform.position, wep.gunBarrel.transform.rotation);
        Rigidbody r_temp = t_Bullet.GetComponent<Rigidbody>();
        r_temp.AddForce(transform.forward * bullet_ForwardForce);
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider[] hitsBox = Physics.OverlapBox(other.transform.position, boxSize, Quaternion.identity);
        for (int i = 0; i < hitsBox.Length; i++)
        {
            if (hitsBox[i].GetComponent<Collider>())
            {
                if (hitsBox[i].CompareTag("Enemy"))
                {
                    // Reduce Enemy health here
                    // Down below checks if the player 
                    /*if (target health = 0)
                    {
                        Destroy(hitsBox[i].gameObject);
                    }*/
                }
            }
        }
    }
    private void RicochetShot()
    {

    }

    private void PiercingShot()
    {

    }

    private void PenetrateShot()
    {

    }
}
