using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapVisual : MonoBehaviour
{ 
	[System.Serializable]
	public struct TilemapSpriteUV{
		public Tilemap.TilemapObject.TilemapSprite tilemapSprite;
		public Vector2Int uv00Pixels;
		public Vector2Int uv11Pixels;
	}

	private struct UVCoords{
		public Vector2 uv00;
		public Vector2 uv11;
	}
	[SerializeField] private TilemapSpriteUV[] tilemapSpriteUVArray;
	private GridArea<Tilemap.TilemapObject> gridArea;
	private GridArea<int> groundData;

	private Mesh mesh;
	//reduce frame updates
	private bool updateMesh;
	private Dictionary<Tilemap.TilemapObject.TilemapSprite, UVCoords> uvCoordsDictionary;

	private void Awake(){
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		Texture texture = GetComponent<MeshRenderer>().material.mainTexture;
		float textureWidth = texture.width;
		float textureHeight = texture.height;

		uvCoordsDictionary = new Dictionary<Tilemap.TilemapObject.TilemapSprite, UVCoords>();
		foreach(TilemapSpriteUV tilemapSpriteUV in tilemapSpriteUVArray){
			uvCoordsDictionary[tilemapSpriteUV.tilemapSprite] = new UVCoords{
				uv00 = new Vector2(tilemapSpriteUV.uv00Pixels.x/textureWidth, tilemapSpriteUV.uv00Pixels.y / textureHeight),
				uv11 = new Vector2(tilemapSpriteUV.uv11Pixels.x/textureWidth, tilemapSpriteUV.uv11Pixels.y / textureHeight)
			};
		}
		//if(gridArea != null) groundData = new GridArea<int>()
	}
	public void SetGrid(Tilemap tilemap, GridArea<Tilemap.TilemapObject> gridArea)
	{
		this.gridArea = gridArea; 
		UpdateHeatMapVisual();

		gridArea.OnGridValueChanged += Grid_OnGridValueChanged;
		tilemap.OnLoaded += Tilemap_Onloaded; 
	} 

	private void Tilemap_Onloaded(object sender, System.EventArgs e)
	{
		updateMesh = true; 
	}

	private void Grid_OnGridValueChanged(object sender, GridArea<Tilemap.TilemapObject>.OnGridValueChangedEventArgs e)
	{
		updateMesh = true;
	}

	private void LateUpdate(){
		if(updateMesh){
			updateMesh = false;
			UpdateHeatMapVisual();
		}
	}
	private void UpdateHeatMapVisual(){
		MeshUtils.CreateEmptyMeshArrays(gridArea.GetWidth() * gridArea.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

		for(int x = 0; x < gridArea.GetWidth(); x++){
			for(int y = 0; y < gridArea.GetHeight(); y++){
				int index = x * gridArea.GetHeight() + y;
				Vector3 quadSize = new Vector3(1, 1) * gridArea.GetCellSize(); 
				
				Tilemap.TilemapObject gridObject = gridArea.GetGridObject(x, y); 
				Tilemap.TilemapObject.TilemapSprite tilemapSprite = gridObject.GetTilemapSprite(); 
				Vector2 gridUV00, gridUV11;
				 
				if(tilemapSprite == Tilemap.TilemapObject.TilemapSprite.Blank){ 
					quadSize = Vector3.zero;
					gridUV00 = Vector2.zero;
					gridUV11 = Vector2.zero; 
					
				}
				else{
					//gridValueUV = new Vector2(0.7f, 0f);  //(meshutils differences) 0.7 def value for black
					//gridUV00 = Vector2.zero;
					//gridUV11 = Vector2.zero;
					UVCoords uvCoords = uvCoordsDictionary[tilemapSprite];
					gridUV00 = uvCoords.uv00;
					gridUV11 = uvCoords.uv11;
				}
				MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, gridArea.GetPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridUV00, gridUV11);
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;  
	}
}
