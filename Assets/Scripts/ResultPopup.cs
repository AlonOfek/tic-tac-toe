using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _newGameButton;

        private void Awake()
        {
            _newGameButton.onClick.AddListener(NewGame);
            _root.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.ResultReady += OnResultReady;
            GameEvents.WinUndo += Hide;
        }

        private void OnDisable()
        {
            GameEvents.ResultReady -= OnResultReady;
            GameEvents.WinUndo -= Hide;
        }

        private void OnResultReady(string message)
        {
            _resultText.text = message;
            _root.SetActive(true);
        }

        private void NewGame()
        {
            Hide();
            GameEvents.ResetBoard?.Invoke();
        }
        private void Hide()
        {
            _root.SetActive(false);
        }
    }
}
