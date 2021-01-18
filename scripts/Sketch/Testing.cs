using UnityEngine.UI;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour 
{     
	[SerializeField] private TilemapVisual tilemapVisual;
	[SerializeField] private TilemapVisual backdrop;
	[SerializeField] private TilemapVisual floorVisual; 
	private GridArea<HeatMapGridObject> gridArea;
	private GridArea<StringGridObject> stringGrid;

	private Tilemap wallmap;
	private Tilemap floormap; 
	private Tilemap drawingsheet; 
	private Tilemap.TilemapObject.TilemapSprite wallmapSprite;
	private Tilemap.TilemapObject.TilemapSprite floormapSprite;

	public bool controlSwitch;    
	public Button save_button; 
	private int direction;  
	private bool directionLock; 
	private bool floorEdit; 
	private Vector3 dragStartPosition;
	private Vector3 dragCurrentPosition;

	private void Start() 
	{   
		direction = 0;  
		directionLock = false; 
		controlSwitch = false; 
		floorEdit = false; 

		dragStartPosition = Vector3.zero;
		dragCurrentPosition = Vector3.zero;
		/*
			dynamic tilemap size set once 
			data to be drawn from storage
			either default value or user pref
		*/
		wallmap = new Tilemap(70, 50, 3f, Vector3.zero);
		floormap = new Tilemap(35, 25, 6f, new Vector3(0, 0, 20));
		drawingsheet = new Tilemap(70, 50, 3f, new Vector3(0, 0, 55));

		wallmap.SetTilemapVisual(tilemapVisual);
		drawingsheet.SetTilemapVisual(backdrop);
		floormap.SetTilemapVisual(floorVisual);
		PlainVisual(drawingsheet);
	}

	private void Update()
	{
		if(controlSwitch) 
		{
			if(floorEdit == true) 
			{
				HandleMouseInputs(floormap, floormapSprite);
				HandleKeyInputs(floormapSprite); 
			}
			else 
			{
				HandleMouseInputs(wallmap, wallmapSprite);
				HandleKeyInputs(wallmapSprite); 
			}
		}  
		/*if(direction_update) {
			dragStartPosition = dragCurrentPosition;
			direction_update = false; 
		} */
	}

	/*One line functions externally controlled by ui buttons
	  element1 - outer wall, plain 
	  element2 - interior wall, vertical
	  element3 - white, tiled */
	public void SetPenControl() {this.controlSwitch = !this.controlSwitch;}
	public void SetDirectionControl(){this.directionLock = !this.directionLock;}
	public void SetFloorEditing(){this.floorEdit = !this.floorEdit;}
	public void SetExteriorWall() {wallmapSprite = Tilemap.TilemapObject.TilemapSprite.element1;}
	public void SetInteriorWall() {wallmapSprite = Tilemap.TilemapObject.TilemapSprite.element2;}
	public void SetPlainFloor() {floormapSprite = Tilemap.TilemapObject.TilemapSprite.element1;}
	public void SetVerticalFloor() {floormapSprite = Tilemap.TilemapObject.TilemapSprite.element2;}
	public void SetTiledFloor() {floormapSprite = Tilemap.TilemapObject.TilemapSprite.element3;} 
	public void SetEraser() {wallmapSprite = Tilemap.TilemapObject.TilemapSprite.Blank;}

	private void HandleMouseInputs(Tilemap tilemap, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{ 
		/* mouse first left click  */
		if(Input.GetMouseButtonDown(0)) 
		{ 
			 
			Vector3 position = UtilsClass.GetMouseWorldPosition();
			tilemap.SetTilemapSprite(position, tilemapSprite);

			Plane plane = new Plane(Vector3.forward, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float entry;
			
			if(plane.Raycast(ray, out entry))
			{
				dragStartPosition = ray.GetPoint(entry);
			}
			if(directionLock == true) GetDirection(); 
		}
 		/*  mouse onclick */
		if(Input.GetMouseButton(0))
		{    
			Vector3 position = UtilsClass.GetMouseWorldPosition();

			Plane plane = new Plane(Vector3.forward, Vector3.zero);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float entry; 

			if(plane.Raycast(ray, out entry)) 
			{  
				dragCurrentPosition = ray.GetPoint(entry);
				if(directionLock == false) GetDirection();

 		 		HandleDragMovements(position, tilemap, tilemapSprite); 
			}  
		} 
	}

	private void HandleKeyInputs(Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{
		if(Input.GetKeyDown(KeyCode.T)){tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Blank;}
		if(Input.GetKeyDown(KeyCode.Y)){tilemapSprite = Tilemap.TilemapObject.TilemapSprite.element1; }
		if(Input.GetKeyDown(KeyCode.U)){tilemapSprite = Tilemap.TilemapObject.TilemapSprite.element2;} 
		if(Input.GetKeyDown(KeyCode.P)){wallmap.Save("wallsketch"); floormap.Save("floorsketch"); }
		if(Input.GetKeyDown(KeyCode.L)){wallmap.Load("wallsketch"); floormap.Load("floorsketch");}
	} 

 	private void PlainVisual(Tilemap map) 
 	{
 		Debug.Log("plained backdrop");
 		for(int x = 0; x < map.gridArea.GetWidth() * map.gridArea.GetCellSize(); x++) {	
 			for(int y = 0; y < map.gridArea.GetHeight() * map.gridArea.GetCellSize(); y++) {
 				map.SetTilemapSprite(new Vector3( x, y, 0), Tilemap.TilemapObject.TilemapSprite.element3);
 			}
 		}
 	}

	private int GetDirection()
	{ 
		float xdiff = Mathf.Abs(dragCurrentPosition.x - dragStartPosition.x);
		float ydiff = Mathf.Abs(dragCurrentPosition.y - dragStartPosition.y); 

		if(Mathf.Floor(xdiff) > Mathf.Floor(ydiff))
		{
			//case horizontal : x varies , y same
			direction = (dragStartPosition.x < dragCurrentPosition.x)? 3:4; 
		}
		if(Mathf.Floor(xdiff) < Mathf.Floor(ydiff))
		{
			//case vertical : x same, y varies
			direction = (dragStartPosition.y < dragCurrentPosition.y)? 1:2; 
		} 
		return direction;
	}

	private void UpwardTrace(Vector3 position, Tilemap tilemap, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{    
		Vector3 alteredPosition =  new Vector3(dragStartPosition.x, position.y, 0); //upward
 	  	tilemap.SetTilemapSprite(alteredPosition, tilemapSprite); 

		int ycurrent = (int) Mathf.Floor(dragCurrentPosition.y);
		int ystart = (int) Mathf.Floor(dragStartPosition.y);
		int xsteady = (int) Mathf.Floor(dragStartPosition.x);

		for(int y = ystart; y < ycurrent; y++)
		{
			tilemap.SetTilemapSprite(new Vector3( xsteady, y, 0), tilemapSprite);
		} 
		/*drag start update in prep
		  for a direction change*/
		dragStartPosition = new Vector3(dragStartPosition.x, dragCurrentPosition.y);
	}

	private void DownwardTrace(Vector3 position, Tilemap tilemap, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{   
		Vector3 alteredPosition =  new Vector3(dragStartPosition.x, position.y, 0); //downward
 	  	tilemap.SetTilemapSprite(alteredPosition, tilemapSprite); 

		int ycurrent = (int) Mathf.Floor(dragCurrentPosition.y);
		int ystart = (int) Mathf.Floor(dragStartPosition.y);
		int xsteady = (int) Mathf.Floor(dragStartPosition.x);

		for(int y = ystart; y > ycurrent; y--)
		{
			tilemap.SetTilemapSprite(new Vector3( xsteady, y, 0), tilemapSprite);
		} 
		/*drag start update in prep
		  for a direction change
		  the verticals updates are the same */
		dragStartPosition = new Vector3(dragStartPosition.x, dragCurrentPosition.y);
	}

	private void LeftwardTrace(Vector3 position, Tilemap tilemap, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{  
		Vector3 alteredPosition =  new Vector3(position.x, dragStartPosition.y, 0); //leftward
 	  	tilemap.SetTilemapSprite(alteredPosition, tilemapSprite); 

		int xcurrent = (int) Mathf.Floor(dragCurrentPosition.x);
		int xstart = (int) Mathf.Floor(dragStartPosition.x);
		int ysteady = (int) Mathf.Floor(dragStartPosition.y);

		for(int x = xstart; x > xcurrent; x--)
		{
			tilemap.SetTilemapSprite(new Vector3( x, ysteady, 0), tilemapSprite);
		} 
		/*drag start update in prep
		  for a direction change
		  horizontals updates are the same as well*/
		dragStartPosition = new Vector3(dragCurrentPosition.x, dragStartPosition.y);
	}

	private void RightwardTrace(Vector3 position, Tilemap tilemap, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{  
		Vector3 alteredPosition =  new Vector3(position.x, dragStartPosition.y, 0); //downward
 	  	tilemap.SetTilemapSprite(alteredPosition, tilemapSprite); 

		int xcurrent = (int) Mathf.Floor(dragCurrentPosition.x);
		int xstart = (int) Mathf.Floor(dragStartPosition.x);
		int ysteady = (int) Mathf.Floor(dragStartPosition.y);

		for(int x = xstart; x < xcurrent; x++)
		{
			tilemap.SetTilemapSprite(new Vector3( x, ysteady, 0), tilemapSprite);
		}

		/*drag start update in prep
		  for a direction change*/
		dragStartPosition = new Vector3(dragCurrentPosition.x, dragStartPosition.y);
	} 
	private void HandleDragMovements(Vector3 position, Tilemap tilemap, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
	{ 
		switch(direction)
		{
		case 1: UpwardTrace(position, tilemap, tilemapSprite); 
			break;
		case 2: DownwardTrace(position, tilemap, tilemapSprite); 
			break;
		case 3: RightwardTrace(position, tilemap, tilemapSprite); 
			break;
		case 4: LeftwardTrace(position, tilemap, tilemapSprite);
			break;
		}   
	}

	private void HandleDiagonalsMovements()
	{
		Debug.Log("a diagonal movement"); 
	}
 
}

public class HeatMapGridObject
{ 
	private const int MIN = 0;
	private const int MAX = 100; 

	private GridArea<HeatMapGridObject> gridArea;
	private int x;
	private int y;
	private int value;

	public HeatMapGridObject(GridArea<HeatMapGridObject> gridArea, int x, int y)
	{
		this.gridArea = gridArea;
		this.x = x;
		this.y = y;
	}

	public void AddValue(int addValue)
	{
		value += addValue;
		Mathf.Clamp(value, MIN, MAX);
		gridArea.TriggerGridObjectChanged(x,y);
	}

	public float GetValueNormalized()
	{
		return (float)value / MAX;
	}

	public override string ToString()
	{
		return value.ToString();
	}
}

public class StringGridObject
{ 
	private GridArea<StringGridObject> gridArea;
	private int x;
	private int y; 

	private string letters;
	private string numbers;

	public StringGridObject(GridArea<StringGridObject> gridArea, int x, int y)
	{
		this.gridArea = gridArea;
		this.x = x; 
		this.y = y;
	}

	public void AddLetter(string letter)
	{
		letters += letter;
		gridArea.TriggerGridObjectChanged(x, y);
	}

	public void AddNumber(string number)
	{
		numbers += number;
		gridArea.TriggerGridObjectChanged(x, y);
	}

	public override string ToString()
	{
		Debug.Log("converted");
		return letters +  "\n" + numbers;
	}
}