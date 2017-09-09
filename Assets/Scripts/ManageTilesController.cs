using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTilesController : MonoBehaviour {
	public GameObject front;
	public GameObject back;

	public void Manage(string name) {
		Debug.Log (gameObject.name + "#Manage");

		// Flip by z component
		if (name.Equals ("front")) { 
			BoardController.DiactivateGuess (gameObject);
			Flip (true);
		} else {
			BoardController.ActivateGuess (gameObject);
			Flip (false);
		}
	}

	public void Flip(bool isFront) {
		Vector3 frontPosition = front.transform.position;
		Vector3 backPosition = back.transform.position;

		front.GetComponent<SpriteRenderer> ().enabled = !front.GetComponent<SpriteRenderer> ().enabled;
		back.GetComponent<SpriteRenderer> ().enabled = !back.GetComponent<SpriteRenderer> ().enabled;

		if (isFront) {
			front.transform.position = new Vector3 (frontPosition.x, frontPosition.y, 1f); // front is in-front, send it to the back
			back.transform.position = new Vector3 (backPosition.x, backPosition.y, 0f);
		} else {
			back.transform.position = new Vector3 (backPosition.x, backPosition.y, 1f); // back is in-front, send it to the back
			front.transform.position = new Vector3 (frontPosition.x, frontPosition.y, 0f);
		}
	}
}
