using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Implementation.AssetsManagment.ImageManagment
{
    public interface ISpriteProvider
    {
        public Task<Sprite> GetAsync(string path);
    }
}