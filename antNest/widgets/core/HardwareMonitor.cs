using LibreHardwareMonitor.Hardware;
using System;
using System.Linq;

namespace antNest.widgets.core
{
    // Implementujemy IDisposable, żeby prawidłowo zwalniać zasoby
    public class HardwareMonitor : IDisposable
    {
        private readonly Computer _computer;

        // Pola do przechowywania referencji do komponentów. 
        // Znajdujemy je raz i nie szukamy ponownie.
        private readonly IHardware? _cpu;
        private readonly IHardware? _gpu;
        private readonly IHardware? _ram;

        public HardwareMonitor()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
            };
            _computer.Open();

            // Locates components and caches them
            _cpu = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Cpu);
            _gpu = _computer.Hardware.FirstOrDefault(h =>
                h.HardwareType == HardwareType.GpuNvidia ||
                h.HardwareType == HardwareType.GpuAmd ||
                h.HardwareType == HardwareType.GpuIntel); // Dodano Intel
            _ram = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Memory);
        }

        public void UpdateCpu()
        {
            if (_cpu == null) return;
            _cpu.Update();
            foreach (var sub in _cpu.SubHardware)
            sub.Update();
        }

        public void UpdateGpu()
        {
            if (_gpu == null) return;
            _gpu.Update();
            foreach (var sub in _gpu.SubHardware)
            sub.Update();

        }

        public void UpdateMemory()
        {
            _ram?.Update();
        }

        public float? GetCpuTemperature()
        {
            return _cpu?.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Core"))?.Value;
        }



        public float? GetCpuLoad()
        {
            return _cpu?.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name == "CPU Total")?.Value;
        }

        public float? GetGpuTemperature()
        {
            return _gpu?.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature)?.Value;
        }

        /// <summary>
        /// Gets available System RAM in Mb.
        /// </summary>
        public float? GetAvailableMemoryMb()
        {
            // Zmieniamy na MB od razu, żeby uniknąć pomyłek
            return _ram?.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Data && s.Name == "Memory Available")?.Value * 1024;
        }

        /// <summary>
        /// Gets used System RAM in Mb
        /// </summary>
        public float? GetUsedMemoryMb()
        {
            return _ram?.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Data && s.Name == "Memory Used")?.Value * 1024;
        }

        /// <summary>
        /// Calculates and returns total System RAM in Mb
        /// </summary>
        public float? GetTotalMemoryMb()
        {
            var used = GetUsedMemoryMb();
            var available = GetAvailableMemoryMb();

            if (used.HasValue && available.HasValue)
            {
                return used.Value + available.Value;
            }
            return null;
        }

        public void Dispose()
        {
            // LHM Dispose
            _computer.Close();
        }
    }
}
