using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.IO;

public class PrefHandler : MonoBehaviour
{
	private string SAVE_FOLDER; 

    PrefObject prefObject;

    private void Awake()
    {	 
    	prefObject = new PrefObject(); 

    	SaveSystem.Init();
    } 

    public void Save()
    {
    	string json = JsonUtility.ToJson(prefObject);
    	SaveSystem.Save("userpref",json, false);
    	Debug.Log("data saved "+json);

    	/*transfer saving json to db*/
    }

    public void Load()
    {
    	string json = SaveSystem.LoadLatestFile("userpref"); 
    	if(json!=null)
    	{
    		PrefObject prefObject = JsonUtility.FromJson<PrefObject>(json);
    	} else {
    		Debug.Log("empty data");
    	}
    	/*edit load process*/
    }

	public void SetWidth(int width)
	{
		prefObject.width = width; 
		Debug.Log("setting width to: "+ prefObject.width);
	}

	public void SetHeight(int height)
	{
		prefObject.height = height;
		Debug.Log("setting height to: "+ prefObject.height);
	}

	public void AddWallMaterial(string mat)
	{ 
		prefObject.mats_wall.Add(mat);
		Debug.Log( mat + " added to walls list");
	}

	public void AddFloorMaterial(string mat)
	{ 
		prefObject.mats_floor.Add(mat);
		Debug.Log(mat + " added to floors list");
	}

	public void AddRoofMaterial(string mat)
	{ 
		prefObject.mats_roof.Add(mat);
		Debug.Log(mat + " added to roofs list");
		Debug.Log(prefObject.mats_roof);
	}

	public void SetWallColor(string name, Color32 rgb){prefObject.wall_color = new ColorDetails(name, rgb);}
	public void SetFloorColor(string name, Color32 rgb){prefObject.floor_color = new ColorDetails(name, rgb);}
	public void SetRoofColor(string name, Color32 rgb){prefObject.roof_color = new ColorDetails(name, rgb); Debug.Log(name+" set");}
	 

	public void AddItem(string item)
	{	
		if(prefObject.items==null)
		{
			prefObject.items = new List<string>();
		}

		prefObject.items.Add(item); 
		Debug.Log("@prefhandler-- "+item+" added");
	} 

	public void RemoveItem(string item)
	{
		if(prefObject.items!=null)
		{
			prefObject.items.Remove(item);
		}
	}

	public int GetItemQuantity(string item)
	{
		int count = 0;
		if(prefObject.items==null) return 0;
		foreach(string i_name in prefObject.items)
		{
			if(i_name == item) count+= 1;
		} 
		return count; 
	}

	public List<string> GetItemList()
	{
		return null;
	} 
	public class ColorDetails{
		public string name; 
		public Color32 rgb; 
		public ColorDetails(string name, Color32 rgb) 
		{
			this.name = name; 
			this.rgb = rgb; 
		}
	}
}
