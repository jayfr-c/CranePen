using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class SketchVisual : MonoBehaviour
{ 
	private AreaGrid<SketchPad.SketchObject> grid;
	private Mesh mesh;
  private bool updateMesh;
  
 	private void Awake(){
 		mesh = new Mesh();
 		GetComponent<MeshFilter>().mesh = mesh;
 	}
    public void SetGrid(AreaGrid<SketchPad.SketchObject> grid){
    	this.grid = grid;
    	UpdateHeatMap();
    		// starting here, visuals update per click in cell
    	grid.OnGridObjectChanged += Grid_OnGridObjectChanged; 
    }

    private void Grid_OnGridObjectChanged(object sender, AreaGrid<SketchPad.SketchObject>.OnGridObjectChangedEventArgs e){
    	//Debug.Log(grid.OnGridValueChanged);
    	updateMesh = true;
    }
    private void LateUpdate(){
      if(updateMesh){
        updateMesh = false;
        UpdateHeatMap();
      }
    }
    private void UpdateHeatMap(){
    	MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

   		for(int x = 0; x < grid.GetWidth(); x++){
   			for(int y = 0; y < grid.GetHeight(); y++){
   				int index = x * grid.GetHeight() + y;
   				Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

   				SketchPad.SketchObject gridObject = grid.GetGridObject(x,y); 
          SketchPad.SketchObject.TraceSprite traceSprite = gridObject.GetTraceSprite(); 
   				Vector2 gridValueUV;
          if(traceSprite == SketchPad.SketchObject.TraceSprite.None){
            gridValueUV = Vector2.zero;
            quadSize = Vector3.zero;
          }
          else{
            gridValueUV = Vector2.one;
          }
   				MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetPosition(x,y)+quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);

   			}
   		}
   		mesh.vertices = vertices;
   		mesh.uv = uv;
   		mesh.triangles = triangles;

    }
}
