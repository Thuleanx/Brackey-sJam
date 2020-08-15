using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	[System.Serializable]
	public class Sound {
		public string name;
		public AudioClip clip;

		[Range(0f, 1f)]
		public float volume = 1;

		[Range(0.1f, 3f)]
		public float pitch = 1f;

		public bool loop;

		[HideInInspector]
		public AudioSource source;
	}

	public static AudioManager Instance;

	[SerializeField] string defaultMusic;
	[SerializeField] List<Sound> sounds = new List<Sound>();
	Dictionary<string, int> soundIndex = new Dictionary<string, int>();

	string currentMusic;

	void Awake() {
		if (Instance == null)
			Instance = this;
		else Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		for (int i = 0; i < sounds.Count; i++)
			soundIndex[sounds[i].name] = i;	
		foreach (Sound s in sounds) {
			GameObject audioObject = new GameObject(s.name, typeof(AudioSource));
			audioObject.transform.parent = transform;
			s.source = audioObject.GetComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.clip = s.clip;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

		defaultMusic = "Music" + SceneManager.GetActiveScene().name;
		if (soundIndex.ContainsKey(defaultMusic))
			Play(defaultMusic);
	}

	public void Play(string name) {
		if (name.StartsWith("Music")) {
			if (currentMusic != null) sounds[soundIndex[currentMusic]].source.Stop();
			currentMusic = name;
		}
		if (soundIndex.ContainsKey(name))
			sounds[soundIndex[name]].source.Play();
	}
}