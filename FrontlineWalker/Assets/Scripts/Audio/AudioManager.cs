using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
	
	[RequireComponent(typeof(AudioSource))]
	
	public class AudioManager : MonoBehaviour
	{
		public AudioMixerGroup mixerGroup;

		public Sound[] sounds;

		void Awake()
		{
			foreach (Sound s in sounds)
			{
				if (s.source == null)
				{
					s.source = gameObject.GetComponent<AudioSource>();
				}
				s.source.volume = s.volume;
				s.source.clip = s.clip;
				s.source.loop = s.loop;

				s.source.outputAudioMixerGroup = mixerGroup;
			}
		}

		public void Play(string sound)
		{
			Sound s = FindSound(sound);

			s.source.clip = s.clip;
			
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
			
			s.source.Play();
		}

		public void Pause(string sound)
		{
			Sound s = FindSound(sound);
			
			s.source.clip = s.clip;
			
			s.source.Pause();
		}
		
		public void Stop(string sound)
		{
			Sound s = FindSound(sound);
			
			s.source.clip = s.clip;
			
			s.source.Stop();
		}

		private Sound FindSound(String soundName)
		{
			Sound s = Array.Find(sounds, item => item.name.Equals(soundName));
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return null;
			}
			
			Debug.Log(s.name);

			return s;
		}

		public void StopAll()
		{
			foreach (var sound in sounds)
			{
				sound.source.Stop();
			}
		}
		
		public void PauseAll()
		{
			foreach (var sound in sounds)
			{
				sound.source.Pause();
			}
		}

		private void Update()
		{
			foreach (var sound in sounds)
			{
				sound.source.volume = sound.volume * SceneController.instance.soundVolume / 100f * SceneController.instance.masterVolume / 100f;
			}
		}
	}
}
