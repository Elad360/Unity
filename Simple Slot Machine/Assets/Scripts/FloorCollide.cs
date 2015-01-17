using UnityEngine;
using System.Collections;

public class FloorCollide : MonoBehaviour 
{
	public AudioClip hitSound;
	void OnCollisionEnter (Collision col) 
	{
		audio.PlayOneShot(hitSound);
	}
}
