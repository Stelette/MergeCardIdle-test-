using Core.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Implementation.Common
{
    public class MoveStepsView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI moveT;
        
        private IGameBoard _gameBoard;
        private int _move = 0;

        [Inject]
        public void Construct(IGameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        private void Start()
        {
            _gameBoard.OnMoveFinish += OnMove;
            UpdateUI();
        }

        private void OnMove()
        {
            _move++;
            UpdateUI();
        }

        private void UpdateUI()
        {
            moveT.text = "Move: " + _move;
        }
    }
}
