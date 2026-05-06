using UnityEngine;

namespace TicTacToe
{
    public class GameManager : MonoBehaviour
    {
        private int _scoreX;
        private int _scoreO;
        private string _lastWin;

        private void OnEnable()
        {
            GameEvents.GameWon += OnGameWon;
            GameEvents.GameDrawn += OnGameDrawn;
            GameEvents.WinUndo += UndoLastWin;
        }

        private void UndoLastWin()
        {
            if (_lastWin == "X")
            {
                _scoreX--;
            }
            else
            {
                _scoreO--;
            }
            GameEvents.ScoreChanged?.Invoke(_scoreX, _scoreO);
        }

        private void OnDisable()
        {
            GameEvents.GameWon -= OnGameWon;
            GameEvents.GameDrawn -= OnGameDrawn;
            GameEvents.WinUndo -= UndoLastWin;
        }

        private void Start()
        {
            GameEvents.ScoreChanged?.Invoke(_scoreX, _scoreO);
        }

        private void OnGameWon(string winner)
        {
            _lastWin = winner;
            if (winner == "X")
            {
                _scoreX++;
            }
            else
            {
                _scoreO++;
            }

            GameEvents.ScoreChanged?.Invoke(_scoreX, _scoreO);
            GameEvents.ResultReady?.Invoke($"{winner} wins!");
        }
        

        private void OnGameDrawn()
        {
            GameEvents.ResultReady?.Invoke("Draw!");
        }
    }
}
