using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Load(){
    	Debug.Log("Load prev");
    }
   	public void Construct(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Instruct(){
    	Debug.Log("Instructions");
    }
    public void Credits(){
    	Debug.Log("Credits");
    }
    public void Exit(){
    	Debug.Log("Exit");
    	Application.Quit();
    }
}
