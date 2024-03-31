using System.Collections.Generic;
using Core.Interface;
using Core.Models;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Implementation.Items.Jobs
{
    public class ItemShowScaleJob : Job
    {
        private const float ScaleDuration = 0.5f;

        private readonly IEnumerable<IItem> _items;

        public ItemShowScaleJob(IEnumerable<IItem> items, int executionOrder = 0) : base(executionOrder)
        {
            _items = items;
        }

        public override async UniTask ExecuteAsync()
        {
            Sequence itemsSequence = DOTween.Sequence();

            foreach (var item in _items)
            {
                Vector3 destinationScale = item.Transform.localScale;
                item.Transform.localScale = Vector3.zero;
                item.Show();

                _ = itemsSequence.Join(item.Transform.DOScale(destinationScale, ScaleDuration));
            }

            await itemsSequence.SetEase(Ease.OutBounce);
        }
    }
}