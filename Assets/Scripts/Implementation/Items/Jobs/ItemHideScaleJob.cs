using System.Collections.Generic;
using Core.Interface;
using Core.Models;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Implementation.Items.Jobs
{
    public class ItemHideScaleJob : Job
    {
        private const float ScaleDuration = 0.5f;

        private readonly IEnumerable<IItem> _items;

        public ItemHideScaleJob(IEnumerable<IItem> items, int executionOrder = 0) : base(executionOrder)
        {
            _items = items;
        }

        public override async UniTask ExecuteAsync()
        {
            var itemsSequence = DOTween.Sequence();

            foreach (var item in _items)
            {
                _ = itemsSequence
                    .Join(item.Transform.DOScale(Vector3.zero, ScaleDuration));
            }

            await itemsSequence.SetEase(Ease.OutBounce);

            foreach (var item in _items)
            {
                item.Hide();
            }
        }
    }
}