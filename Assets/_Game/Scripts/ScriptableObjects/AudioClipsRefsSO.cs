using UnityEngine;

namespace Game
{
    [CreateAssetMenu()]
    public class AudioClipRefsSO : ScriptableObject
    {
        public AudioClip Click;
        public AudioClip Rebound;
        public AudioClip Inversion;
        public AudioClip[] Fail;
        public AudioClip ScoreCounter;
        public AudioClip ScoreDisplaying;
    }
}
