using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapVisual : MonoBehaviour
{ 
	private GridArea<int> gridArea;
	private Mesh mesh;
	//reduce frame updates
	private bool updateMesh;

	private void Awake(){
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
	}
	public void SetGrid(GridArea<int> gridArea){
		this.gridArea = gridArea; 
		UpdateHeatMapVisual();

		gridArea.OnGridValueChanged += Grid_OnGridValueChanged;
	}

	private void Grid_OnGridValueChanged(object sender, GridArea<int>.OnGridValueChangedEventArgs e){
		//Debug.Log("Grid_OnGridValueChanged");
		//UpdateHeatMapVisual();
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
				
				int gridValue = gridArea.GetGridObject(x, y);
				//float gridValueNormalized = (float)gridValue/GridArea.HEAT_MAP_MAX_VALUE;
				float gridValueNormalized = (float)gridValue/100;
				Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);
				MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, gridArea.GetPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;  
	}
}
