using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using PFE.iOS.Renderers;
using PFE.Rendereres;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(PFE.GradientButton), typeof(GradientButtonRenderer))]

namespace PFE.iOS.Renderers
{
    class GradientButtonRenderer : VisualElementRenderer<StackLayout>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientButton stack = (GradientButton)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();

            CGColor endColor = stack.EndColor.ToCGColor();

            var gradientLayer = new CAGradientLayer()
            {
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };

            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}