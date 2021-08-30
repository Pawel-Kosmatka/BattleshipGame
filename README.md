# BattleshipGame

Battleship game based on rules: https://en.wikipedia.org/wiki/Battleship_(game)

Ships:
| No. | Class of ship  | Size  | 
|---|---|---|
| 1 | Carrier  | 5  |
| 2 | Battleship  | 4  |  
| 3 | Cruiser  | 3  |   
| 4 | Submarine  | 3  |   
| 5 | Destroyer  |  2 |   

Ships are randomly placed on board, all have to fit on board and not overlap.
Each turn player takes one try to hit the opponent ship.

Core of the game is in class library to allow reusability with console and WEB project.
Basic Idea was to create a class library that could be updated to allow live players to take a game as well.

A simplifying assumption was made that all incoming data is valid, therefore no validation was added to code. Also managing game context is done by singleton class.

Since, with used rules and listed ships there is no chance to not fit all ships in the grid, a simple algorithm was used, adding ships in descending order. With more ships/ different rules some more sophisticated search algorithm could be needed.

Downside of used rules is the possibility of ships being placed without space between them, therefore additionally to grid, a list of ships is created to track when a ship is sink. 
(Actually while writing this I realized there is related bug in my algorithm for finding new target… made temporary fix).

Since AI game is driven by single class - AutoPlay, to avoid passing all information through this class to presentation layer, observer pattern was used. After game start all observers about result of last shot.

Main goal was to connect this app with React.js frontend using SignalR, which is not ready yet. Also not all unit tests are added, and some classes would need refactoring (I’m looking at you GameController) and overall code cleaning.


