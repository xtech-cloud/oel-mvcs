using System.Collections;
using System.Collections.Generic;

/*
         ______________________________
        \|/      \|/                   |
    Service -> Model -> Controller -> View
        /|\      /|\         |
         |________|__________|
*/

namespace XTC.oelMVCS
{
    public class Framework
    {
        public StaticPipe getStaticPipe()
        {
            return staticPipe_;
        }

        public DynamicPipe getDynamicPipe()
        {
            return dynamicPipe_;
        }

        public void setConfig(Config _value)
        {
            board_.setConfig(_value);
        }

        public void setLogger(Logger _value)
        {
            board_.setLogger(_value);
        }

        public Framework()
        {
            board_ = new Board();
            staticPipe_ = new StaticPipe(board_);
            dynamicPipe_ = new DynamicPipe(board_);
        }

        ~Framework()
        {
            board_ = null;
            staticPipe_ = null;
            dynamicPipe_ = null;
        }

        /// <summary>
        /// 框架初始化，完成各层中心的实例化
        /// </summary>
        public void Initialize()
        {
            board_.getLogger().Info("initialize framework");
            board_.setViewCenter(new ViewCenter(board_));
            board_.setModelCenter(new ModelCenter(board_));
            board_.setControllerCenter(new ControllerCenter(board_));
            board_.setServiceCenter(new ServiceCenter(board_));
        }

        /// <summary>
        /// 框架安装，完成各层中心已注册组件的安装
        /// 此过程将调用各派生组件的setup方法
        /// </summary>
        public void Setup()
        {
            board_.getLogger().Info("setup framework");
            board_.getViewCenter().Setup();
            board_.getServiceCenter().Setup();
            board_.getModelCenter().Setup();
            board_.getControllerCenter().Setup();
        }

        /// <summary>
        /// 框架拆卸，完成各层中心已注册组件的拆卸
        /// 此过程将调用各派生组件的dismantle方法
        /// </summary>
        public void Dismantle()
        {
            board_.getLogger().Info("dismantle framework");
            board_.getViewCenter().Dismantle();
            board_.getServiceCenter().Dismantle();
            board_.getModelCenter().Dismantle();
            board_.getControllerCenter().Dismantle();
        }


        /// <summary>
        /// 框架销毁，完成各层中心的释放
        /// </summary>
        public void Release()
        {
            board_.getLogger().Info("release framework");
            board_.setViewCenter(null);
            board_.setServiceCenter(null);
            board_.setModelCenter(null);
            board_.setControllerCenter(null);
        }

        private StaticPipe staticPipe_ = null;
        private DynamicPipe dynamicPipe_ = null;
        public Config config_ = null;
        public Logger logger_ = null;
        private Board board_ = null;
    }

}//namespace
