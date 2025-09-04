using UnityEngine;

namespace MC.Game.Alchemy
{
    public class AlchemyAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _musicClip;

        void Awake()
        {
            if (_musicSource == null)
            {
                Debug.LogWarning("AudioSource component is not assigned.");
                enabled = false;
                return;
            }

            if (_musicClip == null)
            {
                Debug.LogWarning("Music clip is not assigned.");
                enabled = false;
                return;
            }

            _musicSource.clip = _musicClip;
            _musicSource.Stop();
            _musicSource.loop = true;
        }

        void Start()
        {
            PlayMusic();
        }

        public void PlayMusic()
        {
            if (_musicSource != null && !_musicSource.isPlaying)
            {
                _musicSource.Play();
            }
        }
    }
}
