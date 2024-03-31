using UnityEngine;

namespace Implementation.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        protected object[] _optionalParams;
        public virtual void Close() 
            => Destroy(gameObject);

        public virtual void Initialize(params object[] optionalParams)
            => _optionalParams = optionalParams;
    }
}