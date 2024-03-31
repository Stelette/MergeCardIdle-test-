using System;
using Core.Models;
using Implementation.Items.Interfaces;

namespace Implementation.Items
{
    public class TrapItem : Item
    {
        private IDamageable _player;
        private IGameBoard _gameBoard;

        private bool _isEnable = true;

        private int _tempCount = 0;

        public void Construct(IDamageable playerDamageable,IGameBoard gameBoard)
        {
            _player = playerDamageable;
            _gameBoard = gameBoard;
            Subscribe();
        }

        private void Subscribe()
        {
            _gameBoard.OnMoveFinish += OnMove;
        }

        private void OnMove()
        {
            _isEnable = !_isEnable;
            
            if(_isEnable)
                RestoreCount(_tempCount);
            else
            {
                _tempCount = Count;
                RemoveCount(Count);
            }
        }


        public override void Use()
        {
            _player.TakeDamage(Count);

            if (!_player.IsDied())
            {
                //self-destroy
                Die();
            }
        }

        private void OnDestroy()
        {
            if(_gameBoard != null)
                _gameBoard.OnMoveFinish -= OnMove;
        }
    }
}