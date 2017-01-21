# Rewind Controller
Rewind actions in Unity.

The rewind ability in this project is made possible due to the Command Pattern. Which makes implementing a rewind ability surprisingly easy.

All actions in the scene (including player/enemy movement, deaths, projectile release/movement) are encapsulated as actionable command objects. 

Each command has an execute and undo method.

When a command is generated it is added to a global group containing all commands executed.This group simply encapsulates a list and provides access to a Reverse method.
