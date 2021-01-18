using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem 
{
	 private const string SAVE_EXTENSION = "txt";
     private static readonly string SAVE_FOLDER = Application.dataPath + "/Prefs/";
     private static readonly string SKETCH_FOLDER = Application.dataPath + "/Prefs/Sketches/";
     private static bool isInit = false;

     public static void Init()
     {
     	if(!isInit)
     	{
     		isInit = true; 
     		if(!Directory.Exists(SAVE_FOLDER))
	     	{
	     		Directory.CreateDirectory(SAVE_FOLDER);
	     	}
	     	if(!Directory.Exists(SKETCH_FOLDER))
	     	{
	     		Directory.CreateDirectory(SKETCH_FOLDER);
	     	}
     	}
     }
     //save method for prehandler 
     public static void Save(string fileName, string json, bool overwrite)
     {
     	Save(fileName, json, SAVE_FOLDER, overwrite);
     }

     public static void Save(string fileName, string json, string folder, bool overwrite)
     { 
     	Init();
     	string saveFileName = fileName;
     	if(!overwrite)
     	{  
               for(int i = 1; ; i++) 
               {
                    if(!File.Exists(folder + saveFileName +"_"+i+"." + SAVE_EXTENSION)){
                         saveFileName = fileName + "_" + i;
                         break; 
                    }
               } 
               Debug.Log("at not overwrite filename: "+saveFileName);
     	}
     	File.WriteAllText(folder + saveFileName + "." + SAVE_EXTENSION, json);
          if(File.Exists(folder + saveFileName+"." + SAVE_EXTENSION)) Debug.Log("file saved and exists");
          else {Debug.Log("Entered save function but file does not exist.");}
     }

     public static string Load(string fileName)
     {
     	return Load(fileName, SAVE_FOLDER);
     }

     public static string Load(string fileName, string folder)
     {
     	Init();
     	if(File.Exists(folder + fileName + "." + SAVE_EXTENSION))
     	{
     		string json = File.ReadAllText(folder + fileName + "." + SAVE_EXTENSION);
     		return json; 
     	} else {
     		return null; 
     	}
     }

     public static string LoadLatestFile(string fileName)
     {
     	return LoadLatestFile(SAVE_FOLDER, fileName);
     }

     public static string LoadLatestFile(string folder, string fileName)
     {
     	Init(); 
     	DirectoryInfo directoryInfo = new DirectoryInfo(folder);
     	FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt"); 
     	FileInfo latestFile = null;

     	foreach(FileInfo fileInfo in saveFiles)
     	{
     		if(latestFile==null)
     		{
     			latestFile = fileInfo;
     		} else {
     			if(fileInfo.LastWriteTime > latestFile.LastWriteTime)
	     		{
	     			latestFile = fileInfo; 
	     		}
     		} 
     	}
          
        if(!latestFile.FullName.Contains(fileName)) {
            string[] names = latestFile.FullName.Split('_');
            string latestNum = names[1].Split('.')[0];  
            foreach(FileInfo fileInfo in saveFiles)
            {
                if(fileInfo.FullName.Contains(fileName) && fileInfo.FullName.Contains(latestNum))
                {
                    Debug.Log("fileInfo: "+ fileInfo.FullName);
                    latestFile = fileInfo; 
                }
            }
        }

     	if(latestFile!=null)
     	{        
     		string json = File.ReadAllText(latestFile.FullName);
     		return json;
     	} else {
     		return null;
     	}
     }
// From this line: methods for the sketches 
     public static void SaveObject(string fileName, object saveObject)
     {
     	SaveObject(fileName, saveObject, false);
     }

     public static void SaveObject(string fileName, object saveObject, bool overwrite)
     {
     	Init();
     	string json = JsonUtility.ToJson(saveObject);
     	Save(fileName, json, SKETCH_FOLDER, overwrite); 
     }

     public static TSaveObject LoadLatestObject<TSaveObject>(string fileName)
     {
     	Init(); 
     	string json = LoadLatestFile(SKETCH_FOLDER, fileName);
     	if(json != null)
     	{      
     		TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(json);
     		return saveObject;
     	} else {
     		return default(TSaveObject);
     	}
     }

     public static TSaveObject LoadObject<TSaveObject>(string fileName)
     {
     	Init();
     	string json = Load(fileName);
     	if(json != null)
     	{
     		TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(json);
     		return saveObject;
     	} else {
     		return default(TSaveObject);
     	}
     }
}
