using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public static GameController instance;
	public GameObject spawn;
	public Canvas canvas;
	public GameObject prefabObject;
	public Button spinButton;
	public Button exitButton;
	public GameObject floor;
	public Material[] materials;
	public GameObject[] particles;
	public GameObject coin;
	public Text winningText;
	public AudioClip spinReelSound;
	public AudioClip winningSound;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start() 
	{
		spinButton.onClick.AddListener(SpinReel);
		exitButton.onClick.AddListener(ExitGame);
		StartCoroutine(InstantiateTiles());
	}

	void ExitGame()
	{
		Debug.Log("Exit");
		Application.Quit();
	}

	void SpinReel()
	{
		audio.PlayOneShot(spinReelSound);
		spinButton.interactable = false;
		winningText.gameObject.SetActive(false);
		StartCoroutine(EnableFloorAfterCleaning());
	}
	
	IEnumerator EnableFloorAfterCleaning()
	{
		while(GameObject.Find("Cube(Clone)") != null)
		{
			yield return new WaitForSeconds(0.3f);

		}
		floor.SetActive(true);
		StartCoroutine(InstantiateTiles());
	}

	public IEnumerator InstantiateTiles()
	{
		int randomNumber = Random.Range(0,3);
		for (int x=0; x<=4; x+=2)
		{
			for (int y=0; y<=6; y+=2)
			{
				GameObject tileObj = (GameObject)Instantiate(prefabObject, new Vector3(spawn.transform.position.x + x, spawn.transform.position.y + y, spawn.transform.position.z), Quaternion.identity);
				int randomId = Random.Range(0,3);
				tileObj.renderer.material = materials[randomId];
				tileObj.transform.parent = canvas.transform;
				if (randomId == randomNumber)
				{
					GameObject particleObj = (GameObject)Instantiate(particles[randomId]);
					particleObj.transform.parent = tileObj.transform;
					particleObj.transform.localPosition = new Vector3(0f, 0f, -0.5f);
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
		yield return new WaitForSeconds(2f);
		audio.PlayOneShot(winningSound);
		winningText.gameObject.SetActive(true);
		for (int i=0; i<10; i++)
		{
			Instantiate(coin, new Vector3(Random.Range(-5,5), spawn.transform.position.y + 2f, spawn.transform.position.z - 10f), Quaternion.identity);
			yield return new WaitForSeconds(0.1f);
		}
		spinButton.interactable = true;
	}
}
