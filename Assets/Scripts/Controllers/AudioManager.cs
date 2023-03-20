using System;
using UnityEngine;

namespace Controllers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip matchSound;
        private AudioSource _audioSource;
        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                _audioSource = GetComponent<AudioSource>();
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlayMatchSound()
        {
            AudioSource.PlayClipAtPoint(matchSound, Vector3.zero);
        }
    }
}
