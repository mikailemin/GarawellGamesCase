using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAndCol : MonoBehaviour
{
    public List<Row> row;
    public List<Row> col;

    public static RowAndCol instance;
    private void Awake()
    {
        if (instance!=null)
        {
            instance= this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
[System.Serializable]
public class Row
{
  
    public List<GameObject> col;
}
[System.Serializable]
public class Col
{
    
    public List<GameObject> col;
}