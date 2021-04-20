using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{

    public float shootRate = 0.5f;
    public float shootRateTimestamp = 0.01f;
    public float shootForce = 1000f;
    public Transform weaponHolder;


    public BulletTypes bulletTypes = BulletTypes.StraightBullet;
    public GameObject bulletPrefab;
    public bool bIsManual = false;

    void Update()
    {
        if(bIsManual && Input.GetKey(KeyCode.Space))
        {
            if (Time.time > shootRateTimestamp)
            {
                shoot();
            }
        }
    }

    public void shoot()
    {
        switch (bulletTypes)
        {
            case BulletTypes.None:
                break;
            case BulletTypes.StraightBullet:
                {
                    GameObject gObj = BulletFactory.customBullets[BulletTypes.StraightBullet].GetBulletInstance(weaponHolder.position,
                        weaponHolder.rotation);

                    gObj.GetComponent<Rigidbody>().AddForce(weaponHolder.forward * shootForce);
                    shootRateTimestamp = Time.time + shootRate;
                }
                break;
            case BulletTypes.StraightBomb:
                break;
            case BulletTypes.HomingMissile:
                {
                    GameObject gObj = BulletFactory.customBullets[BulletTypes.HomingMissile].GetBulletInstance(weaponHolder.position,
                    weaponHolder.rotation);

                    HomingMissile homingMissile = gObj.GetComponent<HomingMissile>();
                    homingMissile.targetToFollow = Camera.main.transform;
                    //homingMissile.targetToFollow = PlayerProperties.Instance.transform;


                }
                break;
            default:
                break;
        }

    }
}
