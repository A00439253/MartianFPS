using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBulletFactory : BulletFactory
{
    public override BulletTypes bulletType => BulletTypes.HomingMissile;
}
