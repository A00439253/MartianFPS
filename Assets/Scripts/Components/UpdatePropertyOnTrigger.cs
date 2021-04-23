using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePropertyOnTrigger : MonoBehaviour
{

    public EnumProperties propertyToUpdate;
    public string tagToCompare = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            (CustomerProperty.customProperties[propertyToUpdate]).UpdateProperty();
        }
    }
}
