using antNest.widgets.core;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace antNest.widgets
{
    public partial class SystemStatsWidget : UserControl, IWidget
    {
        private readonly DispatcherTimer _cpuTimer;
        private readonly DispatcherTimer _ramTimer;
        private readonly DispatcherTimer _gpuTempTimer;
        private readonly HardwareMonitor _monitor;

        public SystemStatsWidget()
        {
            InitializeComponent();

            _monitor = new HardwareMonitor();

            _cpuTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _cpuTimer.Tick += CpuTimer_Tick;

            _ramTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            _ramTimer.Tick += RamTimer_Tick;

            _gpuTempTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _gpuTempTimer.Tick += GpuTempTimer_Tick;

            this.Loaded += (s, e) =>
            {
                _cpuTimer.Start();
                _ramTimer.Start();
                _gpuTempTimer.Start();
            };

            this.Unloaded += (s, e) =>
            {
                _cpuTimer.Stop();
                _ramTimer.Stop();
                _gpuTempTimer.Stop();
                _monitor.Dispose();
            };
        }

        private void CpuTimer_Tick(object? sender, EventArgs e)
        {
            _monitor.UpdateCpu();
            var cpuLoad = _monitor.GetCpuLoad() ?? 0;
            var cpuTemp = _monitor.GetCpuTemperature() ?? 0;
            DoubleAnimation anim = new DoubleAnimation
            {
                To = cpuLoad,
                Duration = TimeSpan.FromMilliseconds(1200),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            CpuProgressBar.BeginAnimation(ProgressBar.ValueProperty, anim);
            CpuValueTextBlock.Text = $"{cpuLoad:F0}% / {cpuTemp:F0}°C";
        }

        private void RamTimer_Tick(object? sender, EventArgs e)
        {
            _monitor.UpdateMemory();
            var usedMb = _monitor.GetUsedMemoryMb() ?? 0;
            var totalMb = _monitor.GetTotalMemoryMb() ?? 1;
            var usagePercent = totalMb > 0 ? (usedMb / totalMb) * 100 : 0;
            DoubleAnimation anim = new DoubleAnimation
            {
                To = usagePercent,
                Duration = TimeSpan.FromMilliseconds(1000),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            RamValueTextBlock.Text = $"{usagePercent:F0}% ({usedMb:F0} MB / {totalMb:F0} MB)";
        }

        private void GpuTempTimer_Tick(object? sender, EventArgs e)
        {
            _monitor.UpdateGpu();
            var gpuTemp = _monitor.GetGpuTemperature() ?? 0;
            TempProgressBar.Value = gpuTemp;
            TempValueTextBlock.Text = $"{gpuTemp:F0}°C";
        }

        public void Configure(Dictionary<string, JsonElement> settings)
        {
            // No settings for this widget in v1
        }
    }
}
