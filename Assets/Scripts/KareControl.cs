using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KareControl : MonoBehaviour
{
    [Tooltip("Bu degişken,acik kapilari yukardan başlar ve saat yönünün tersine göre degerler veriler")]
    public bool[] doorState = new bool[4]; // 4 kapının açık/kapalı durumu
    public List<GameObject> stick;


    private void Awake()
    {
        for (int i = 0; i < stick.Count; i++)
        {
            stick[i].GetComponent<StickReferans>().kareobject.Add(this);
            stick[i].GetComponent<StickReferans>().kareValue.Add(i);
        }
       
    }
    public void CheckCoontrol()
    {
        for (int i = 0; i < stick.Count; i++)
        {
            if (!stick[i].activeInHierarchy)
            {
                return;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        RowAndCol.instance.IsRowOrColumnComplete();
    }
}
