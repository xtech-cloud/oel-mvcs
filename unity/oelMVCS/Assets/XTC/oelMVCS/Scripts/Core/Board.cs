namespace XTC.oelMVCS
{
    // 内部通讯的主板
    public class Board
    {
        public ServiceCenter serviceCenter
        {
            get;
            set;
        }

        public ControllerCenter controllerCenter
        {
            get;
            set;
        }

        public ViewCenter viewCenter
        {
            get;
            set;
        }

        public ModelCenter modelCenter
        {
            get;
            set;
        }

        public Config config
        {
            get;
            set;
        }

        public Logger logger
        {
            get;
            set;
        }
    }
}
