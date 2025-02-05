using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDedective : MonoBehaviour
{
    public DragAndDrop dragdrop;
    ObjectStateTracker tracker;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("girdi");
        if (tracker!=null)
        {
            for (int i = 0; i < tracker.doorState.Length; i++)
            {
                if (tracker.doorState[i])
                {
                    tracker.doorObject[i].SetActive(false);
                }
            }
        }
         tracker = other.GetComponent<ObjectStateTracker>();
        if (tracker != null)
        {
            dragdrop.currentTracker = tracker; // Mevcut Tracker'ı kaydet
            if (dragdrop.IsDoorStateValid(tracker))
            {
                //if (previewEffect != null)
                //{
                //    previewEffect.SetActive(true); // Yansımayı aç
                //    previewEffect.transform.position = tracker.transform.position; // Konum güncelle
                //}
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<ObjectStateTracker>() == dragdrop.currentTracker)
        {
            if (dragdrop.currentTracker != null && dragdrop.IsDoorStateValid(dragdrop.currentTracker))
            {
                for (int i = 0; i < dragdrop.keyObject.Length; i++)
                {
                    if (dragdrop.keyObject[i] != null)
                    {
                        dragdrop.keyObject[i].gameObject.SetActive(true);
                    }
                }
            }

        }
    }

    // Nesne çıkınca preview kapanır
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ObjectStateTracker>() == dragdrop.currentTracker)
        {
            for (int i = 0; i < dragdrop.keyObject.Length; i++)
            {
                if (dragdrop.keyObject[i] != null)
                {

                    dragdrop.keyObject[i].SetActive(false);
                    dragdrop.keyObject[i] = null;
                }
            }
            dragdrop.currentTracker = null; // Takibi bırak
                                            //  if (previewEffect != null) previewEffect.SetActive(false); // Yansımayı kapat
        }
    }
}
