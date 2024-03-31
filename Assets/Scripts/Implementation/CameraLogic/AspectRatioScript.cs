using UnityEngine;

namespace Implementation.CameraLogic
{
    public class AspectRatioScript : MonoBehaviour
    {
        public float targetAspect;

        void Start () 
        {
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float scaleHeight = windowAspect / targetAspect;
            Camera camera = Camera.main;

            if (scaleHeight < 1.0f)
            { 
                camera.orthographicSize /= scaleHeight;
            }
        }
    }
}
