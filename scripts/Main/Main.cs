using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Main : MonoBehaviour
{
	public static Main Instance; 

    public Database database; 
    public UserInfo user_info; 
    private TabFunction tab_function;
    public GameObject authorization; 
    public FileGroup file_group;
   	public TMPro.TMP_Text login_prompt; 
   	public TMPro.TMP_Text register_prompt; 

    void Start() 
    {
    	database = transform.GetChild(0).GetComponent<Database>(); 
    	tab_function = GetComponent<TabFunction>();
    	Instance = this;  
        DontDestroyOnLoad (transform.gameObject);
    }

    public void WrongCredentials(){ login_prompt.text = "Wrong credentials";}
    public void UsernameNotFound(){ login_prompt.text = "Username not found";}
    public void UsernameTaken(){ register_prompt.text = "Username taken";}
    public void RegisterError(){ register_prompt.text = "Register error";}
    public void WipePrompt(){login_prompt.text = ""; register_prompt.text = "";}
    public void PasswordUnequal(){register_prompt.text = "Password unequal";}
    public void Forward(){tab_function.Forward();} 
 
    public IEnumerator GetUserId(System.Action<string> callback)
    {
        yield return new WaitUntil(() => user_info.user_id != null);
        Debug.Log("after the waituntil yield userid: "+user_info.user_id);
        callback(user_info.user_id);
    }
    /*required for coroutines to be called in monobehavior*/
    public void TilemapSave(string file_name, string file_type, string userID, string detailsJson) 
    {
        Debug.Log("at tilemap save");
        StartCoroutine(database.SaveOutline(file_name, file_type, userID, detailsJson));
    }
    
}
