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

## Organization

The game is laid out in a level which contains static sprites for each cloud. Each group of cloud
sprites that create a platform are grouped as children of a single game object. A
`PolygonCollider2D` is applied to the game object with manually drawn boundaries to give the
players something to stand on.

The players are represented by simple, unanimated sprites with `CircleCollider2D` and `BoxCollider2D`
colliders attached. Each has several control scripts and empty `GameObject` children used for
positioning important features required by those scripts. All this structure is wrapped into the
*Hero.prefab* file and instantiated for each player in the game.

The `Main Camera` is automatically controlled such that it pans and zooms to keep both players in
view at all times.

Other components such as the sun, background and logo are self-contained objects with no effect
on gameplay.

## Major Components

### Hero

The `Hero` component controls the main character's visual representation and movement behavior
through the world. Using commands mapped from a `HeroController`, it applies forces to the `Rigidbody2D`
to allow the character to move horizontally, jump or shoot stars.

Two interesting components of the `Hero` are the `ViewportEdgeDetector` and the `GroundDetector`.
Each references an empty child game object of the game object representing the player.
`ViewportEdgeDetector` determines when the player is about to leave the screen so that `Hero`
can halt horizontal motion. `GroundDetector` performs an intersection test with the `Ground`
layer to learn if the hero is touching the ground, and is therefore allowed to jump.

### HeroController

Each `Hero` references a `HeroController` on that object that maps user input to actions. This
abstraction is useful because:
* It allows the controller to be entirely separated from the mechanics of the hero
* Multiple players are distinguished simply by changing the `PlayerNumber` field
* In the future, AI could extend a `HeroController` without having to rewrite anything in `Player`

### CameraFollow

### TitleScreen

## Other Components

For documentation on the common components (`JollyDebug`, `Vector3`, `Vector2`) please see
the `../template/` directory.


