
# GameOfLife

## Description
The GameOfLife project is a console-based implementation of the classic 'Game of Life'. This automated game simulates the life cycle of cells on a grid based on a set of rules. Players set the initial configuration, and the game evolves each turn without further player interaction.

### Rules of the Game:
- Any live cell with fewer than two live neighbors dies (underpopulation).
- Any live cell with two or three live neighbors lives on to the next generation.
- Any live cell with more than three live neighbors dies (overpopulation).
- Any dead cell with exactly three live neighbors becomes a live cell (reproduction).

## Features
- Game logic implementation for the Game of Life.
- Data management for game constants, entities, and saved game states.
- Comprehensive unit testing with Moq framework.

## Installation
To use the GameOfLife application, download the executable and run it on your system. Ensure that you have the necessary runtime environment for the application to function correctly.

## Usage
Upon starting the application, users are presented with four options:

1. **Start Game:** Begins a new game with an initial random configuration.
2. **Change Board Size:** Allows the user to set the dimensions of the game board. The default size is 20x20.
3. **Upload Saved Game:** Users can upload a saved game state from a `.txt` file. The application validates the file and, if valid, loads the game state.
4. **Exit Application:** Closes the GameOfLife application.

### In-Game Controls:
- **S:** Stops the game, returning to the main menu.
- **U:** Uploads the current game state, saving it to a file and after pressing enter returns to current game.
- **P:** Pauses the game, allowing for the game state to be saved or the game to be resumed.

The game runs automatically based on the initial configuration and evolves according to the traditional rules of the Game of Life.
