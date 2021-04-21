﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform targetToFollow;
    public Rigidbody rocketRigidBody;

    public float turnSpeed = 1f;
    public float explosionRadius = 5f;
    public float explosionForce = 50f;
    public float rocketFlySpeed = 10f;

    private Transform rocketLocalTrans;

    void Start()
    {
        if (!targetToFollow)
        {
            //TODO : Cache player and set Target to follow at runtime
            Debug.LogError("Rocket Target missing!!");
        }

        rocketLocalTrans = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        if (!rocketRigidBody) return;

        rocketRigidBody.velocity = rocketLocalTrans.forward * rocketFlySpeed;

        //Now Turn the Rocket towards the Target
        var rocketTargetRot = Quaternion.LookRotation(targetToFollow.position - rocketLocalTrans.position);

        rocketRigidBody.MoveRotation(Quaternion.RotateTowards(rocketLocalTrans.rotation, rocketTargetRot, turnSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO : Test on a moving player
     //   if (collision.gameObject.tag == "Player")
        {
            Rigidbody collidingRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            if (collidingRigidBody)
                collidingRigidBody.AddForceAtPosition(Vector3.up * 1000f, collidingRigidBody.position);


            ExplosionEmulation();

            if (collision.gameObject.tag == "Player")
                (CustomerProperty.customProperties[EnumProperties.ReduceHealth]).UpdateProperty();

            //Deactivate Rocket..
            this.gameObject.SetActive(false);
        }
    }

    private void ExplosionEmulation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

}