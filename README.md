# Tic Tac Toe using Minimax

## Table of Contents

* [Introduction](#introduction)
* [Project Setup](#project-setup)
* [MiniMax Explained](#minimax-explained)

## Introduction
The purpose of the project was to create a Tic Tac Toe game, allowing the game to be played between two players, or in a single player mode where the user plays against an AI.

The AI uses the MiniMax algorithm to decide the optimal position for it to move to, making it impossible to beat, and the best outcome being a draw!

![TicTacToeScreenshot](https://user-images.githubusercontent.com/43998430/80810628-2e974700-8bbc-11ea-833e-f565de5580a1.PNG)

Above is a screenshot showing the application in the middle of a game.

## Project Setup
Run the following commands to get a local copy of the Repository:
```
cd <desired file path for install location>

git clone https://github.com/LukeGuest/TicTacToe
```
Open the project in Visual Studio 2019 (2017 should also work), and then run the project!
To start a new game, press File > New Game > Player vs Player/ Player vs AI

## MiniMax Explained
The MiniMax algorithm works by using a tree to map the current game's state, and evaluate the most efficient move to be made, by recursively breaking down each possible outcome, and scoring them respectively.

A visual representation can be seen below:

![TicTacToeDiagram](https://user-images.githubusercontent.com/43998430/80808422-723b8200-8bb7-11ea-9d33-470b92d8f73f.png)

The idea behind the Minimax algorithm is for one player tries to get the highest possible score (the Maximiser - in this case X) and the other tries to get the lowest possible score (the Minimiser - in this case O). The score is calculated by seeing whether anyone has won on that turn, if so, it's passed further up the tree (represented by the black numbers).

In the example tree above, it doesn't matter which option the algorithm chooses, as it can win by choosing two different positions for it to win in the same turn. 

The algorithm can be applied to various other games, such as Chess, but Alpha-beta pruning needs to be added, due to the vast amount of possible moves.
