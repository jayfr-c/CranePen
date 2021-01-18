using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementGroup : MonoBehaviour
{

	public PrefHandler prefHandler;

     public List<MeasurementHeight> heights;
     public List<MeasurementWidth> widths;

     private MeasurementWidth selectedWidth;
     private MeasurementHeight selectedHeight;

     public List<int> correspondingValues;

     private void Start(){

     	correspondingValues.Add(30);
     	correspondingValues.Add(40);
     	correspondingValues.Add(50);
     	correspondingValues.Add(60);
     	correspondingValues.Add(70);
     	
     }

     public void Subscribe(MeasurementWidth width){

     	if(widths == null){

     		widths = new List<MeasurementWidth>();
     	}

     	widths.Add(width);
     }

     public void Subscribe(MeasurementHeight height){

     	if(heights == null){

     		 heights = new List<MeasurementHeight>();
     	}

     	heights.Add(height);
     }

     public void OnToggleActive(MeasurementWidth width){ 

     	width.Activate();

     	selectedWidth = width;
     	ResetWidths();

     	int index = width.transform.GetSiblingIndex();

     	for(int i = 0; i < correspondingValues.Count; i++){

     		if(i == index){
     			prefHandler.SetWidth(correspondingValues[i]);
     		}
     	}
     }

     public void OnToggleActive(MeasurementHeight height){

     	height.Activate();

     	selectedHeight = height;
     	ResetHeights();

     	int index = height.transform.GetSiblingIndex();

     	for(int i = 0; i < correspondingValues.Count; i++){

     		if(i == index){
     			prefHandler.SetHeight(correspondingValues[i]);
     		}
     	}
     }
 
     public void ResetWidths(){  

     	foreach(MeasurementWidth width in widths){

     		if(selectedWidth!=null && width == selectedWidth){continue;}
     		width.Deactivate();
     	}
     }

     public void ResetHeights(){  

     	foreach(MeasurementHeight height in heights){

     		if(selectedHeight!=null && height == selectedHeight){continue;}
     		height.Deactivate();
     	}
     }
}
