using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{

    public float shootRate = 0.5f;
    public float shootRateTimestamp = 0.01f;
    public float shootForce = 1000f;

    public GameObject bulletPrefab;
    public Transform weaponHolder;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time > shootRateTimestamp)
            {
                GameObject gObj = Instantiate(
                    bulletPrefab, 
                    weaponHolder.position, 
                    weaponHolder.rotation);

                gObj.GetComponent<Rigidbody>().AddForce(weaponHolder.forward * shootForce);
                shootRateTimestamp = Time.time + shootRate;
            }
        }
    }
}
