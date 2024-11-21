using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //when instantiated, call method to add to all units list
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
    }

    //remove when unit dead
    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject); 
    }

}