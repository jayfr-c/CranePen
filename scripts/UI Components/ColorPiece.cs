using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPiece : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{ 
	//display on ui display clicked box
	// send to prefhandler 


	public ColorPalette c_palette; 
	private bool c_change;
	/*color visual*/ 
	private GameObject inner_box;
	private Image box_image;
	private GameObject box_hover; 

 	/*color property*/
	public string c_name; 
	public Color32 c_values; 
	private byte r;
	private byte g;
	private byte b;
 
 	public void OnPointerClick(PointerEventData eventData)
 	{  
    	c_palette.SelectedColor(this);
    }

    public void OnPointerEnter(PointerEventData eventData){
 		c_palette.HoveredColor(this);
    }

    public void OnPointerExit(PointerEventData eventData){
 		c_palette.DeselectedColor(this);
    }

	private void Start()
	{  
		c_change = false;  
		c_palette.Subscribe(this);
		inner_box = transform.GetChild(0).gameObject;
		box_image = inner_box.GetComponent<Image>();  
		box_hover = transform.GetChild(1).gameObject; 
 
	}

	public void Off_Box()
	{
		box_hover.SetActive(false);
	}

	public void On_Box()
	{
		box_hover.SetActive(true);
	}

	private void Update()
	{	 
		if(c_change)
		{
			SetColor();
			c_change = false;
		}
	} 


	private void SetColor()
	{
		box_image.color = c_values;  
	}

	public void SetValues(string c_name, Color32 c_values)
	{ 
		this.c_name = c_name;   
		this.c_values = c_values;    
		this.r = c_values.r;
		this.g = c_values.g;
		this.b = c_values.b;

		c_change = true; 
	} 
}
