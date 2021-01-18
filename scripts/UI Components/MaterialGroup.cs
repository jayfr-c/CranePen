using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGroup : MonoBehaviour 
{ 
	public enum Type{
		Wall,
		Floor,
		Roof
	}
	[SerializeField] public Type type; 

	public Sprite tabIdle;
	public Sprite tabHover;
	public Sprite tabActive;

	public List<MaterialButton> mat_buttons; 
	public List<string> roofs; 
	public List<string> walls;
	public List<string> floors;  

	public MaterialButton selectedmat;

	public PrefHandler prefhandler;

	void Start()
	{
		roofs.Add("Corrugated Roofing, Gauge 26 (0.551 mm x 2.44mm)");
		roofs.Add("Pre-painted Metal Roofing Sheet GA 26, Long Span");
		roofs.Add("Pre-painted Metal Roofing Sheet GA 26, 2.44m");
		roofs.Add("Pre-painted Gutter, GA 24(0.701 mm) x 2.44m");
		roofs.Add("Ordinary Gutter, GA 24 (0.701 mm) x 2.44m");
		roofs.Add("Pre-painted Flashing, GA 24 (0.701 mm) x 2.44m");
		roofs.Add("Ordinary Flashing, GA 24 (0.701 mm) x 2.44m");
		roofs.Add("Pre-painted Ridge Roll, GA 24 (0.701 mm) x 2.44m");
		roofs.Add("Ordinary Ridge Roll, GA 24 (0.701 mm) 2.44m");
		roofs.Add("Roof Ventilators");
		
		if(type == Type.Wall) SetNames(walls, mat_buttons);
		if(type == Type.Floor) SetNames(floors, mat_buttons);
		if(type == Type.Roof) SetNames(roofs, mat_buttons);
 	}
 

	public void Subscribe(MaterialButton mat){
		if(mat_buttons == null){ 
	 		mat_buttons = new List<MaterialButton>();
	 	}
	  
		mat_buttons.Add(mat);
	}
	
	private void SetNames(List<string> names, List<MaterialButton> mats)
	{
		for (int i = 0; i < mats.Count; i++)
		{
			mats[i].SetName(names[i]);
		}
	}

	public void OnMatEnter(MaterialButton mat)
	{ 
		ResetMats(); 
	 	if(selectedmat==null || mat != selectedmat){
	 		mat.background.sprite = tabHover;
	 	}
	} 

	public void OnMatExit(MaterialButton mat){

		ResetMats();
	}

	public void OnMatSelected(MaterialButton mat){

		selectedmat = mat;

	 	ResetMats(); 
	 	mat.background.sprite = tabActive;
	 	/*
	
	 		save section redirect=>PrefHandler.cs

	 	*/
	 	if(type == Type.Wall){
	 		prefhandler.AddWallMaterial(mat.GetName());
	 	}
	 	if(type == Type.Floor){
	 		prefhandler.AddFloorMaterial(mat.GetName());
	 	}
	 	if(type == Type.Roof){
	 		prefhandler.AddRoofMaterial(mat.GetName());
	 	} 
	}

	public void ResetMats(){

	 	foreach(MaterialButton mat in mat_buttons){

	 		if(selectedmat!=null && mat == selectedmat)continue;
	 		mat.background.sprite = tabIdle;
	 	}
	} 
}
