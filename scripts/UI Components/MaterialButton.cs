using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class MaterialButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler 
{
	public Image background;

	public MaterialGroup materialGroup;
	private Button  button; 
	private string m_name; 

	public void OnPointerClick(PointerEventData eventData){
    	materialGroup.OnMatSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData){
    	materialGroup.OnMatEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData){
    	materialGroup.OnMatExit(this);
    }

	private void Start(){

		background = GetComponent<Image>();
		button = GetComponent<Button>();
		materialGroup.Subscribe(this);
		button.onClick.AddListener(() => {
			Debug.Log("button clicked! name=> "+this.m_name);
    	});
	}

	public void SetName(string name){m_name = name;} 
	public string GetName() {return this.m_name; }
}
