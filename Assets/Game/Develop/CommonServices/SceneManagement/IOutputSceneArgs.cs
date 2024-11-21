namespace Game.Develop.CommonServices.SceneManagement
{
    public interface IOutputSceneArgs
    {
        IInputSceneArgs NextSceneInputArgs { get; }
    }

    public abstract class OutputSceneArgs : IOutputSceneArgs
    {
        protected OutputSceneArgs(IInputSceneArgs nextSceneArgs)
        {
            NextSceneInputArgs = nextSceneArgs;
        }

        public IInputSceneArgs NextSceneInputArgs { get; }
    }

    public class OutputGameplayArgs : OutputSceneArgs
    {
        public OutputGameplayArgs(IInputSceneArgs nextSceneArgs) : base(nextSceneArgs)
        {
        }
    }

    public class OutputMainMenuArgs : OutputSceneArgs
    {
        public OutputMainMenuArgs(IInputSceneArgs nextSceneArgs) : base(nextSceneArgs)
        {
        }
    }

    public class OutputBootstrapArgs : OutputSceneArgs
    {
        public OutputBootstrapArgs(IInputSceneArgs nextSceneArgs) : base(nextSceneArgs)
        {
        }
    }
}
