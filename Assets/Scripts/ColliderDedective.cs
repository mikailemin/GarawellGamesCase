using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDedective : MonoBehaviour
{
    public DragAndDrop dragdrop;
    ObjectStateTracker tracker;
    KareControl kareTracker;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dragdrop.objectValue == ObjectValue.u)
        {
            if (other.CompareTag("Kare"))
            {
                Debug.Log("girdi");
                if (kareTracker != null)
                {
                    for (int i = 0; i < kareTracker.doorState.Length; i++)
                    {
                        if (kareTracker.doorState[i])
                        {
                            kareTracker.stick[i].SetActive(false);
                        }
                    }
                }
                kareTracker = other.GetComponent<KareControl>();
                if (kareTracker != null)
                {
                    dragdrop.currentKare = kareTracker; // Mevcut Tracker'ı kaydet
                    if (dragdrop.IsDoorStateValidU(kareTracker))
                    {
                        //if (previewEffect != null)
                        //{
                        //    previewEffect.SetActive(true); // Yansımayı aç
                        //    previewEffect.transform.position = tracker.transform.position; // Konum güncelle
                        //}
                    }
                }
                return;
            }


        }
        Debug.Log("girdi");
        if (tracker != null)
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
        if (dragdrop.objectValue == ObjectValue.u)
        {
            if (other.CompareTag("Kare"))
            {
                if (other.GetComponent<KareControl>() == dragdrop.currentKare)
                {
                    if (dragdrop.currentKare != null && dragdrop.IsDoorStateValidU(dragdrop.currentKare))
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
                return;
            }


        }
        else if (other.GetComponent<ObjectStateTracker>() == dragdrop.currentTracker)
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
        if (dragdrop.objectValue == ObjectValue.u)
        {
            if (other.CompareTag("Kare"))
            {
                if (other.GetComponent<KareControl>() == dragdrop.currentKare)
                {
                    for (int i = 0; i < dragdrop.keyObject.Length; i++)
                    {
                        if (dragdrop.keyObject[i] != null)
                        {

                            dragdrop.keyObject[i].SetActive(false);
                            dragdrop.keyObject[i] = null;
                        }
                    }
                    dragdrop.currentKare = null; // Takibi bırak
                                                 //  if (previewEffect != null) previewEffect.SetActive(false); // Yansımayı kapat
                }
                return;
            }

           
        }
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
