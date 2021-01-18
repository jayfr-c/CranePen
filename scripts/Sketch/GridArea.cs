using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 
/*
    Source:: ......................
*/
public class GridArea<TGridObject>{
 
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

	private int width;
	private int height;
    private float cellSize;
	private TGridObject[,] gridArray; 
    private Vector3 originPosition;

    public GridArea(int width, int height, float cellSize, Vector3 originPosition, Func<GridArea<TGridObject>, int, int, TGridObject> createGridObject){
    	this.width = width;
    	this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
 
    	gridArray = new TGridObject[width, height];

        for(int x = 0; x < gridArray.GetLength(0); x++){
            for(int y = 0; y < gridArray.GetLength(1); y++){
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = false;
        if(showDebug){
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for(int x = 0; x < gridArray.GetLength(0); x++){
                for(int y = 0; y < gridArray.GetLength(1); y++){
                    debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 30, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetPosition(x, y), GetPosition(x, y+1), Color.white, 100f);
                    Debug.DrawLine(GetPosition(x, y), GetPosition(x+1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetPosition(0, height), GetPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetPosition(width, 0), GetPosition(width, height), Color.white, 100f); 
            
            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>{
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int GetWidth(){
        return this.width;
    }
    public int GetHeight(){
        return this.height;
    }
    public float GetCellSize(){
        return this.cellSize;
    }
    public Vector3 GetPosition(int x, int y){
        return new Vector3(x, y ) * cellSize + originPosition;
    }

    public void GetXY(Vector3 position, out int x, out int y){
        x = Mathf.FloorToInt((position- originPosition).x / cellSize);
        y = Mathf.FloorToInt((position- originPosition).y / cellSize);
    }
 
    public void TriggerGridObjectChanged(int x, int y){
        if(OnGridValueChanged != null){ 
            OnGridValueChanged(this, new OnGridValueChangedEventArgs{ x = x, y = y});
        }
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
    public TGridObject[] GetGridObjectArray(Vector3 position){
        TGridObject[] objArray = new TGridObject[9];
        int x, y;
        GetXY(position, out x, out y); 
        objArray[0] = GetGridObject(x - 1, y - 1);
        objArray[1] = GetGridObject(x, y - 1);
        objArray[2] = GetGridObject(x + 1, -y);
        objArray[3] = GetGridObject(x - 1, y);
        objArray[4] = GetGridObject(x, y);
        objArray[5] = GetGridObject(x + 1, y);
        objArray[6] = GetGridObject(x - 1, y + 1);
        objArray[7] = GetGridObject(x, y + 1);
        objArray[8] = GetGridObject(x + 1, y + 1);      
        return objArray;
    }
 
    
    
}

