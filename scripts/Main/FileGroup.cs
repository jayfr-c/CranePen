using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class FileGroup : MonoBehaviour
{
	Action<string> _createFilescallback;
    Action<string> _loadcallback;

    public GameObject file_prefab;  

    void Start () 
    { 
    	_createFilescallback = (jsonArrayString) => {
    		StartCoroutine(LoadFilesRoutine(jsonArrayString));
    	};
        _loadcallback = (userId) => {
            Debug.Log("@_loadcallback function");
            LoadFiles(userId);
        };
        StartCoroutine(Main.Instance.GetUserId(_loadcallback));
    }
    
    public void LoadFiles(string userId) 
    { 
    	//string userId = Main.Instance.user_info.user_id;
        /*before load, user id must have data first 
          wait for login so pass callback to loginfunction 
          from that function insert information to callback*/
        Debug.Log("at loadfiles "+userId);
    	StartCoroutine(Main.Instance.database.GetFileNames(userId, _createFilescallback)); 
        //StartCoroutine(Main.Instance.database.GetFileNames("1", _createItemscallback));  
    }

    private string[] JsonToArray(string jsonArrayString)
    { 
        jsonArrayString = jsonArrayString.TrimEnd(']').TrimStart('['); 
        return jsonArrayString.Split(','); 
    }

    IEnumerator LoadFilesRoutine(string jsonArrayString)
    { 
        string[] jsonArray = JsonToArray(jsonArrayString); 
        for (int i = 0; i < jsonArray.Length; i++) 
        {
            bool isDone; 
            string file_name = JsonUtility.FromJson<FileName>(jsonArray[i]).name;
            Debug.Log(file_name);  
            /*following callback collects all three files*/
                        /*Action<string> getFileInfoCallback = (prefs, flooring, outline) => {
                            isDone = true; 
                            Debug.Log(prefs);  
                        };
                        StartCoroutine(Main.Instance.database.GetFile(file_name, getFileInfoCallback));
                        yield return new WaitUntil(()=> isDone = true);*/
            /*Instantiate files*/
            GameObject new_file = Instantiate(file_prefab);
            new_file.transform.SetParent(this.transform);
            new_file.transform.localScale = Vector3.one;
            new_file.transform.localPosition = Vector3.zero; 
            /*Fill information */
            Files file_script = new_file.GetComponent<Files>();
            file_script.SetName(file_name); 
        }
    	yield return null;

    }

    public class FileName 
    {
        public string name; 
    }
    public class FileInfo
    {
        public string details;
    }
}

