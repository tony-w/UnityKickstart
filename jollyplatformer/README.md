JollyPlatformer
===============

![JollyPlatformer](./jollyplatformerpreview.png?raw=true)

JollyPlatformer


Controls
--------

Player 1:

 * a/d for movement
 * w to jump
 * s to shoot stars

Player 2:
 * left/right keys for movement
 * up to jump
 * down to shoot stars


# Architecture

## Scenes

`titlescreen` and `level1` are the only two scenes, with the former being the way the game starts.
It functions only to load the first level. There is no way to win or lose the level, but falling
off of the platforms makes the robots no longer controllable.

## Major Components

### Hero

### HeroController

### CameraFollow

### TitleScreen

## Other Components

For documentation on the common components (`JollyDebug`, `Vector3`, `Vector2`) please see
the `../template/` directory.


