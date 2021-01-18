using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchPad{

	private AreaGrid<SketchObject>  grid;

 	public SketchPad(int width, int height, float cellSize, Vector3 originPosition){
 		Debug.Log("at constructor");
 		grid = new AreaGrid<SketchObject>(width, height, cellSize, originPosition, (AreaGrid<SketchObject> g, int x, int y) => new SketchObject(grid, x, y));
 	} 
 	public void SetTraceSprite(Vector3 position, SketchObject.TraceSprite traceSprite){
			Debug.Log(position);
 		SketchObject sketchObject = grid.GetGridObject(position);
 		if(sketchObject != null){
 			Debug.Log("Initial sketchObject value: "+sketchObject);
 			sketchObject.SetTraceSprite(traceSprite);
 		}
 	}
 	public void SetSketchVisual(SketchVisual sketchVisual){
 		sketchVisual.SetGrid(grid);
 	}
 	public AreaGrid<SketchObject> GetGrid(){
 		return grid;
 	}
 	public class SketchObject{
 		public enum TraceSprite{
 			None, 
 			Ground
 		}

 		private AreaGrid<SketchObject> grid;
 		private int x;
 		private int y;
 		private TraceSprite traceSprite;

 		public SketchObject(AreaGrid<SketchObject> grid, int x, int y){
 			this.grid = grid;
 			this.x = x;
 			this.y = y; 
 		}
 		public void SetTraceSprite(TraceSprite traceSprite){
 			this.traceSprite = traceSprite;
 			Debug.Log("at set traceSprite: "+ traceSprite);
 			grid.TriggerGridObjectChanged(x, y);
 			//grid.SetGridObject(x,y, traceSprite.Ground);
 		}
 		public TraceSprite GetTraceSprite(){
 			return traceSprite;
 		}
 		public override string ToString(){
 			return traceSprite.ToString(); 
 		}
 	}
}
