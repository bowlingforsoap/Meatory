using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
	public static GameObject[] activeGuesses = {null, null};

	// References
	public GameObject guess; // First guess
	public GameObject rightGuess; // Guess on the left
	public GameObject bottomGuess; // Guess on the bottom
	public int rows;
	public int cols;

	void Start() {
		Transform guessTransform;
		Transform rightGuessTransform;
		Transform bottomGuessTransform;
		Vector3 positionDiffRight;
		Vector3 positionDiffBottom;

		guessTransform = guess.transform;
		rightGuessTransform = rightGuess.transform;
		bottomGuessTransform = bottomGuess.transform;

		positionDiffRight = rightGuessTransform.position - guessTransform.position;
		positionDiffBottom = bottomGuessTransform.position - guessTransform.position;

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				GameObject newGuess;
				newGuess = Object.Instantiate (guess);
				newGuess.transform.position += i * positionDiffBottom + j * positionDiffRight;
				newGuess.transform.SetParent (gameObject.transform);
				newGuess.SetActive (true);
			}
		}

		
	}

	public static void UpdateBoard() {
	}
}
