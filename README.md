UnityKickstart
==============

This is a collection of simple Unity project templates for cooperative video games.

 * [JollyPlatformer](./jollyplatformer) - A side-scrolling 2D platformer game with 2 characters, like New Super Mario Bros.
 * [JollyTopdown](./jollytopdown) - Top-down game where you roll and bounce around a snowy landscape collecting coins
 * [JollyTouch](./jollytouch) - A physics/puzzle game meant for mobile devices

Each project is functional but not feature-complete. They are meant as starting points for your projects so that you
can create something in Unity without having to start from scratch. There are also general code components that we have found
useful in the [template](./template) directory.

All of the projects source code is written in C#. We will be able to give you the most help if you also use this language. 
However, if you are more comfortable with Unityscript, go ahead and use that instead--but we suggest you avoid Boo.

# Unity Tips

## General

* Make sure you [Configure the Unity editor and .gitignore properly](http://stackoverflow.com/questions/18225126/how-to-use-git-for-unity3d-source-control)
* Try to make every scene in your game so the game can be started from that scene. It makes design iteration a lot easier.
  For example, instead of having 20 levels in 1 scene, making 20 scenes each with 1 level makes each one easily editable.
* Push as much design into the Unity editor as possible so that it's easy to edit (and as a bonus, you can tinker with
  it while the game is running!)

## Where can I get some art?

* [2D Art List](http://getprismatic.com/story/1414065792475)
* [Open Game Art](http://opengameart.org/)

## Programmers

Here's a quick rundown of things you absolutely need to know when getting started with Unity that you might not find in
usual tutorials:

* `Prefab` is to `GameObject` as class is to instance. Mostly. Unity is weird: A `Prefab` actually IS a `GameObject` when
  referenced in code, but `Prefab`s should be used as object templates for spawning new objects. Also, `GameObject`s can
  exist without a prefab if you only ever need one of them and that object should be created when the scene loads.
* Empty `GameObject`s are quite common. Use them to execute code in the scene or create world-location references such
  as spawn points or character attachment points.
* Making a setting configurable in the Unity editor just requires it being a public variable. For the most part, you should
  avoid having constants in code and use this instead.
* Make isolated, reusable components whenever possible. Doing so makes it easy to build new functionality by just
  dragging components on to an object and hooking up references.
* All physics-related code should be in `FixedUpdate`
* Your camera is jittery because it should only be moved in `OnPreCull`
* Your players are jittery because forces applied are only computed after OnFixedUpdate, but you are clamping the velocity
  inside that function. Move velocity clamping to `Update` instead (a notable exception to physics-code in `FixedUpdate`)
* The built-in Unity GUI is redrawn every frame using code. There is no GUI editor like in Xcode or Visual Studio.
* MonoBehaviour is spelled with a 'u'. If you get errors about `MonoBehavior` not being defined, that's why.
* It's best to spawn objects as children of another game object to keep them organized in the scene hierarchy and make
  debugging using the editor easier. To do this, set the `GameObject.transform.parent` to another `GameObject.transform`. It
  is perfectly fine to use top-level empty game object containers to do this.
