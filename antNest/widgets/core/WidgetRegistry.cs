namespace antNest.widgets.core
{
    /// <summary>
    /// Holds a mapping of widget string identifiers to their actual types for dynamic instantiation.
    /// </summary>
    public class WidgetRegistry
    {
        private readonly Dictionary<string, Type> _widgets = new();

        public WidgetRegistry()
        {
            // Register all available widgets here
            RegisterWidget("ClockWidget", typeof(ClockWidget));
            RegisterWidget("WeatherWidget", typeof(WeatherWidget));
            RegisterWidget("SystemStatsWidget", typeof(SystemStatsWidget));
            // To add a new widget, create its UserControl and register it here.
        }

        public void RegisterWidget(string name, Type type)
        {
            if (!_widgets.ContainsKey(name))
            {
                _widgets.Add(name, type);
            }
        }

        public Type? GetWidgetType(string name)
        {
            _widgets.TryGetValue(name, out var type);
            return type;
        }
    }
}
