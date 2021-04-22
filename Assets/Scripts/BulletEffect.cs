using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{



    public GameObject particleObj;
    public GameObject rocketMesh;

    public AudioSource audioSource;
    public Rigidbody bulletRigidBody;

    public float delayToDestroyObj = 3f;
    public float explosionForce = 500f;
    public float explosionRadius = 3f;


    // Start is called before the first frame update
    void Start()
    {
        particleObj.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        ExplosionEmulation();

        if (collision.gameObject.tag == "Player")
            (CustomerProperty.customProperties[EnumProperties.ReduceHealth]).UpdateProperty();

        //Deactivate Rocket..
        rocketMesh.SetActive(false);
        particleObj.SetActive(true);

        StartCoroutine("DestroyBullet");
        StartCoroutine("DestroyRigidBody");
    }


    IEnumerator DestroyRigidBody()
    {

        audioSource.Play();

        if (bulletRigidBody == null) yield return null;

        yield return new WaitForSeconds(0.1f);
        DestroyImmediate(bulletRigidBody);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(delayToDestroyObj);
        DestroyImmediate(gameObject);
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
