using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragWithJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler {
	public static GameObject dragged;

	//	public bool record;
	//	public float clickTime;
	private bool singleClick;

	Vector3 startPosition;

	public void Start() {
	}

	//	public void Update () {
	// Keep adding to currentTime if record is true
	//		if (record)
	//			clickTime += 1f * Time.deltaTime;
	//		else
	//			clickTime = 0f;
	//	}

	public void OnPointerDown(PointerEventData eventData) {
//		Debug.Log ("mouse down called");
		singleClick = true;
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (singleClick) {
			Wheel w = transform.parent.GetComponent<Wheel> ();
			w.CastSpell (w.spell.type, w.spell.level, w.spell.id);
		}
	}

	//	void OnMouseDrag()
	//	{
	//		Debug.Log ("Drag fired");
	//		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
	//		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
	//		transform.position = curPosition;
	//	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = eventData.position;
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		dragged = gameObject;
		Debug.Log (dragged.name);
		startPosition = transform.position;
		singleClick = false;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		//		Transform fifthSlot = transform.parent.parent.GetChild (transform.parent.childCount - 1);
		//		int siblingIndex = transform.GetSiblingIndex ();
		//		fifthSlot.transform.SetSiblingIndex (siblingIndex);
		transform.parent.SetAsLastSibling ();

		//		startParent = transform.parent;
		//		Debug.Log (transform.parent.name);
		//		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		//		GetComponent<CanvasGroup>().blocksRaycasts = true;
		//		Debug.Log (transform.parent.name);
		//		if(transform.parent == startParent){
		//			transform.position = startPosition;
		//		}
		//		transform.position = transform.parent.position;
		transform.position = startPosition;
	}

	public void OnDrop(PointerEventData eventData) {
		if (gameObject != dragged) {
			Wheel triggerWheel = transform.parent.GetComponent<Wheel> ();
			Wheel draggedWheel = dragged.transform.parent.GetComponent<Wheel> ();
			Debug.Log (dragged.name + ", type " + draggedWheel.spell.type + " is dropped on " + gameObject.name + ", type " + triggerWheel.spell.type);
			if (triggerWheel.Stack (draggedWheel.spell)) {
				draggedWheel.DisableWheel (2);
			}
		}
		dragged = null;
	}
}