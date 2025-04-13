using UnityEngine;

namespace Game
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayClickSound()
        {
            PlaySound(_audioClipRefsSO.Click, transform.position);
        }

        public void PlayInversionSound()
        {
            PlaySound(_audioClipRefsSO.Inversion, transform.position);
        }

        public void PlayReboundSound()
        {
            PlaySound(_audioClipRefsSO.Rebound, transform.position);
        }

        public void PlayFailSound()
        {
            PlaySound(_audioClipRefsSO.Fail, transform.position);
        }

        public void PlayScoreCounterSound()
        {
            PlaySound(_audioClipRefsSO.ScoreCounter, transform.position);
        }

        public void PlayScoreDisplayingSound()
        {
            PlaySound(_audioClipRefsSO.ScoreDisplaying, transform.position);
        }

        private void PlaySound(AudioClip[] audioClipArray, Vector3 playPosition, float volume = 1f)
        {
            PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], playPosition, volume);
        }

        private void PlaySound(AudioClip audioClip, Vector3 playPosition, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, playPosition, volume);
        }
    }
}
