using antNest.widgets.core;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Controls;

namespace antNest.widgets
{
    public partial class WeatherWidget : UserControl, IWidget
    {
        public WeatherWidget()
        {
            InitializeComponent();
            // todo: fetch api data
            // For v1, we just display placeholder data set via Configure method.
        }

        public void Configure(Dictionary<string, JsonElement> settings)
        {
            if (settings.TryGetValue("City", out var cityElement) && cityElement.ValueKind == JsonValueKind.String)
            {
                CityTextBlock.Text = cityElement.GetString();
            }

            // Placeholder data
            TempTextBlock.Text = "15°C";
            ConditionTextBlock.Text = "Clear Sky";
        }
    }
}
