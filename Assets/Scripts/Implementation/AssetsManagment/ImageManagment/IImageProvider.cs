using System;
using UnityEngine;

namespace Implementation.AssetsManagment.ImageManagment
{
    public interface IImageProvider
    {
        public void Get(string path,Action<Texture2D> callback);
    }
}