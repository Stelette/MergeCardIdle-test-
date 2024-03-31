using Implementation.UI.Enum;

namespace Implementation.UI.Services
{
    public interface IWindowService
    {
        void Open(WindowsId windowsId, params object[] optionalParams);

        void CloseCurrentWindow();
    }
}