UnityKickstart
==============

This is a collection of simple Unity project templates for cooperative video games.

 * [JollyPlatformer](./jollyplatformer) - A side-scrolling 2D platformer game with 2 characters, like New Super Mario Bros.
 * [JollyTopdown](./jollytopdown) - Top-down game where you roll and bounce around a snowy landscape collecting coins
 * [JollyTouch](./jollytouch) - A physics/puzzle game meant for mobile devices

Each project is fully functional but not feature-complete. They are meant as starting points for your projects so that you
can create something in Unity without having to start from scratch. There are also general code components that we have found
useful in the [template](./template) directory.

All of the projects source code is written in C#. We will be able to give you the most help if you also use this language. 
However, if you are more comfortable with Unityscript, go ahead and use that instead--but we suggest you avoid Boo.

# General Unity Tips

## First-Time User Tips for Programmers

Here's a quick rundown of things you absolutely need to know when getting started with Unity that you might not find in
usual tutorials:
* If you're using Git for source control, make sure you [Configure the editor and .gitignore properly](http://stackoverflow.com/questions/18225126/how-to-use-git-for-unity3d-source-control)
* `Prefab` is to `GameObject` as class is to instance. However, a `GameObject` without a prefab is like an instance of the
  global root class type `object`.
* Empty `GameObject`s are quite common. Use them to execute code in the scene or create world-location references such
  as spawn points or character attachment points.
* Making a setting configurable in the Unity editor just requires it being a public variable. For the most part, you should
  avoid having constants in code and use this instead.
* Make separate, reusable components whenever possible. Doing so makes it easy to build new functionality by just
  dragging components on to an object and hooking up references.
* All physics-related code should be in the `FixedUpdate` function.
* Your camera is jittery because it should only be moved in `OnPreCull`
* MonoBehaviour is spelled with a 'u'. If you get errors about `MonoBehavior` not being defined, that's why.
* The built-in Unity GUI is redrawn every frame from scratch. There is no GUI editor like in Xcode or Visual Studio.

## 