﻿using WebSnake.Features.UI;
using WebSnake.UI.Impl;

namespace WebSnake.UI
{
    public class UiWindowExtended : UiWindow
    {
        protected UiFade Fade { get; private set; }

        public override void Init(UiFeature uiFeature)
        {
            Fade = uiFeature.Fade;
            base.Init(uiFeature);
        }
    }
}