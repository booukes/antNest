using antNest.widgets.core;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Threading;

namespace antNest.widgets
{
    public partial class ClockWidget : UserControl, IWidget
    {
        private readonly DispatcherTimer _timer;

        public ClockWidget()
        {
            InitializeComponent();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            UpdateTime();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
            DateTextBlock.Text = DateTime.Now.ToString("dddd, dd MMMM");
            var offset = TimeZoneInfo.Local.BaseUtcOffset;
            string sign = offset >= TimeSpan.Zero ? "+" : "-";
            TimeZoneTextBlock.Text = $"UTC{sign}{Math.Abs(offset.Hours)}";

        }

        public void Configure(Dictionary<string, JsonElement> settings)
        {
            // This widget has no settings, but the method is required by the interface.
        }
    }
}
