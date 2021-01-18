using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefObject
{
	/*Land Area*/
    public int width;
	public int height;
	
	/*Materials*/ 
	public List<string> mats_roof;
	public List<string> linear_mats_roof; 
	public List<string> mats_wall;
	public List<string> linear_mats_wall;
	public List<string> mats_floor; 
	public List<string> linear_mats_floor; 
	
	/*Color*/
	public object wall_color;
	public object floor_color;
	public object roof_color;  
	/*interior*/
	public List<string> items;
	public List<int> quantities;
}
