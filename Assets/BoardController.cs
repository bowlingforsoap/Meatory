using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
	// References
	public GameObject guess; // First guess
	public GameObject rightGuess; // Guess on the left
	public GameObject bottomGuess; // Guess on the bottom
	public int rows;
	public int cols;

	public Transform m_leftWin; // Position of where to place the left win
	public Transform m_middleWin; // .. the middle win
	public Transform m_rightWin; // .. the right win

	public GameObject m_winView;
	public GameObject m_cowPlaceHolder;

	public static GameObject cowPlaceHolder;
	public static GameObject winView;

	public static Transform leftWin; // Position of where to place the left win
	public static Transform middleWin; // .. the middle win
	public static Transform rightWin; // .. the right win

	private static GameObject[] activeGuesses = {null, null};
	private static int guessCount = 0;
	private static GameObject[] wins = { null, null, null };
	private static int winCount = 0;
	private static BoardController self;

	private Sprite[] sprites; // All sprites in resources folder

	void Start() {
		self = this;
		leftWin = m_leftWin;
		middleWin = m_middleWin;
		rightWin = m_rightWin;
		winView = m_winView;
		cowPlaceHolder = m_cowPlaceHolder;

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
		cowPlaceHolder.SetActive (false);

		if (guessCount >= 2) {
			// Check if previous active are the same
			if (activeGuesses [0] != null && activeGuesses [1] != null && activeGuesses [0].name != null && activeGuesses [0].name.Equals (activeGuesses [1].name)) {
				GameObject win = Object.Instantiate (activeGuesses [0]);
				win.transform.SetParent (self.gameObject.transform);
				switch (winCount) {
				case 0:
					win.transform.position = leftWin.position;
					break;
				case 1:
					win.transform.position = middleWin.position;
					break;
				case 2:
					win.transform.position = rightWin.position;
					break;
				}
				win.transform.localScale = new Vector3 (2f, 2f, 2f);

				activeGuesses [0].SetActive (false);
				activeGuesses [1].SetActive (false);

				wins [winCount++] = win;

				if (winCount == 3) { // If we win
					//BoardController.self.gameObject.SetActive(false);
					winView.SetActive(true);
				}
			} else {

				// Flip the previous active
				activeGuesses [0].GetComponent<ManageTilesController> ().Flip (true);
				activeGuesses [1].GetComponent<ManageTilesController> ().Flip (true);
			}
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
