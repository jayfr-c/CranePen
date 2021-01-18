using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour
{
    public TMPro.TMP_InputField username_input;
	public TMPro.TMP_InputField password_input; 
	public TMPro.TMP_InputField password2_input;
	public Button register_button; 

    void Start()
    { 
    	register_button.onClick.AddListener(() => {
    		Debug.Log(username_input.text + " "+password_input.text);
    		StartCoroutine(Main.Instance.database.Register(username_input.text, password_input.text, password2_input.text));
    	});
    }
}
