using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Tilemap {
	public event EventHandler OnLoaded;
	public GridArea<TilemapObject> gridArea;

	public Tilemap(int width, int height, float cellSize, Vector3 origin){
		gridArea = new GridArea<TilemapObject>(width, height, cellSize, origin, (GridArea<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y));

	}
/*	public void SetTilemapSprite(Vector3 position, TilemapObject.TilemapSprite tilemapSprite){
		//TilemapObject tilemapObject = gridArea.GetGridObject(position); 
		TilemapObject[] objectArray = gridArea.GetGridObjectArray(position);
		for(int run = 0; run < 9; run++){
			if(objectArray[run] != null){
				objectArray[run].SetTilemapSprite(tilemapSprite);
			}
		}
	}*/
	public void SetTilemapSprite(Vector3 position, TilemapObject.TilemapSprite tilemapSprite){
		TilemapObject tilemapObject = gridArea.GetGridObject(position);
		if(tilemapObject != null){
			tilemapObject.SetTilemapSprite(tilemapSprite);
		}
	}
	public void SetTilemapVisual(TilemapVisual tilemapVisual){
		tilemapVisual.SetGrid(this, gridArea);
	}

	public void Save(string file_type)
	{
		List<TilemapObject.SaveObject> saveObjectList = new List<TilemapObject.SaveObject>();
		for(int x = 0; x < gridArea.GetWidth(); x++)
		{
			for(int y = 0; y < gridArea.GetHeight(); y++)
			{
				TilemapObject tilemapObject = gridArea.GetGridObject(x, y);
				saveObjectList.Add(tilemapObject.Save());
			}
		}
		SaveObject saveObject = new SaveObject {saveArray = saveObjectList.ToArray()};
		
		//SaveSystem.SaveObject(fileName, saveObject);
		string id = Main.Instance.user_info.user_id;
		string json = JsonUtility.ToJson(saveObject);  
		Main.Instance.TilemapSave("new_file",file_type, id, json); 
	}

	public void Load(string fileName)
	{
		SaveObject saveObject = SaveSystem.LoadLatestObject<SaveObject>(fileName);
		foreach(TilemapObject.SaveObject s_object in saveObject.saveArray)
		{
			TilemapObject tilemapObject = gridArea.GetGridObject(s_object.x, s_object.y);
			tilemapObject.Load(s_object);
		}
		OnLoaded?.Invoke(this, EventArgs.Empty);
	}

	public class SaveObject
	{
		public TilemapObject.SaveObject[] saveArray;
	}

	public class TilemapObject
	{
		public enum TilemapSprite
		{   /*the first element, is default sprite. 
			  All other elements are required to be in the dictionary then.*/
			Blank,  
			element1, 
			element2,
			element3
		}

		private GridArea<TilemapObject> gridArea;
		private int x;
		private int y;
		private TilemapSprite tilemapSprite;
		
		public TilemapObject(GridArea<TilemapObject> gridArea, int x, int y)
		{
			this.gridArea = gridArea;
			this.x = x;
			this.y = y;
		}
		
		public void SetTilemapSprite(TilemapSprite tilemapSprite)
		{
			this.tilemapSprite = tilemapSprite;
			gridArea.TriggerGridObjectChanged(x, y); 
		}
		
		public TilemapSprite GetTilemapSprite()
		{
			return this.tilemapSprite;
		}

		public override string ToString()
		{
			return tilemapSprite.ToString();
		}

		[System.Serializable]
		public class SaveObject
		{
			public TilemapSprite tilemapSprite;
			public int x;
			public int y; 
		}

		public SaveObject Save()
		{
			return new SaveObject 
			{
				tilemapSprite = tilemapSprite,
				x = x,
				y = y,
			};
		}

		public void Load(SaveObject saveObject)
		{
			tilemapSprite = saveObject.tilemapSprite;
		} 
	}
}
