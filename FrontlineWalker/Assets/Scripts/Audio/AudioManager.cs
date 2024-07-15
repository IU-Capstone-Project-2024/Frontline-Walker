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
				s.source = gameObject.GetComponent<AudioSource>();
				s.source.volume = s.volume;
				s.source.clip = s.clip;
				s.source.loop = s.loop;

				s.source.outputAudioMixerGroup = mixerGroup;
			}
		}

		public void Play(string sound)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
			
			s.source.Play();
		}

		public void Pause(string sound)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}
			
			s.source.Pause();
		}
		
		public void Stop(string sound)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}
			
			s.source.Stop();
		}
	}
}
