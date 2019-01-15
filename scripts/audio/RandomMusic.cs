using UnityEngine;
using System.Linq;

public class RandomMusic : MonoBehaviour {
	public float FadeOutThreshold = 0.05f;
	public float FadeSpeed = 0.5f;
	public AudioSource audioSource;
	public FadeState fadeState = FadeState.None;
	public AudioClip nextClip;
	public AudioClip[] musics;
	private int index;
	private bool nextClipLoop;
	private float nextClipVolume;
	
	public enum FadeState {
		None,
		FadingOut,
		FadingIn
	}

	public void Fade(AudioClip clip, float volume, bool loop) {
		if (clip == null || clip == audioSource.clip)
			return;
		index = Random.Range(0, musics.Length);
		nextClip = musics[index];
		nextClipVolume = volume;
		nextClipLoop = loop;

		if (audioSource.enabled) {
			if (audioSource.isPlaying)
				fadeState = FadeState.FadingOut;
			else FadeToNextClip();
		}
		else FadeToNextClip();
	}

	public void Play() {
		fadeState = FadeState.FadingIn;
		audioSource.Play();
	}
	public void Stop() {
		audioSource.Stop();
		fadeState = FadeState.None;
	}
	
	private void Awake() {
		musics = Resources.LoadAll("common/audio/musics", typeof(AudioClip)).Cast<AudioClip>().ToArray();
		index = Random.Range(0, musics.Length);
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.0f;
		audioSource.clip = musics[Random.Range(0, musics.Length)];
		audioSource.enabled = true;
		Play();
		Fade(musics[index], 1, false);
	}

	private void Start () {
		
	}

	private void FadeToNextClip() {
		audioSource.clip = nextClip;
		audioSource.loop = nextClipLoop;
		fadeState = FadeState.FadingIn;
		if (audioSource.enabled)
			audioSource.Play();
	}

	private void Update() {
		if (!audioSource.enabled)
			return;
		
		if (fadeState == FadeState.FadingOut) {
			if (audioSource.volume > FadeOutThreshold)
				audioSource.volume -= FadeSpeed * Time.deltaTime;
			else FadeToNextClip();
		}
		else if (fadeState == FadeState.FadingIn) {
			if (audioSource.volume < nextClipVolume)
				audioSource.volume += FadeSpeed * Time.deltaTime;
			else fadeState = FadeState.None;
		}
		
		// force load if nothing plays
		else if (!audioSource.isPlaying) {
			Fade(musics[Random.Range(0, musics.Length)], 1, false);
		}
	}
}