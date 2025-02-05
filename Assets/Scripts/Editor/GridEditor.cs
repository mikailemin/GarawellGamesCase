using UnityEditor;
using UnityEngine;

public class GridEditor : EditorWindow
{
    private int gridSizeX = 5;
    private int gridSizeY = 5;
    private float spacing = 1.1f;

    private GameObject horizontalBarPrefab;
    private GameObject verticalBarPrefab;
    private GameObject squarePrefab;

    [MenuItem("Tools/Grid Creator")]
    public static void ShowWindow()
    {
        GetWindow<GridEditor>("Grid Creator");
    }

    void OnGUI()
    {
        GUILayout.Label("Grid Ayarları", EditorStyles.boldLabel);
        gridSizeX = EditorGUILayout.IntField("Grid Genişliği:", gridSizeX);
        gridSizeY = EditorGUILayout.IntField("Grid Yüksekliği:", gridSizeY);
        spacing = EditorGUILayout.FloatField("Boşluk:", spacing);

        horizontalBarPrefab = (GameObject)EditorGUILayout.ObjectField("Yatay Çubuk Prefab", horizontalBarPrefab, typeof(GameObject), false);
        verticalBarPrefab = (GameObject)EditorGUILayout.ObjectField("Dikey Çubuk Prefab", verticalBarPrefab, typeof(GameObject), false);
        squarePrefab = (GameObject)EditorGUILayout.ObjectField("Kare Prefab", squarePrefab, typeof(GameObject), false);

        if (GUILayout.Button("Grid Oluştur"))
        {
            GenerateGrid();
        }

        if (GUILayout.Button("Grid'i Temizle"))
        {
            ClearGrid();
        }
    }

    private void GenerateGrid()
    {
        if (horizontalBarPrefab == null || verticalBarPrefab == null || squarePrefab == null)
        {
            Debug.LogError("Lütfen tüm Prefab'leri ekleyin!");
            return;
        }

        GameObject gridParent = new GameObject("Grid");

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Yatay çubuk ekleme (x yönünde)
                if (x < gridSizeX - 1) // Son sütunda yatay çubuk eklemeye gerek yok
                {
                    GameObject hBar = PrefabUtility.InstantiatePrefab(horizontalBarPrefab) as GameObject;
                    hBar.transform.position = new Vector3(x * spacing + (spacing / 2), y * spacing, 0);
                    hBar.transform.SetParent(gridParent.transform);
                }

                // Dikey çubuk ekleme (y yönünde)
                if (y < gridSizeY - 1) // Son satırda dikey çubuk eklemeye gerek yok
                {
                    GameObject vBar = PrefabUtility.InstantiatePrefab(verticalBarPrefab) as GameObject;
                    vBar.transform.position = new Vector3(x * spacing, y * spacing + (spacing / 2), 0);
                    vBar.transform.rotation = Quaternion.identity; // Dikey çubuk olduğu için döndürme gerekmiyor
                    vBar.transform.SetParent(gridParent.transform);
                }

                // Kare ekleme (Dört kenar tamamlandıysa)
                if (x > 0 && y > 0)
                {
                    GameObject square = PrefabUtility.InstantiatePrefab(squarePrefab) as GameObject;
                    square.transform.position = new Vector3((x - 0.5f) * spacing, (y - 0.5f) * spacing, 0);
                    square.transform.SetParent(gridParent.transform);
                }
            }
        }
    }

    private void ClearGrid()
    {
        GameObject existingGrid = GameObject.Find("Grid");
        if (existingGrid != null)
        {
            DestroyImmediate(existingGrid);
        }
    }
}
