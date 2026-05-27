using UnityEngine;
using UnityEditor;

public class FindMissingScripts
{
    [MenuItem("Tools/Cari Script Hilang")]
    public static void FindInScene()
    {
        GameObject[] go = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int count = 0;
        
        foreach (GameObject g in go)
        {
            Component[] components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    count++;
                    Debug.LogError($"KETEMU! Objek rusak namanya: '{g.name}'", g);
                    break;
                }
            }
        }
        Debug.Log($"Pencarian selesai. Menemukan {count} objek dengan skrip hilang.");
    }
}