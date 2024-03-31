using UnityEngine.SceneManagement;

namespace Implementation.UI.Windows
{
    public class FinishGameWindow : WindowBase
    {
        public void Continue()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Close();
        }
    }
}