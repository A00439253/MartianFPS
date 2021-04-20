using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyFactory : MonoBehaviour
{



    private void Start()
    {

        IncreasePlayerHealthProperty increasePlayerHealthProperty = new IncreasePlayerHealthProperty();
        ReducePlayerHealthProperty reducePlayerHealthProperty = new ReducePlayerHealthProperty();

        increasePlayerHealthProperty.UpdateProperty();
        reducePlayerHealthProperty.UpdateProperty();

        foreach (var customerProperty in CustomerProperty.customProperties)
        {
            Debug.Log(" customerProperty.Value : " + customerProperty.Key);
        }



        (CustomerProperty.customProperties[EnumProperties.IncreaseHealth]).UpdateProperty();


    }

}


public enum EnumProperties
{
    None = 0,
    ReduceHealth,
    IncreaseHealth,
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
        Debug.Log("Reducing!");
        PlayerProperties.Instance.health--;
    }
}
public class IncreasePlayerHealthProperty : CustomerProperty
{
    public override EnumProperties propertyName => EnumProperties.IncreaseHealth;

    public override void UpdateProperty()
    {
        Debug.Log("Increasing!");
        PlayerProperties.Instance.health++;

    }
}