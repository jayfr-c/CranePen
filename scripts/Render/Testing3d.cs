using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing3d : MonoBehaviour
{
    public GridArea<GameObject> grid;
    public GameObject gameObject;
    public int length;
    public int width;
    public float cellSize;
    public GameObject objectHandler;

    void Start()
    {	
    	length = 10;
    	width = 10;
    	cellSize = 8.3f	;   //cellsize(8.3) : scale(0.07, 0, 0.07)     
     	 newFunction();
     	//normal();
    }
 
    void Update()
    {
   		
    }
    private void normal(){
    	Vector3 position = new Vector3(0, 0, 0);
    	objectHandler = Instantiate(this.gameObject, position, Quaternion.identity);
    	objectHandler.transform.localScale += new Vector3(0.07f, 0, 0.07f);
    }
    private void minifunction(int x, int z){
    	Vector3 position = new Vector3(x  , 0, z  );
		objectHandler = Instantiate(this.gameObject, position, Quaternion.identity);
		objectHandler.transform.localScale += new Vector3(0.07f, 0, 0.07f);
		objectHandler.transform.position += Vector3.up * 0.7f;
		//objectHandler.transform.localRotation = Quaternion.Euler(0, 45, 0);
    }
    private void newFunction(){
    	for(int x = 0; x < width; x++){                  
   			for(int z = 0; z < length; z++){ 	 
   				if(x == 0 || x == (width-1) || z == 0 || z == (length-1)){  
   					minifunction(x, z);			 
   				}  
   			}
			
   		}
    }
}
