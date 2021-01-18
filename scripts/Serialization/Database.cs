using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using System;

public class Database : MonoBehaviour
{
    void Start()
    {
        /*StartCoroutine(GetUsers());
        StartCoroutine(Login("null", "null")); 
        StartCoroutine(Register("newdraft", "asdfsdf"));*/
    }
 
    public IEnumerator GetUsers() 
    {
    	using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/cranepen/GetUsers.php")) 
    	{
    		yield return www.Send();

    		if (www.isNetworkError || www.isHttpError) 
    		{
    			Debug.Log(www.error);
    		} 
    		else 
    		{
    			/*Show results as text
    			  or retrieve results as binary data*/
    			Debug.Log(www.downloadHandler.text);

    			byte[] results = www.downloadHandler.data; 
    		}
    	}
    }

    public IEnumerator Login(string username, string password)
    {
    	WWWForm form = new WWWForm();
    	form.AddField("login_user", username);
    	form.AddField("login_password", password);

    	using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/Login.php", form))
    	{
    		yield return www.SendWebRequest();

    		if (www.isNetworkError || www.isHttpError) 
    		{
    			Debug.Log(www.error);
    		}
    		else 
    		{
    			string data = www.downloadHandler.text;  
                Debug.Log("at login "+data);
                if(data == "0" || data == "-1") 
                {
                    if(data == "0")  Main.Instance.WrongCredentials();
                    if(data == "-1") Main.Instance.UsernameNotFound();
                }   
                else 
                { 
                    Main.Instance.user_info.SetCredentials(username, password);
                    Main.Instance.user_info.SetID(data);
                    //Main.Instance.Forward();
                    Main.Instance.authorization.SetActive(false);
                }
    		}
    	}
    }
    public IEnumerator Register(string username, string password, string password2)
    {
    	WWWForm form = new WWWForm();
    	form.AddField("login_user", username);
        if(password.Equals(password2)) 
        {
            form.AddField("login_password", password);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/Register.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError) 
                {
                    Debug.Log(www.error);
                }
                else 
                { 
                    string data = www.downloadHandler.text;  
                    Debug.Log("at register "+data);
                    if(data == "0") Main.Instance.RegisterError();
                    if(data == "-1") Main.Instance.UsernameTaken();
                    else 
                    { 
                        Main.Instance.user_info.SetCredentials(username, password);
                        Main.Instance.user_info.SetID(data);
                        Main.Instance.Forward();
                    }
                }
            }
        }
    	else 
        {
            Main.Instance.PasswordUnequal();
        }
    }
    public IEnumerator GetFileNames(string userID , System.Action<string> callback) 
    { 
        WWWForm form = new WWWForm();
        form.AddField("user_id", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/GetFileNames.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) { 
                Debug.Log(www.error);
            }
            else
            { 
                string name_string = www.downloadHandler.text; 
                callback(name_string);
            }
        }
    }
    /*getfiles uses the output => name from the getfilenames*/
  /*  public IEnumerator GetFiles(string fileName, System.Action<string> callback)
    {
        string prefs, outline, flooring; 

        Action<string> _getPref = (_prefs) => {return prefs;};
        Action<string> _getOutline = (_outline) => {return outline;};
        Action<string> _getFloor = (_flooring) =>  {return flooring;};

        StartCoroutine(GetPref(fileName, _getPref));
        StartCoroutine(GetOutline(fileName, _getOutline));
        StartCoroutine(GetFloor(fileName, _getFLoor));

        yield return null; 
    }*/
    public IEnumerator GetPref(string fileName, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("file_name", fileName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/GetPrefs.php", form))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {Debug.Log(www.error);}
            else
            {
                string details_string = www.downloadHandler.text;
                callback(details_string);
            }
        }
        yield return null;   
    }
    public IEnumerator SavePref(string file_name, string file_type, string userID, string detailsJson) 
    {
        WWWForm form = new WWWForm();
        form.AddField("file_name", file_name);
        form.AddField("file_type", file_type);
        form.AddField("user_id", userID);
        form.AddField("details_json", detailsJson);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/SavePrefs.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) { 
                Debug.Log(www.error);
            }
            else
            { 
                Debug.Log("saving outline");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator GetOutline(string fileName, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("file_name", fileName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/GetOutline.php", form))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {Debug.Log(www.error);}
            else
            {
                string details_string = www.downloadHandler.text;
                callback(details_string);
            }
        }
        yield return null;   
    }
    public IEnumerator SaveOutline(string filename, string file_type, string userID, string detailsJson) 
    {
        WWWForm form = new WWWForm();
        form.AddField("file_name", filename);
        form.AddField("file_type", file_type);
        form.AddField("user_id", userID);
        form.AddField("details_json", detailsJson);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/SaveOutline.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError) { 
                Debug.Log(www.error);
            }
            else
            { 
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator GetFloor(string fileName, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("file_name", fileName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cranepen/GetFloor.php", form))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) {Debug.Log(www.error);}
            else
            {
                string details_string = www.downloadHandler.text;
                callback(details_string);
            }
        }
        yield return null;   
    }
}
