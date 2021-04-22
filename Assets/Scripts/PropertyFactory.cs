using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyFactory : MonoBehaviour
{



    private void Start()
    {
        IncreasePlayerHealthProperty increasePlayerHealthProperty = new IncreasePlayerHealthProperty();
        ReducePlayerHealthProperty reducePlayerHealthProperty = new ReducePlayerHealthProperty();

        IncreasePlayerBulletsProperty increasePlayerBulletsProperty = new IncreasePlayerBulletsProperty();
        DecreasePlayerBulletsProperty decreasePlayerBulletsProperty = new DecreasePlayerBulletsProperty();

        IncreasePlayerGoodiesProperty increasePlayerGoodiesProperty = new IncreasePlayerGoodiesProperty();
    }

}


public enum EnumProperties
{
    None = 0,
    ReduceHealth,
    IncreaseHealth,
    DecreaseBullets,
    IncreaseBullets,
    IncreaseGoodies,
};


public abstract class CustomerProperty
{
    public static Dictionary<EnumProperties, CustomerProperty> customProperties;

    public CustomerProperty()
    {
        if (customProperties == null) customProperties = new Dictionary<EnumProperties, CustomerProperty>();

        customProperties.Add(propertyName, this);
    }

    public abstract EnumProperties propertyName { get; }

    public abstract void UpdateProperty();
    
}

public class ReducePlayerHealthProperty : CustomerProperty
{
    public override EnumProperties propertyName => EnumProperties.ReduceHealth;

    public override void UpdateProperty()
    {
        if(ScoreManager.Instance)
        ScoreManager.Instance.UpdateHealth(--PlayerProperties.Instance.health);
    }
}


public class IncreasePlayerHealthProperty : CustomerProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseHealth;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.health++;

    }
}

public class DecreasePlayerBulletsProperty : CustomerProperty
{
    public override EnumProperties propertyName => EnumProperties.DecreaseBullets;

    public override void UpdateProperty()
    {
        ScoreManager.Instance.UpdateBullets(--PlayerProperties.Instance.bullets);
    }
}

public class IncreasePlayerBulletsProperty : CustomerProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseBullets;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.health+=10;
        ScoreManager.Instance.UpdateBullets(PlayerProperties.Instance.bullets);

    }
}

public class IncreasePlayerGoodiesProperty : CustomerProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseGoodies;

    public override void UpdateProperty()
    {
        PlayerProperties.Instance.goodies+=20;
        ScoreManager.Instance.UpdateGoodies(PlayerProperties.Instance.goodies);

    }
}