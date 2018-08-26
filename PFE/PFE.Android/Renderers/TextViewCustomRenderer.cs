using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PFE.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PFE.Rendereres.myEntry), typeof(TextViewCustomRenderer))]
namespace PFE.Droid.Renderers
{
    class TextViewCustomRenderer : EntryRenderer
    {
        public TextViewCustomRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                Control.Gravity = GravityFlags.CenterVertical;
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Argb(200, 100, 181, 246));
                gd.SetCornerRadius(5);
                gd.SetStroke(2, global::Android.Graphics.Color.Gray);
                Control.SetBackgroundDrawable(gd);
                Control.SetPadding(20, 0, 0, 0);
            }
        }
        }
}