using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{ 
 
    public TabContainer tabContainer; 
    public TabGroup tabGroup; 
    
    public Image background;

    public UnityEvent onTabSelected;

    public UnityEvent onTabDeselected;

    public void OnPointerClick(PointerEventData eventData)
    { 
        if(tabGroup!=null) tabGroup.OnTabSelected(this);
        tabContainer.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    	tabContainer.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    	tabContainer.OnTabExit(this);
    }

    void Start()
    {
        background = GetComponent<Image>(); 
        tabContainer.Subscribe(this);
    }
 
    public void Select()
    {
        if(onTabSelected!=null){
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if(onTabDeselected!=null)
        {
            onTabDeselected.Invoke();
        }
    }
}
