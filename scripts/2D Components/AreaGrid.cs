using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class AreaGrid<TGridObject> {

	public const int HEAT_MAP_MAX_VALUE = 3;
	public const int HEAT_MAP_MIN_VALUE = 0;
	public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged; 
	public class OnGridObjectChangedEventArgs : EventArgs{
		public int x;
		public int y;
	}

	public int width;
	public int height; 
	private float cellSize;

	public TGridObject[,] gridArray;
	private Vector3 originPosition;
	private GameObject sheet; 

	public AreaGrid(int width, int height, float cellSize, Vector3 originPosition, Func<AreaGrid<TGridObject>, int, int, TGridObject> createGridObject){
		this.width = width;
		this.height = height; 
		this.cellSize = cellSize; 
		this.originPosition = originPosition;

		gridArray = new TGridObject[width, height];  
 		
 		for(int x = 0; x < gridArray.GetLength(0); x++){
 			for(int y = 0; y < gridArray.GetLength(1); y++){
 				gridArray[x, y] = createGridObject(this, x, y); 
 				//debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetPosition(x,y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.black, TextAnchor.MiddleCenter);
 			}
 		}
 		bool showDebug = true;
 		if(showDebug){
			TextMesh[,] debugTextArray = new TextMesh[width, height];

	 		for(int x = 0; x < gridArray.GetLength(0); x++){
				for (int y = 0; y < gridArray.GetLength(1); y++){ 
					//UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetPosition(x,y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.black, TextAnchor.MiddleCenter);
					debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y]?.ToString(), null, GetPosition(x,y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.black, TextAnchor.MiddleCenter);
					Debug.DrawLine(GetPosition(x, y), GetPosition(x, y + 1), Color.black, 100f);
					Debug.DrawLine(GetPosition(x, y), GetPosition(x + 1, y), Color.black, 100f);
				}
			}
			Debug.DrawLine(GetPosition(width, height), GetPosition(0, height), Color.black, 100f);
			Debug.DrawLine(GetPosition(width, height), GetPosition(width, 0), Color.black, 100f);
			
			OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>{
				Debug.Log("asdfasdf");
				debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
			}; 
 		} 
	}
	public int GetWidth(){
		return width;
	}

	public int GetHeight(){
		return height;
	}

	public float GetCellSize(){
		return cellSize;
	}

	public Vector3 GetPosition(int x, int y ){ 
		return new Vector3(x, y) * cellSize + originPosition;  
	}

	private void GetXY(Vector3 position, out int x, out int y){
		x = Mathf.FloorToInt((position - originPosition).x / cellSize);
		y = Mathf.FloorToInt((position - originPosition).y / cellSize);
	}

	public void SetGridObject(int x, int y, TGridObject value){
		if( x>= 0 && y >= 0 && x < width && y < height){
			gridArray[x, y] = value;    //study method
			if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
		}
	}

	public void TriggerGridObjectChanged(int x, int y){
		if(OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});
	}
	public void SetGridObject(Vector3 position, TGridObject value){
		int x, y;
		GetXY(position, out x, out y);
		SetGridObject(x, y, value);
	}

	public TGridObject GetGridObject(int x, int y){
		if(x >= 0 && y >= 0 && x < width && y < height){
			return gridArray[x, y];
		}
		else{
			return default(TGridObject);
		}
	}

	public TGridObject GetGridObject(Vector3 position){
		int x, y;
		GetXY(position, out x, out y);
		return GetGridObject(x, y);
	}
}
