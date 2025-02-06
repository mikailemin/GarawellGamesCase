using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateTracker : MonoBehaviour
{

    public bool isLock;
    [SerializeField]
    private Sprite whitesprite, blackSprite;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [Tooltip("Bu degişken,acik kapilari yukardan başlar ve saat yönünün tersine göre degerler veriler")]
    public bool[] doorState = new bool[4]; // 4 kapının açık/kapalı durumu

    public GameObject[] doorObject;

    private void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        
        for (int i = 0; i < doorObject.Length; i++)
        {
            if (doorObject[i] != null)
            {
                doorObject[i].GetComponent<StickReferans>().objectStates.Add(this);
                doorObject[i].GetComponent<StickReferans>().statesvalue.Add(i);
            }
        }
    }
    public void SpriteChanges(string color)
    {
        if (!isLock)
        {
            if (color == "white")
            {
                spriteRenderer.sprite = whitesprite;
            }
            else
            {
                spriteRenderer.sprite = blackSprite;
            }
        }
       
    }
}

