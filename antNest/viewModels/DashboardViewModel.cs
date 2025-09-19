using antNest.Models;
using antNest.ViewModels;
using antNest.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace antNest.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private DashboardConfig? _dashboardConfig;
        public DashboardConfig? DashboardConfig
        {
            get => _dashboardConfig;
            set => SetProperty(ref _dashboardConfig, value);
        }

        private const string ConfigPath = "config/DashboardConfig.json";

        public DashboardViewModel()
        {
            LoadConfig();
        }

        /// <summary>
        /// Loads the dashboard configuration from the JSON file.
        /// </summary>
        public void LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    var json = File.ReadAllText(ConfigPath);
                    DashboardConfig = JsonSerializer.Deserialize<DashboardConfig>(json);
                }
                else
                {
                    // Create a default config if not found
                    DashboardConfig = new DashboardConfig();
                    MessageBox.Show("Configuration file not found. A default empty dashboard will be loaded.", "Config Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load dashboard configuration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DashboardConfig = new DashboardConfig();
            }
        }

        /// <summary>
        /// Saves the current dashboard configuration to the JSON file.
        /// </summary>
        public void SaveConfig()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(DashboardConfig, options);

                // Ensure directory exists
                var directory = Path.GetDirectoryName(ConfigPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save dashboard configuration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
