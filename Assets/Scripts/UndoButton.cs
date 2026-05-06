using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class UndoButton : MonoBehaviour
    {
        [SerializeField] private Button undoButton;

        private void Awake()
        {
            undoButton.onClick.AddListener(Undo);
        }

        private void OnDisable()
        {
            undoButton.onClick.RemoveListener(Undo);
        }

        private void Undo()
        {
            GameEvents.Undo?.Invoke();
        }
    }
}