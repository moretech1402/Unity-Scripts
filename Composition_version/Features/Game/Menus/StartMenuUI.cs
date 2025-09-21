using UnityEngine;
using UnityEngine.Events;

namespace MC.Game.Menus
{
    public class StartMenuUI : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onStartGame;

        public void StartGame() => _onStartGame?.Invoke();
    }
}
