using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public Transform target;
    public float damping = 2;
    public float fireDelay = 2;
    public Animator animator;

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
        animator.SetBool("shoot", false);
        yield return new WaitForSeconds(1.3f);

        var rotation = Quaternion.LookRotation(target.position - transform.position);
        rotation *= Quaternion.Euler(0, correctionAngle, 0);
        float rotationDiff = (transform.rotation.eulerAngles.y - rotation.eulerAngles.y);
        bool bToShoot = (rotationDiff < 1 && rotationDiff > -1);

        if(bToShoot) animator.SetBool("shoot", true);
        yield return new WaitForSeconds(fireDelay - 1.3f);


        if (bToShoot)
        {
            weapon.shoot();
            (CustomerProperty.customProperties[EnumProperties.ReduceHealth]).UpdateProperty();
        }

        StartCoroutine("TryShooting");
    }
}
