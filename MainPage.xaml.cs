using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Diagnostics;

namespace MauiAbsoluteLayout
{
	public partial class MainPage : ContentPage
	{
        private Rectangle _startedBounds;

        public MainPage()
		{
			InitializeComponent();

			var panGesture = new PanGestureRecognizer();
			panGesture.PanUpdated += PanGesture_PanUpdated;
			MyGrid.GestureRecognizers.Add(panGesture);

		}

        private void PanGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _startedBounds = AbsoluteLayout.GetLayoutBounds(MyGrid);
                    break;
                case GestureStatus.Running:
                    var newBounds = new Rectangle(_startedBounds.X + e.TotalX, _startedBounds.Y + e.TotalY, _startedBounds.Width, _startedBounds.Height);
                    AbsoluteLayout.SetLayoutBounds(MyGrid, newBounds);
                    break;
                case GestureStatus.Completed:
                    _startedBounds = Rectangle.Zero;
                    break;
            }

            Debug.WriteLine($"PanGesture_PanUpdated: GestureId={e.GestureId} | StatusType={e.StatusType} | XY={e.TotalX},{e.TotalY} | CurrentBounds={_startedBounds}");
        }
    }
}
