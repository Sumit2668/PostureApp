using System;
using Android.Views;
using Android.Content;
using Android.Graphics;

namespace LockScreen.Android.Sample
{
	internal class CustomBackground : View
	{
		public CustomBackground (Context context) : base(context)
		{
		}

		public override void Draw (Canvas canvas)
		{
			canvas.DrawRect(0, 0, canvas.Width, canvas.Height, new Paint() { Color = Color.Red });
		}
	}
}