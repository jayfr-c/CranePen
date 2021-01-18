using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using CodeMonkey.Utils;

public class Area : MonoBehaviour
{
	[SerializeField] private SketchVisual sketchVisual;
	private SketchPad sketchpad;

	private void Start(){ 
		sketchpad = new SketchPad(20, 10, 10f, Vector3.zero); 
		sketchpad.SetSketchVisual(sketchVisual);
	}
	private void Update(){
		if(Input.GetMouseButtonDown(0)){
			Vector3 mousePosition = UtilsClass.GetMouseWorldPosition(); 
			sketchpad.SetTraceSprite(mousePosition, SketchPad.SketchObject.TraceSprite.Ground);
		}
	}
  /* //codemonkey   
  	public static Vector3 GetMouseWorldPosition(){
    	Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    	vec.z = 0f;
    	return vec; 
    }
    public static Vector3 GetMouseWorldPositionWithZ(){
    	return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera camera){
    	return GetMouseWorldPositionWithZ(Input.mousePosition, camera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera camera){
    	Vector3 position = camera.ScreenToWorldPoint(screenPosition);
    	return worldPosition; 
    }*/

} 