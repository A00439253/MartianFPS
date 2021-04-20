using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletTypes
{
    None = 0,
    StraightBullet,
    StraightBomb,
    HomingMissile,
};


public abstract class BulletFactory: MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public static Dictionary<BulletTypes, BulletFactory> customBullets = null;

    public BulletFactory()
    {
        if (customBullets == null) customBullets = new Dictionary<BulletTypes, BulletFactory>();

        if(!customBullets.ContainsKey(bulletType)) customBullets.Add(bulletType, this);
    }

    public abstract BulletTypes bulletType { get; }
    
    // Reference to prefab.
    [SerializeField]
    private GameObject bulletPrefab;

    public GameObject GetBulletInstance(Vector3 position, Quaternion rotation)
    {
        return 
            Instantiate(
            bulletPrefab,
            position,
            rotation);
    }
    
}
