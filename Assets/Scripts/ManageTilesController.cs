using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTilesController : MonoBehaviour {
	public GameObject front;
	public GameObject back;

	public void Manage(string name) {
		Debug.Log (gameObject.name + "#Manage");

		Vector3 frontPosition = front.transform.position;
		Vector3 backPosition = back.transform.position;

		front.GetComponent<SpriteRenderer> ().enabled = !front.GetComponent<SpriteRenderer> ().enabled;
		back.GetComponent<SpriteRenderer> ().enabled = !back.GetComponent<SpriteRenderer> ().enabled;

		if (name.Equals ("front")) { // Flip by z component
			front.transform.position = new Vector3 (frontPosition.x, frontPosition.y, 1f);
			back.transform.position = new Vector3 (backPosition.x, backPosition.y, 0f);
		} else {
			back.transform.position = new Vector3 (backPosition.x, backPosition.y, 1f);
			front.transform.position = new Vector3 (frontPosition.x, frontPosition.y, 0f);
		}
	}
}
