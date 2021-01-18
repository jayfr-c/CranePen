using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class MeasurementWidth : MonoBehaviour, IPointerClickHandler
{
	public MeasurementGroup measurementGroup;

    public PrefHandler prefHandler;

    public Toggle toggle;
    
    private void Start(){
    	measurementGroup.Subscribe(this);
    }

	public void OnPointerClick(PointerEventData eventData){
    	measurementGroup.OnToggleActive(this);
    }
 	
 	public void Activate(){
 		toggle.SetIsOnWithoutNotify(true);
 	}

 	public void Deactivate(){
 		toggle.SetIsOnWithoutNotify(false);
 	}
}
