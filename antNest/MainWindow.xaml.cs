using antNest.ViewModels;
using antNest.Models;
using antNest.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using antNest.widgets.core;

namespace antNest
{
    public partial class MainWindow : Window
    {
        private readonly DashboardViewModel _viewModel;
        private readonly WidgetRegistry _widgetRegistry;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new DashboardViewModel();
            _widgetRegistry = new WidgetRegistry();
            DataContext = _viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWidgets();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.SaveConfig();
        }

        /// <summary>
        /// Dynamically loads and places widgets onto the grid based on the configuration.
        /// </summary>
        private void LoadWidgets()
        {
            if (_viewModel.DashboardConfig?.Widgets == null) return;

            MainGrid.Children.Clear();

            foreach (var widgetConfig in _viewModel.DashboardConfig.Widgets)
            {
                try
                {
                    // Get the widget type from the registry
                    Type? widgetType = _widgetRegistry.GetWidgetType(widgetConfig.Type);

                    if (widgetType != null && typeof(IWidget).IsAssignableFrom(widgetType))
                    {
                        // Create an instance of the widget
                        if (Activator.CreateInstance(widgetType) is UserControl widgetInstance and IWidget iWidget)
                        {
                            // Configure and place the widget on the grid
                            iWidget.Configure(widgetConfig.Settings);

                            Grid.SetRow(widgetInstance, widgetConfig.GridRow);
                            Grid.SetColumn(widgetInstance, widgetConfig.GridColumn);
                            Grid.SetRowSpan(widgetInstance, widgetConfig.RowSpan);
                            Grid.SetColumnSpan(widgetInstance, widgetConfig.ColumnSpan);

                            MainGrid.Children.Add(widgetInstance);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Widget type '{widgetConfig.Type}' not found or does not implement IWidget.", "Widget Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading widget '{widgetConfig.Type}':\n{ex.Message}", "Widget Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
