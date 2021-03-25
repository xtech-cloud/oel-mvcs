﻿using System.Collections;
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
        public StaticPipe staticPipe
        {
            get;
            private set;
        }

        public DynamicPipe dynamicPipe
        {
            get;
            private set;
        }

        public Config config
        {
            set
            {
                board_.config = value;
            }
        }

        public Logger logger
        {
            set
            {
                board_.logger = value;
            }
        }

        private Board board_
        {
            get;
            set;
        }

        public Framework()
        {
            board_ = new Board();
            staticPipe = new StaticPipe(board_);
            dynamicPipe = new DynamicPipe(board_);
        }

        ~Framework()
        {
            board_ = null;
            staticPipe = null;
            dynamicPipe = null;
        }

        public void Initialize()
        {
            board_.logger.Info("initialize framework");
            board_.viewCenter = new ViewCenter(board_);
            board_.modelCenter = new ModelCenter(board_);
            board_.controllerCenter = new ControllerCenter(board_);
            board_.serviceCenter = new ServiceCenter(board_);
        }

        public void Setup()
        {
            board_.logger.Info("setup framework");
            board_.viewCenter.Setup();
            board_.serviceCenter.Setup();
            board_.modelCenter.Setup();
            board_.controllerCenter.Setup();
        }

        public void Dismantle()
        {
            board_.logger.Info("dismantle framework");
            board_.viewCenter.Dismantle();
            board_.serviceCenter.Dismantle();
            board_.modelCenter.Dismantle();
            board_.controllerCenter.Dismantle();
        }


        public void Release()
        {
            board_.logger.Info("release framework");
            board_.viewCenter = null;
            board_.serviceCenter = null;
            board_.modelCenter = null;
            board_.controllerCenter = null;
        }
    }

}//namespace
