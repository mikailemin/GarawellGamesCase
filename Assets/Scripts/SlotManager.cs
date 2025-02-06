using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameObject[] prefabs;  // Kullanılabilir prefab listesi
    public Transform[] slots;     // 3 slotun referansları
    private GameObject[] currentObjects; // Şu an slotlardaki objeler
    private int usedSlotCount = 0; // Kullanılan slot sayacı

    public static SlotManager instance;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);

        }
    }
    void Start()
    {
        currentObjects = new GameObject[slots.Length];
        FillSlots();
    }

    // 3 slotu rastgele prefablarla doldur
    void FillSlots()
    {
        usedSlotCount = 0; // Kullanım sayısını sıfırla
        for (int i = 0; i < slots.Length; i++)
        {
            SpawnPrefabInSlot(i);
        }
    }


    // Belirtilen slota rastgele bir prefab oluştur
    void SpawnPrefabInSlot(int slotIndex)
    {
        if (currentObjects[slotIndex] != null)
        {
            Destroy(currentObjects[slotIndex]); // Önceki objeyi temizle
        }

        int randomIndex = Random.Range(0, prefabs.Length); // Rastgele prefab seç
        GameObject newObject = Instantiate(prefabs[randomIndex], slots[slotIndex].position, Quaternion.identity);
        newObject.transform.SetParent(slots[slotIndex]); // Slot düzenli kalsın
        currentObjects[slotIndex] = newObject;
    }

    // Prefab kullanıldığında çağrılacak fonksiyon
    public void UseSlot()
    {
        //if (currentObjects[slotIndex] != null)
        //{
        //    Destroy(currentObjects[slotIndex]); // Kullanılan prefabı yok et
        //    currentObjects[slotIndex] = null; // Slotu boş olarak işaretle
        //}
            usedSlotCount++; // Kullanılan slot sayısını artır

        // Eğer 3 slot da boşaldıysa yeniden doldur
        if (usedSlotCount >= slots.Length)
        {
            FillSlots();
        }
    }
}
