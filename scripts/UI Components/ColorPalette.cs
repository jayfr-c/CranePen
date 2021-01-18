using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{ 
	public PrefHandler prefHandler; 
	public GameObject labelText; 

	private List<ColorPiece> colors; 
	public List<string> colorNames;
	public List<Color32> hex_values; 

    public bool at_wall; 
    public bool at_floor; 
    public bool at_roof; 

	private ColorPiece selectedColor; 

    private void Start()
    { 
        Reset();
     	///<indices>1-4 
     	///<row> 1 
     	hex_values.Add(new Color32(253, 223, 223,255)); colorNames.Add("Misty Rose");
     	hex_values.Add(new Color32(252, 247, 222,255)); colorNames.Add("Cornsilk");  
     	hex_values.Add(new Color32(222, 253, 224,255)); colorNames.Add("Nylon"); 
     	hex_values.Add(new Color32(244, 173, 198,255)); colorNames.Add("Metallic Pink"); 
     	
     	///<indices>5-8 
     	///<row> 2
     	hex_values.Add(new Color32(244, 226, 210,255)); colorNames.Add("Champagne Pink");
     	hex_values.Add(new Color32(255, 245, 251,255)); colorNames.Add("Snow");  
     	hex_values.Add(new Color32(219, 196, 223,255)); colorNames.Add("Languid Lavender"); 
     	hex_values.Add(new Color32(184, 139, 173,255)); colorNames.Add("Opera Mauve"); 
     	///<indices>9-12 
     	///<row> 3
     	hex_values.Add(new Color32(255, 221, 221,255)); colorNames.Add("Pale Pink");
     	hex_values.Add(new Color32(255, 255, 207,255)); colorNames.Add("Cream");  
     	hex_values.Add(new Color32(217, 255, 223,255)); colorNames.Add("Nyanza"); 
     	hex_values.Add(new Color32(175, 195, 210,255)); colorNames.Add("Pastel Blue"); 
     	///<indices>13-16 
     	///<row> 4
     	hex_values.Add(new Color32(255, 255, 216,255)); colorNames.Add("Light Yellow");
     	hex_values.Add(new Color32(234, 235, 255,255)); colorNames.Add("Lavender");  
     	hex_values.Add(new Color32(211, 238, 255,255)); colorNames.Add("Water"); 
     	hex_values.Add(new Color32(119, 153, 204,255)); colorNames.Add("Dark Pastel Blue"); 
        ///<indices>17-20 
     	///<row> 5
     	hex_values.Add(new Color32(173, 230, 208,255)); colorNames.Add("Magic Mint");
     	hex_values.Add(new Color32(241, 240, 207,255)); colorNames.Add("Eggshell");  
     	hex_values.Add(new Color32(235, 206, 237,255)); colorNames.Add("Classic Rose"); 
     	hex_values.Add(new Color32(222, 179, 235,255)); colorNames.Add("Mauve"); 
     	
     	///<indices>21-24 
     	///<row> 6
     	hex_values.Add(new Color32(190, 215, 209,255)); colorNames.Add("Jet Stream");
     	hex_values.Add(new Color32(247, 235, 195,255)); colorNames.Add("Lemon Meringue");  
     	hex_values.Add(new Color32(251, 214, 198,255)); colorNames.Add("Unbleached Silk"); 
     	hex_values.Add(new Color32(248, 225, 231,255)); colorNames.Add("Piggy Pink"); 
     	///<indices>25-28 
     	///<row> 7
     	hex_values.Add(new Color32(193, 231, 227,255)); colorNames.Add("Columbia Blue");
     	hex_values.Add(new Color32(255, 220, 244,255)); colorNames.Add("Pink Lace");  
     	hex_values.Add(new Color32(218, 191, 222,255)); colorNames.Add("Thistle"); 
     	hex_values.Add(new Color32(193, 187, 221,255)); colorNames.Add("Lavender Gray"); 
     	///<indices>29-32 
     	///<row> 8
     	hex_values.Add(new Color32(224, 254, 254,255)); colorNames.Add("Sky White");
     	hex_values.Add(new Color32(199, 206, 234,255)); colorNames.Add("Perwinkle");  
     	hex_values.Add(new Color32(255, 218, 193,255)); colorNames.Add("Very Pale Orange"); 
     	hex_values.Add(new Color32(255, 255, 216,255)); colorNames.Add("Light Yellow");
     	///<indices>33-36 
     	///<row> 9
     	hex_values.Add(new Color32(253, 207, 179,255)); colorNames.Add("Apricot");
     	hex_values.Add(new Color32(206, 200, 228,255)); colorNames.Add("Light Perwinkle");  
     	hex_values.Add(new Color32(249, 247, 232,255)); colorNames.Add("Old Lace"); 
     	hex_values.Add(new Color32(255, 154, 162,255)); colorNames.Add("Light Salmon Pink");

     	if(transform.childCount != 0)SetColorBoxes();
    }
    private void Reset()
    {
        at_wall = false; 
        at_floor = false; 
        at_roof = false; 
    }
    public void EditWall() {Reset(); at_wall = true;}
    public void EditFloor() {Reset(); at_floor = true;}
    public void EditRoof() {Reset(); at_roof = true;}

    public void Subscribe(ColorPiece piece)
    {
     	if(colors==null){
     		colors = new List<ColorPiece>();
     	}
     	colors.Add(piece);
    }

    public void HoveredColor(ColorPiece piece)
    {
     	ResetList();
    	DisplayName(piece.c_name); 
        if(selectedColor == null || piece !=selectedColor) {piece.On_Box();}
    }

    public void SelectedColor(ColorPiece piece)
    { 
     	selectedColor = piece; 
     	ResetList();
     	piece.On_Box();
 		SetPref(piece.c_name, piece.c_values);
    }

    public void DeselectedColor(ColorPiece piece) {ResetList();} 
     /*
     
      save section redirect=>PrefHandler.cs

     */
    public void SetPref(string name, Color32 values)
    {
     	if(at_wall) {prefHandler.SetWallColor(name, values);}
        if(at_floor) {prefHandler.SetFloorColor(name, values);}
        if(at_roof) {prefHandler.SetRoofColor(name, values);}
    }

    public void DisplayName(string name)
    { 
     	TMPro.TextMeshProUGUI textMesh = labelText.GetComponent<TMPro.TextMeshProUGUI>();
     	textMesh.text = name; 
    }
 
    private void SetColorBoxes()
    {
     	int children = transform.childCount;
     	for(int i = 0; i < children; i++)
     	{
     		GameObject colorPiece = transform.GetChild(i).gameObject;
     		ColorPiece obj_class = colorPiece.GetComponent<ColorPiece>();
     		obj_class.SetValues(colorNames[i], hex_values[i]);
     	}
    }

    private void DisplayLists()
    {
     	for(int i = 0; i != hex_values.Count; i++)
     	{
     		Debug.Log(colorNames[i]+ " "+ hex_values[i]);
     	}
    }

    private void ResetList()
    {
     	foreach(ColorPiece piece in colors)
     	{
     		if(selectedColor!=null && piece == selectedColor){continue;}
            piece.Off_Box(); 
     	}
    }
}
