using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
	public string user_id {get; private set;}
    private string username;
    private string password;
    private string prefs_json; 
    public TMPro.TMP_Text name_holder;
    public Image img_holder; 
 
    public void SetCredentials(string username, string userpassword)
    {
    	this.username = username;
    	this.password = password;
        name_holder.text = username;
    }

    public void SetID(string id) 
    {
    	user_id = id;
    }
}
