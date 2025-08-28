# Pathfinding

GMR Pathfinding is a Windows Forms application written in C# that demonstrates pathfinding algorithms on a grid-based map. The project visualizes how different algorithms traverse a grid to find the shortest path between two points, making it a useful educational tool for understanding pathfinding concepts.

## Features

- Visual representation of a grid and nodes (cells)
- Support for multiple pathfinding algorithms (e.g., A*, Dijkstra, etc.)
- Interactive UI to set start/end points and obstacles
- Step-by-step visualization of the algorithm’s progress
- Customizable grid size and settings

## Getting Started

### Prerequisites

- Windows OS
- .NET 8.0 SDK or newer
- Visual Studio 2022 or later (recommended for Windows Forms designer support)

### Building and Running

1. Clone the repository:
   ```sh
   git clone https://github.com/alex45101/GMR_Pathfinding.git
   ```
2. Open `GMR_Pathfinding.sln` in Visual Studio.
3. Build the solution (`Ctrl+Shift+B`).
4. Run the project (`F5` or click "Start Debugging").

## Project Structure

- `Cell.cs`, `Grid.cs`, `Graph.cs`: Core data structures for the grid and pathfinding logic
- `Form1.cs`, `Form1.Designer.cs`: Main Windows Form and UI logic
- `Enums.cs`, `VisualState.cs`, `Settings.cs`: Supporting types and configuration
- `IDrawable.cs`: Interface for drawable objects
- `Program.cs`: Application entry point

## Usage

- Set the start and end points by clicking on the grid.
- Add obstacles by clicking or dragging on cells.
- Select the desired algorithm and start the visualization.
- Watch the algorithm find the shortest path in real time.

## License

This project is licensed under the MIT License.
