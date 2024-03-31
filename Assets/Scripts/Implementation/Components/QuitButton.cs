using UnityEngine;

namespace Implementation.Components
{
    public class QuitButton : MonoBehaviour
    {
        public void QuitClick()
        {
            Application.Quit();
        }
    }
}