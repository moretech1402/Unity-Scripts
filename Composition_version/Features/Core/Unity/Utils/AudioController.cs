using UnityEngine;

namespace MC.Core.Unity.Utils
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("No audio clip provided.");
                return;
            }

            if (_audioSource != null)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (_audioSource != null && clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}
