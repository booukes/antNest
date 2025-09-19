using System.Collections.Generic;
using System.Text.Json;

namespace antNest.widgets.core
{
    /// <summary>
    /// Defines the contract for all widgets in AntNest.
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// Configures the widget with settings from the JSON config.
        /// </summary>
        /// <param name="settings">A dictionary of settings for the widget.</param>
        void Configure(Dictionary<string, JsonElement> settings);
    }
}
