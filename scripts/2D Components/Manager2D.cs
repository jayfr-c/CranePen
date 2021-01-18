using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager2D : MonoBehaviour
{
    // Start is called before the first frame update
     public void Render(){
    	Debug.Log("Rendering");
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Back(){
    	Debug.Log("Back");
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
   /* private IEnumerator Cosave(){
    	yield return new WaitForEndOfFrame();
    	Debug.Log(Application.dataPath);

    	RenderTexture.active = RTexture;

    	var texture2D = new Texture2D(RTexture.width, RTexture.height);
    	texture2D.ReadPixels(new Rext(0, 0, RTexture.width, RTexture.height), 0, 0);
    	texture2D.Apply();

    	var data = texture2D.EncodeToPNG();

    	File.WriteAllBytes(Application.dataPath + "/savedImage.png", data);
    }*/
}
