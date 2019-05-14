# BomberManGamePrototype
Popular 2D Bomber man game

--Architectutre/ Patterns Used
-MVC
-Singleton Pattern
-Factory Pattern

--Data
-Scriptable Object is used for custom data.

--Features/Game play:
-Map spawns in a 2D Array
-Bounds spawns with Map
-Map has random destrucible tiles
-Enemies position is random as well
-Player spawns at top left corner
-Player has ability to spawn bombs using Space bar, One at a time
-Player can move over bombs, enemies cannot
-Player wins after killing all enemies
-Player dies if he comes in contact with bomb
-Player dies if he comes in contact with enemy
-Can Restart Game as many times as you want
-Enemy AI gets random direction
-Player Animations are used via Animator

--Improvements
-Implement lives
-Implement multiple kills, currently single enemy gets killed if same enemies in the same cell
-Implement multiple levels
-Implement multiple types of enemy
