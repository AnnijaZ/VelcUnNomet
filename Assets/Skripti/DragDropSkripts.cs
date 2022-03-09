using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Jāizmortē, lai lietotu visus I interfeisus
using UnityEngine.EventSystems;

public class DragDropSkripts : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
	//Velkamajam objektam piestiprinātā komponente
	private CanvasGroup kanvasGrupa;
	//Priekš pārvietojamā ojbekta atrašanās vietas uzglabāšanas
	private RectTransform velkObjRectTransf;
	//Norāde uz objektu skriptu
	public Objekti objektuSkripts;

	// Use this for initialization
	void Start () {
		//Piekļūst objekta piestripinātai CanvasGroup komponentei
		kanvasGrupa = GetComponent<CanvasGroup>();
		//Piekļūst objekta RectTransform komponentei
		velkObjRectTransf = GetComponent<RectTransform>();
	}

	public void OnPointerDown(PointerEventData notikums){
		Debug.Log ("Uzklikšķināts uz velkama objekta!");
	}

	public void OnBeginDrag(PointerEventData notikums){
		Debug.Log ("Uzsākta vilkšana!");
		//Velkot objektu tas paliek caurspīdīgs
		kanvasGrupa.alpha = 0.6f;
		//Lai objektam velkot iet cauri raycast stari
		kanvasGrupa.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData notikums){
		Debug.Log ("Notiek vilkšana!");
		//Maina objekta x, y koordinātas
		velkObjRectTransf.anchoredPosition += notikums.delta / objektuSkripts.kanva.scaleFactor;
	}

	public void OnEndDrag(PointerEventData notikums){
		Debug.Log ("Veikta objekta vilkšana!");
		kanvasGrupa.alpha = 1f;

		//Ja objekts nebija nolikts īstajā vietā
		if (objektuSkripts.vaiIstajaVieta == false) {
			//Tad to atkal var vilkt
			kanvasGrupa.blocksRaycasts = true;
			//Ja nolikts īstajā vietā
		} else {
			//Aizmirst pēdējo objektu, kas vilkts
			objektuSkripts.pedejaisVilktais = null;
		}
		//Ja viens objekts nolikts īstajā vietā, tad lai varētu turpināt vilkt pārējos
		//iestata uz false
		objektuSkripts.vaiIstajaVieta = false;

	}
}
