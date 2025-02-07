using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StickReferans : MonoBehaviour
{
    public List<KareControl> kareobject;
    public List<int> kareValue;
    public List<ObjectStateTracker> objectStates;
    public List<int> statesvalue;

    public void StickOpen()
    {
        for (int j = 0; j < objectStates.Count; j++)
        {
            objectStates[j].doorState[statesvalue[j]] = false;
            objectStates[j].SpriteChanges("white");
            objectStates[j].isLock = true;


        }
        for (int k = 0; k <kareobject.Count; k++)
        {
           kareobject[k].doorState[kareValue[k]] = false;
            kareobject[k].GetComponent<KareControl>().CheckCoontrol();
        }
    }


    public void StickControl(string color)
    {
        for (int j = 0; j < objectStates.Count; j++)
        {
            
                objectStates[j].SpriteChanges(color);
            
        }
    }
}
