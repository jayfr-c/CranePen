using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System; 

public class Files : MonoBehaviour
{
	public string file_name {get; private set;}

	public GameObject name_holder; 
	public Button selection_button;
	public Button sketch_button;
	public Button render_button; 

	private string prefs_json;
	private string outline_json; 
	private string floor_json; 
	Action<string> _detailsCallback; 

	void Awake()
	{ 

		selection_button.onClick.AddListener(() => { 
			Selection();
			_detailsCallback = (prefs) => {
				Debug.Log("prefs retrieved =>   "+prefs);
			};
			StartCoroutine(Main.Instance.database.GetPref(file_name, _detailsCallback));
    	});
    	sketch_button.onClick.AddListener(() => {
    		Draw();  
    		StartCoroutine(RetrieveSketches());
    	});
    	render_button.onClick.AddListener(() => {
    		Render(); 
    		StartCoroutine(RetrieveSketches());
    	});
	}
    public void Selection() { SceneManager.LoadScene("Selection"); }
	public void Draw() { SceneManager.LoadScene("Planner"); }
	public void Render() { SceneManager.LoadScene("Renderer"); }

	public void SetName(string name) 
	{ 
		TMPro.TextMeshProUGUI textMesh = name_holder.GetComponent<TMPro.TextMeshProUGUI>();
		textMesh.text = name;
		file_name = name;  
		Debug.Log("file name set: "+file_name);
	}

	private IEnumerator RetrieveSketches() 
	{
		_detailsCallback = (outline) => {
			Debug.Log("outline retrieved =>  "+outline);
		};
		StartCoroutine(Main.Instance.database.GetOutline(file_name, _detailsCallback));
		Action<string> _floorCallback = (floor) => {
			Debug.Log("floor retrieved =>   "+floor);
		};
		StartCoroutine(Main.Instance.database.GetFloor(file_name, _floorCallback));
		yield return null; 
	}
}
