using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 startPosition;   // Nesnenin başlangıç pozisyonu
    private bool isDragging = false;
    private Vector3 offset;
    public ObjectValue objectValue;
  
    [HideInInspector]
    public ObjectStateTracker currentTracker; // Anlık temas edilen ObjectStateTracker
    [HideInInspector]
    public KareControl currentKare; // Anlık temas edilen ObjectStateTracker
    public bool[] keyState = new bool[4];
    public GameObject[] keyObject = new GameObject[4];
   

  
    private void OnMouseDown()
    {

        isDragging = true;
        startPosition = transform.position;

        // Fare pozisyonunu alıp nesnenin biraz yukarısında başlatıyoruz
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        offset = transform.position - mousePos;
        offset.y += 3.5f;  // Nesneyi biraz yukarı kaldır (0.5 birim yukarı)
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (objectValue == ObjectValue.u)
        {
            if (currentKare != null && IsDoorStateValidU(currentKare))
            {
                for (int i = 0; i < keyObject.Length; i++)
                {
                    if (keyObject[i] != null)
                    {
                        keyObject[i] = null;
                    
                        currentKare.stick[i].SetActive(true);
                        currentKare.stick[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.9f);
                       currentKare.stick[i].GetComponent<StickReferans>().StickOpen();
                       
                       
                    }


                }
             
                SlotManager.instance.UseSlot();
                gameObject.SetActive(false);
           
            }
            else
            {
                transform.position = startPosition;
                return;
            }
           
           
        }
        else if (currentTracker != null && IsDoorStateValid(currentTracker))
        {
          
            for (int i = 0; i < keyObject.Length; i++)
            {
                if (keyObject[i] != null)
                {
                    keyObject[i] = null;
                    
                    currentTracker.doorObject[i].SetActive(true);
                    currentTracker.doorObject[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.9f);
                    currentTracker.doorObject[i].GetComponent<StickReferans>().StickOpen();
                   
                }


            }
         
            SlotManager.instance.UseSlot();
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = startPosition; // Uygun değilse eski yerine dön
        }

    }

    // Trigger ile ObjectStateTracker'ı algılar
    public bool IsDoorStateValidU(KareControl tracker)
    {
        for (int i = 0; i < tracker.doorState.Length; i++)
        {
            if (keyState[i] == false)
            {
               
                continue;
            }
            if (keyState[i] != tracker.doorState[i])
            {

               
                return false;
            }
            else
            {
                keyObject[i] = tracker.stick[i];

            }
        }
        return true;
    }

    // Hedef objenin doorState değerlerini kontrol et
    public bool IsDoorStateValid(ObjectStateTracker tracker)
    {

        if (objectValue == ObjectValue.i || objectValue == ObjectValue.y)
        {
            for (int i = 0; i < tracker.doorState.Length; i++)
            {
                if (keyState[i] == false)
                {

                    continue;
                }
                if (keyState[i] == tracker.doorState[i])
                {

                    keyObject[i] = tracker.doorObject[i];
                   
                    return true;
                }
            }
            return false;
        }
        else
        {
            for (int i = 0; i < tracker.doorState.Length; i++)
            {
                if (keyState[i] == false)
                {
                
                    continue;
                }
                if (keyState[i] != tracker.doorState[i])
                {

                  
                    return false;
                }
                else
                {
                    keyObject[i] = tracker.doorObject[i];

                }
            }
            return true;
        }


    }
}
public enum ObjectValue
{
    i, l, u, y
}