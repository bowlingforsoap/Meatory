using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
	public static GameObject[] activeGuesses = {null, null};
	private static int guessCount = 0;

	// References
	public GameObject guess; // First guess
	public GameObject rightGuess; // Guess on the left
	public GameObject bottomGuess; // Guess on the bottom
	public int rows;
	public int cols;

	private Sprite[] sprites; // All sprites in resources folder

	void Start() {
		Transform guessTransform;
		Transform rightGuessTransform;
		Transform bottomGuessTransform;
		Vector3 positionDiffRight;
		Vector3 positionDiffBottom;

		// Load 
		sprites = Resources.LoadAll<Sprite>("");
		foreach (var t in sprites)
		{
			Debug.Log(t.name);
		}

		guessTransform = guess.transform;
		rightGuessTransform = rightGuess.transform;
		bottomGuessTransform = bottomGuess.transform;

		positionDiffRight = rightGuessTransform.position - guessTransform.position;
		positionDiffBottom = bottomGuessTransform.position - guessTransform.position;

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				GameObject newGuess;
				Transform frontTransform;
				GameObject front;
				SpriteRenderer spriteRenderer; // newGuesse's SpriteRenderer

				// Displace
				newGuess = Object.Instantiate (guess);
				newGuess.transform.position += i * positionDiffBottom + j * positionDiffRight;
				newGuess.transform.SetParent (gameObject.transform);

				// Set a different sprite image
				frontTransform = newGuess.transform.GetChild(0);
				front = frontTransform.gameObject;
				spriteRenderer = front.GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = sprites [Random.Range (0, sprites.Length)];
				newGuess.name = spriteRenderer.sprite.name;

				newGuess.SetActive (true);
			}
		}

		
	}

	public static void ActivateGuess(GameObject guess) {
		if (guessCount >= 2) {
			// Flip the previous active
			activeGuesses [0].GetComponent<ManageTilesController> ().Flip(true);
			activeGuesses [1].GetComponent<ManageTilesController> ().Flip(true);
			activeGuesses [0] = null;
			activeGuesses [1] = null;
			guessCount = 0;
		}
		activeGuesses [guessCount++] = guess; 	

		Debug.Log ("Active guess count: " + guessCount);
		Debug.Log("Guesses: " + activeGuesses[0] + ", " + activeGuesses[1]);
	}

	public static void DiactivateGuess(GameObject guess) {
		if (activeGuesses [0] != null && activeGuesses [0].name == guess.name) {
			activeGuesses [0] = activeGuesses [1];
			activeGuesses [1] = null;
			guessCount--;
		} else if (activeGuesses [1] != null && activeGuesses [1].name == guess.name) {
			activeGuesses [1] = null;
			guessCount--;
		}

		Debug.Log ("Active guess count: " + guessCount);
		Debug.Log("Guesses: " + activeGuesses[0] + ", " + activeGuesses[1]);
	}
		
}
