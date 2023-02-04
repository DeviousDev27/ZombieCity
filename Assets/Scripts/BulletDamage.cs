using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    public int bulletDamage;
    private void OnTriggerEnter2D(Collider2D bulletHit)
    {
        if (bulletHit.tag == "Enemy")
        {

            bulletHit.GetComponent<Enemy>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if(bulletHit.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }

}
