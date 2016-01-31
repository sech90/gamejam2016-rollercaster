using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PropagateTouch : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,  IPointerDownHandler, IPointerUpHandler, IPointerExitHandler {

	[SerializeField] GameObject PropagateTo;

	[SerializeField] bool PointerDown = true;
	[SerializeField] bool PointerUp = true;
	[SerializeField] bool PointerEnter = true;
	[SerializeField] bool PointerExit = true;
	[SerializeField] bool BeginDrag = true;
	[SerializeField] bool Drag = true;
	[SerializeField] bool EndDrag = true;


	public void OnPointerExit (PointerEventData eventData){
		if(PointerExit && FindAndSetBeneathElement(eventData)){
			//eventData.pointerEnter = PropagateTo;
			ExecuteEvents.Execute<IPointerExitHandler>(PropagateTo, eventData,ExecuteEvents.pointerExitHandler);
		}
	}

	public void OnPointerEnter (PointerEventData eventData){
		if(PointerEnter && FindAndSetBeneathElement(eventData)){
			//eventData.pointerEnter = PropagateTo;
			ExecuteEvents.Execute<IPointerEnterHandler>(PropagateTo, eventData,ExecuteEvents.pointerEnterHandler);
		}
	}

	public void OnPointerUp (PointerEventData eventData){
		if(PointerUp && FindAndSetBeneathElement(eventData)){
			//eventData.pointerPress = PropagateTo;
			ExecuteEvents.Execute<IPointerUpHandler>(PropagateTo, eventData,ExecuteEvents.pointerUpHandler);
		}
	}

	public void OnPointerDown (PointerEventData eventData){
		if(PointerDown && FindAndSetBeneathElement(eventData)){
			//eventData.pointerPress = PropagateTo;
			ExecuteEvents.Execute<IPointerDownHandler>(PropagateTo, eventData,ExecuteEvents.pointerDownHandler);
		}
	}

	public void OnBeginDrag (PointerEventData eventData){
		if(BeginDrag && FindAndSetBeneathElement(eventData)){
			//eventData.pointerDrag = PropagateTo;
			ExecuteEvents.Execute<IBeginDragHandler>(PropagateTo, eventData,ExecuteEvents.beginDragHandler);
		}
	}

	public void OnDrag (PointerEventData eventData){
		if(Drag && FindAndSetBeneathElement(eventData)){
			//eventData.pointerDrag = PropagateTo;
			ExecuteEvents.Execute<IDragHandler>(PropagateTo, eventData,ExecuteEvents.dragHandler);
		}
	}


	public void OnEndDrag (PointerEventData eventData){
		if(EndDrag && FindAndSetBeneathElement(eventData)){
			//eventData.pointerDrag = PropagateTo;
			ExecuteEvents.Execute<IEndDragHandler>(PropagateTo, eventData,ExecuteEvents.endDragHandler);
		}
	}


	private bool FindAndSetBeneathElement(PointerEventData eventData){

	

		//find the element immediatly beneath this object
		List<RaycastResult> tempResults = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData,tempResults);

		if(tempResults.Count > 1){
			PropagateTo = tempResults[1].gameObject;
			Debug.Log("PropagateTo is "+PropagateTo.name);
		}
		else 
			PropagateTo = null;

		return PropagateTo != null;
	}
}
