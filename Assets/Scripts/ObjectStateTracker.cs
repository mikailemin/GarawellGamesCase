using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateTracker : MonoBehaviour
{
    [Tooltip("Bu degişken,acik kapilari yukardan başlar ve saat yönünün tersine göre degerler veriler")]
    public bool[] doorState = new bool[4]; // 4 kapının açık/kapalı durumu


    public GameObject[] doorObject;

    private void Start()
    {
        for (int i = 0; i < doorObject.Length; i++)
        {
            if (doorObject[i]!=null)
            {
                doorObject[i].GetComponent<StickReferans>().objectStates.Add(this);
                doorObject[i].GetComponent<StickReferans>().statesvalue.Add(i);
            }
        }
    }
}

