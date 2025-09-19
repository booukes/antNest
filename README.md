# 🐜 AntNest Dashboard

**Your personal command center for the desktop.**  

⚠️ **Important:**  antNest is still in very early development. Expect a terrible app with many bugs, issues and exposed API keys.

AntNest is a clean, modern, and extensible launcher-style dashboard for Windows. Built with **WPF**, it’s powerful, easy to customize, and lets you see everything that matters in one glance—no more alt-tabbing like a maniac.  

---

## 🔥 Key Features

- **Dynamic Grid Layout** – Widgets neatly arranged in a **4x3 grid** for now, dynamic grid layout is the no. 1 priority for development.  
- **JSON Configuration** – Customize layout and widget settings via a simple JSON file. No recompiling required.  
- **Extensible Widget System** – Build and add new widgets in a breeze.  
- **Modern Dark Theme** – Sleek, eye-friendly, and ready for future theming expansion.  

---

## 🚀 Getting Started

1. **Clone the repository**  

    ```bash
    git clone https://github.com/booukes/antNest.git
    ```

2. **Open in Visual Studio**  
   Open `antNest.sln` in **Visual Studio 2022+**  

3. **Restore dependencies**  
   NuGet packages should restore automatically  

4. **Run the project**  
   Hit **F5** and boom—your dashboard is live  

⚠️ **Important:**  
The `SystemStatsWidget` uses `LibreHardwareMonitor` for CPU & RAM usage. If stats show `"Error"`, try running **Visual Studio** or the compiled app as an **Administrator**.  

---

## ⚙️ Configuration

Your entire dashboard layout is controlled by **`config/DashboardConfig.json`**:  

    ```json
    {
      "Widgets": [
        {
          "Type": "ClockWidget",
          "GridRow": 0,
          "GridColumn": 0,
          "Settings": {}
        },
        {
          "Type": "SystemStatsWidget",
          "GridRow": 1,
          "GridColumn": 0,
          "RowSpan": 1,
          "ColumnSpan": 2,
          "Settings": {}
        }
      ]
    }
    ```

**Fields explained:**

    ```text
    Type        : Name of the widget class. Must match a widget registered in WidgetRegistry.cs
    GridRow     : Top-left cell row (0-indexed)
    GridColumn  : Top-left cell column (0-indexed)
    RowSpan     : (Optional) Number of rows to span (default 1)
    ColumnSpan  : (Optional) Number of columns to span (default 1)
    Settings    : Widget-specific settings (JSON object), e.g., city for a weather widget
    ```

---

## 🛠️ Adding a New Widget

1. **Create the UserControl**  
   - Add a new UserControl in the `Widgets` folder (e.g., `SpotifyWidget.xaml`)  
   - Design the UI in XAML  

2. **Implement the Logic**  
   - In `SpotifyWidget.xaml.cs`, implement the `IWidget` interface  
   - Implement `Configure(Dictionary<string, JsonElement> settings)` to read settings from the JSON  

3. **Register the Widget**  
   - Open `Widgets/WidgetRegistry.cs` and add:  

    ```csharp
    RegisterWidget("SpotifyWidget", typeof(SpotifyWidget));
    ```

4. **Add it to the Config**  
   - Open `config/DashboardConfig.json` and add your widget entry  

Done! Your widget will now load on the dashboard.  

---

## 🔮 Future Plans

- [ ] **Drag & Resize** – Move and resize widgets with your mouse  
- [ ] **More Widgets** – Spotify, Steam, custom hardware monitors  
- [ ] **Theming Engine** – Light/Dark mode, Glassmorphism, custom themes  
- [ ] **Extensive JSON Configuration** – Manage widgets and settings with JSON.

---

## 🙌 Contributing

Got a cool idea? Found a bug? Open an issue or submit a pull request. antNest is open-source and welcomes all contributions!
