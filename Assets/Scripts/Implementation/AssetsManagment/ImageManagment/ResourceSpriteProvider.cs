using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Implementation.AssetsManagment.ImageManagment
{
    public class ResourceSpriteProvider : ISpriteProvider
    {
        public Task<Sprite> GetAsync(string path)
        {
            //var tcs = new TaskCompletionSource<Sprite>();
            Sprite sprite = Resources.Load<Sprite>(path);;
            //tcs.TrySetResult(sprite); 
            //return tcs.Task;
            return Task.FromResult(sprite);
        }
    }
}