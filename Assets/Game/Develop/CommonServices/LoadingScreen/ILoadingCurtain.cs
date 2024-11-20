using System;

namespace Game.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingCurtain
    {
        bool IsShowing { get; }
        void Show();
        void Hide();
    }
}
