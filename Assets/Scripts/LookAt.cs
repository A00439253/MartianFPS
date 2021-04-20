using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public Transform target;
    public float damping = 2;
    public float fireDelay = 2;

    public Weapon weapon;

    public float correctionAngle = 120;

    public bool bFreezeXZ = false;

    private void Start()
    {
        StartCoroutine("TryShooting");
    }

    private void LateUpdate()
    {
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        if (bFreezeXZ)
        {
            rotation.x = 0;
            rotation.z = 0;
        }


        rotation *= Quaternion.Euler(0, correctionAngle, 0);


        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }



    IEnumerator TryShooting()
    {
        yield return new WaitForSeconds(fireDelay);

        var rotation = Quaternion.LookRotation(target.position - transform.position);
        rotation *= Quaternion.Euler(0, correctionAngle, 0);
        float rotationDiff = (transform.rotation.eulerAngles.y - rotation.eulerAngles.y);

        if (rotationDiff < 1 && rotationDiff > -1)
        {

            weapon.shoot();
            //Debug.Log("Shooot!!!: " + rotationDiff);
            (CustomerProperty.customProperties[EnumProperties.ReduceHealth]).UpdateProperty();
        }

        StartCoroutine("TryShooting");
    }
}
