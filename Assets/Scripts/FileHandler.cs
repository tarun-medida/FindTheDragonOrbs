using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileHandler
{
    private string dirPath;
    private string fileName;


    public FileHandler(string dirPath, string fileName)
    {
        this.dirPath = dirPath;
        this.fileName = fileName;
    }

    public GameData LoadData()
    {
        string fullPath = Path.Combine(dirPath, fileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            // JSON data is string when stored in the file
            string dataToLoad = "";
            using(FileStream stream = new FileStream(fullPath,FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }
            loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

        }
        return loadedData;
    }

    public void SaveData(GameData gameData) 
    {
        string fullPath = Path.Combine(dirPath, fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        // serialize C# gameData to JSON
        string dataToStore = JsonUtility.ToJson(gameData);
        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using(StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }



}
