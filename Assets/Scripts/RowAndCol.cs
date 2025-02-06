using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAndCol : MonoBehaviour
{
    public List<Row> row;
    public List<Row> col;
    public List<StickReferans> tempRow;
    public List<StickReferans> tempCol;
    public List<StickReferans> AllStick;

    public List<GameObject> tempRowKare;
    public List<GameObject> tempColKare;

    private List<GameObject> getallactiveobjects;
    private List<StickReferans> getallactivesticks;

    public static RowAndCol instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void IsCompleteControlRowAndCol(int x)
    {
        if (x == 0)
        {
            for (int i = 0; i < tempRowKare.Count; i++)
            {
                tempRowKare[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            for (int k = 0; k < tempRow.Count; k++)
            {

                tempRow[k].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.6f);
                tempRow[k].gameObject.SetActive(false);
                for (int j = 0; j < tempRow[k].kareobject.Count; j++)
                {
                    tempRow[k].kareobject[j].doorState[tempRow[k].kareValue[j]] = true;

                }

            }
            for (int i = 0; i < tempRow.Count; i++)
            {
                for (int j = 0; j < tempRow[i].objectStates.Count; j++)
                {
                    tempRow[i].objectStates[j].doorState[tempRow[i].statesvalue[j]] = true;
                    tempRow[i].objectStates[j].isLock = false;
                    tempRow[i].objectStates[j].SpriteChanges("black");

                }
            }


        }
        else
        {

            for (int i = 0; i < tempColKare.Count; i++)
            {
                tempColKare[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            for (int k = 0; k < tempCol.Count; k++)
            {

                tempCol[k].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.6f);
                tempCol[k].gameObject.SetActive(false);
                for (int j = 0; j < tempCol[k].kareobject.Count; j++)
                {
                    tempCol[k].kareobject[j].doorState[tempCol[k].kareValue[j]] = true;
                }

            }
            for (int i = 0; i < tempCol.Count; i++)
            {
                for (int j = 0; j < tempCol[i].objectStates.Count; j++)
                {
                    tempCol[i].objectStates[j].doorState[tempCol[i].statesvalue[j]] = true;
                    tempCol[i].objectStates[j].isLock = false;
                    tempCol[i].objectStates[j].SpriteChanges("black");
                }
            }
        }
        getallactiveobjects = GetAllActiveObjects();
        for (int i = 0; i < getallactiveobjects.Count; i++)
        {
            for (int j = 0; j < getallactiveobjects[i].GetComponent<KareControl>().doorState.Length; j++)
            {
                getallactiveobjects[i].GetComponent<KareControl>().doorState[j] = false;
            }


        }
        getallactivesticks = GetAllActiveStick();
        for (int l = 0; l < getallactivesticks.Count; l++)
        {
            for (int i = 0; i < getallactivesticks[l].objectStates.Count; i++)
            {
                getallactivesticks[l].objectStates[i].isLock = false;
                getallactivesticks[l].objectStates[i].SpriteChanges("white");
                getallactivesticks[l].objectStates[i].isLock = true;

            }
        }
    }

    // Satır ve Sütunların tamamının aktif olup olmadığını kontrol eden fonksiyon
    public bool IsRowOrColumnComplete()
    {
        // Satırları kontrol et
        foreach (Row r in row)
        {
            if (IsAllActive(r.rowcol, 0))
            {
                Debug.Log("Bir satır tamamlandı!");
                return true;
            }
        }

        // Sütunları kontrol et
        foreach (Row c in col)
        {
            if (IsAllActive(c.rowcol, 1))
            {
                Debug.Log("Bir sütun tamamlandı!");
                return true;
            }
        }

        return false;
    }
    // Liste içindeki tüm GameObject'lerin aktif olup olmadığını kontrol eden yardımcı fonksiyon
    private bool IsAllActive(List<GameObject> objects, int x)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null && !obj.GetComponent<SpriteRenderer>().enabled)
            {
                tempCol.Clear();
                tempRow.Clear();
                return false; // Eğer herhangi biri kapalıysa false dön
            }

            if (x == 0)
            {
                for (int i = 0; i < obj.GetComponent<KareControl>().stick.Count; i++)
                {
                    tempRow.Add(obj.GetComponent<KareControl>().stick[i].GetComponent<StickReferans>());
                }
                tempRowKare.Add(obj);

            }
            else
            {
                for (int i = 0; i < obj.GetComponent<KareControl>().stick.Count; i++)
                {
                    tempCol.Add(obj.GetComponent<KareControl>().stick[i].GetComponent<StickReferans>());
                }
                tempColKare.Add(obj);
            }
        }
        IsCompleteControlRowAndCol(x);
        return true; // Hepsi aktifse true dön
    }

    // Aktif olan tüm objeleri döndüren metod
    public List<GameObject> GetAllActiveObjects()
    {
        List<GameObject> activeObjects = new List<GameObject>();

        // Satırlardaki aktif objeleri ekle
        foreach (Row r in row)
        {
            AddActiveObjects(r.rowcol, activeObjects);
        }

        // Sütunlardaki aktif objeleri ekle
        foreach (Row c in col)
        {
            AddActiveObjects(c.rowcol, activeObjects);
        }

        return activeObjects;
    }


    // Liste içinde aktif objeleri bulan ve verilen listeye ekleyen yardımcı metod
    private void AddActiveObjects(List<GameObject> objects, List<GameObject> activeList)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null && obj.GetComponent<SpriteRenderer>().enabled && !activeList.Contains(obj))
            {
                activeList.Add(obj);
            }

        }
    }
    public List<StickReferans> GetAllActiveStick()
    {
        List<StickReferans> activeObjects = new List<StickReferans>();
        foreach (StickReferans item in AllStick)
        {
            if (item!=null &&item.gameObject.activeSelf)
            {
                activeObjects.Add(item);
            }
        }

        return activeObjects;
    }
}
[System.Serializable]
public class Row
{
    public List<GameObject> rowcol;
}
