using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace antNest.Models
{
    /// <summary>
    /// Represents the overall configuration of the dashboard, containing a list of widgets.
    /// </summary>
    public class DashboardConfig
    {
        [JsonPropertyName("Widgets")]
        public List<WidgetConfig> Widgets { get; set; } = new List<WidgetConfig>();
    }

    /// <summary>
    /// Represents the configuration for a single widget on the dashboard.
    /// </summary>
    public class WidgetConfig
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("GridRow")]
        public int GridRow { get; set; }

        [JsonPropertyName("GridColumn")]
        public int GridColumn { get; set; }

        [JsonPropertyName("RowSpan")]
        public int RowSpan { get; set; } = 1;

        [JsonPropertyName("ColumnSpan")]
        public int ColumnSpan { get; set; } = 1;

        [JsonPropertyName("Settings")]
        public Dictionary<string, JsonElement> Settings { get; set; } = new Dictionary<string, JsonElement>();
    }
}
