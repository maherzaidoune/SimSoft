using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PFE.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(PFE.GradientButton), typeof(GradientButtonRenderer))]

namespace PFE.Droid.Renderers
{
    public class GradientButtonRenderer : VisualElementRenderer<StackLayout>
    {

        private Color StartColor
        {
            get;
            set;
        }
        private Color EndColor
        {
            get;
            set;
        }
        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0, this.StartColor.ToAndroid(), this.EndColor.ToAndroid(), Android.Graphics.Shader.TileMode.Mirror);
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientButton;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR:", ex.Message);
            }
        }
    }
}