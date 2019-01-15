using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomSound : MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip[] sounds;
	private AudioClip soundClip;
	private int index;

 	void Start () {
		sounds = Resources.LoadAll("common/audio/panels", typeof(AudioClip)).Cast<AudioClip>().ToArray();
		audioSource = gameObject.GetComponent<AudioSource>();
		index = Random.Range(0, sounds.Length);
		soundClip = sounds[index];
		audioSource.clip = soundClip;
		InvokeRepeating("playSound", 0.01f, soundClip.length + 1.0f);
	}

	void playSound() {
		audioSource.Play();
	}
}
