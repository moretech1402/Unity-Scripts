using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity.Utils
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] float _duration = 1f;
        [SerializeField] UnityEvent<float, float> _onTimePassed;
        [SerializeField] UnityEvent _onTimerCompleted;

        public bool IsRunning { get; set; } = false;

        float _elapsedTime = 0f;

        void Awake() => Reset();

        void Update()
        {
            if (!IsRunning) return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _duration)
            {
                _elapsedTime = _duration;
                IsRunning = false;
                _onTimerCompleted?.Invoke();
            }

            NotifyTimePassed();
        }

        public void Reset()
        {
            _elapsedTime = 0f;
            IsRunning = false;
            NotifyTimePassed();
        }

        private void NotifyTimePassed() => _onTimePassed?.Invoke(_elapsedTime, _duration);

    }
}
