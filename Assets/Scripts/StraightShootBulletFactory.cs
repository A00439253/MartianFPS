using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShootBulletFactory : BulletFactory
{
    //protected GameObject bulletPrefab;
    public override BulletTypes bulletType => BulletTypes.StraightBullet;
}