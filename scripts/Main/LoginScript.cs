using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LoginScript : MonoBehaviour
{
	public TMPro.TMP_InputField username_input;
	public TMPro.TMP_InputField password_input; 
	public Button login_button; 

    void Start()
    { 
    	login_button.onClick.AddListener(() => {
    		Debug.Log(username_input.text + " "+password_input.text);
    		StartCoroutine(Main.Instance.database.Login(username_input.text, password_input.text));
    	});
    }

  
}
