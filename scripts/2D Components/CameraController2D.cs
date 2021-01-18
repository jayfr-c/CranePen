using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{ 
	public Transform cameraTransform;
	public Camera camera; 
	public float normalSpeed;
	public float fastSpeed;
	public float movementSpeed; 
	public float movementTime;  
	public float rotationAmount;
	public float zoomAmount;

	public Vector3 newPosition;
	public Quaternion newRotation;
	//public Vector3 newZoom; //case perspective
	public float newZoom; // case orthographic

	public Vector3 dragStartPosition;
	public Vector3 dragCurrentPosition;

    void Start()
    {	 
    	newPosition = new Vector3(transform.position.x + 85, transform.position.y+100);   
    	newRotation = transform.rotation; 
    	//newZoom = cameraTransform.localPosition;
    	newZoom = camera.orthographicSize;
    }
    void Update(){   
    	HandleMouseInput(); 
    	HandleMovementInput();
    }
 
 	private void HandleMouseInput(){
 		if(Input.GetMouseButtonDown(1)){
 			Plane plane = new Plane(Vector3.forward, Vector3.zero); 
 			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

 			float entry;

 			if(plane.Raycast(ray, out entry)){
 				dragStartPosition = ray.GetPoint(entry);
 			}
 		}
 		if(Input.GetMouseButton(1)){ 
 			Plane plane = new Plane(Vector3.forward, Vector3.zero);

 			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

 			float entry;

 			if(plane.Raycast(ray, out entry)){
 				dragCurrentPosition = ray.GetPoint(entry);

 				newPosition = transform.position + dragStartPosition - dragCurrentPosition; 
 			}
 		}
 	}
 	private void HandleMovementInput(){
 		if(Input.GetKey(KeyCode.LeftShift)){
 			movementSpeed = fastSpeed;
 		}
 		else{
 			movementSpeed = normalSpeed; 
 		}
 		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){ 
 			newPosition += (transform.up * movementSpeed);  
 		}
 		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
 			newPosition += (transform.up * -movementSpeed);
 		}
 		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
 			newPosition += (transform.right * movementSpeed);
 		}
 		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
 			newPosition += (transform.right * -movementSpeed);
 		}
 		if(Input.GetKey(KeyCode.Q)){
 			newRotation *= Quaternion.Euler(Vector3.forward * rotationAmount);
 		}
 		if(Input.GetKey(KeyCode.E)){
 			newRotation *= Quaternion.Euler(Vector3.forward * -rotationAmount);
 		}

 		if(Input.GetKey(KeyCode.R)){
 			newZoom -= zoomAmount; //Zoom in
 		}
 		if(Input.GetKey(KeyCode.F)){
 			newZoom += zoomAmount; //Zoom out
 		}

 		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
 		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
 		//cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
 		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, newZoom, Time.deltaTime * movementTime);
 	}
    
}
