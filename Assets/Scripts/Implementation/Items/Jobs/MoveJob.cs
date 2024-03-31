using Core.Interface;
using Core.Models;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Implementation.Items.Jobs
{
    public class MoveJob : Job
    {
        private const float MoveDuration = 0.15f;
        
        private readonly IItem _item;
        private readonly Vector3 _endPosition;

        public MoveJob(IItem item,Vector3 endPosition,int executionOrder = 0) : base(executionOrder)
        {
            _item = item;
            _endPosition = endPosition;
        }

        public override async UniTask ExecuteAsync()
        {
            var itemsSequence = DOTween.Sequence();
            
            _ = itemsSequence
                .Join(_item.Transform.DOMove(_endPosition,MoveDuration));
            
            await itemsSequence.SetEase(Ease.OutBounce);
        }
    }
}