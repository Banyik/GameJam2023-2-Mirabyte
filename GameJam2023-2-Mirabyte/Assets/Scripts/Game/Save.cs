using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    public int map;
    public string character;
    public int weapon;
    public string path;

    private void Start()
    {
        path = $"{Application.persistentDataPath}/save.save";
    }

    public void SetVariables(string character)
    {
        this.character = character;
        weapon = 0;
        map = 1;
    }
    public void SaveGame()
    {
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine($"{character};{map};{weapon}");
        sw.Close();
    }
    public void Increment()
    {
        map++;
    }
    public void DeleteSave()
    {
        File.Delete(path);
    }
    public void LoadGame()
    {
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string save = sr.ReadLine();
            sr.Close();
            string[] elements = save.Split(';');
            character = elements[0];
            map = int.Parse(elements[1]);
            weapon = int.Parse(elements[2]);
        }
    }
}
