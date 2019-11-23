# Axe Man

## Introduction

> Axe Man, Axe Man, does whatever an axe can!

*Axe Man* is a Unity Roguelike game. You need to build four skills with effect
blocks and fight your way through the dungeon. A successful run takes about
fifteen minutes.

The current version is `0.0.2`. The game is playable but it lacks a lot of user
friendly features. Try to kill five Dummies in the dungeon for fun, die
a heroic death, or press Space to reload the game at any time. If you are bold
and curious, edit `Data/skillTemplate.xml` to build your own skill. Please
refer to `manual.md` for more information. The manual is located at the same
place as `AxeMan.exe`. It is also available
[on-line](https://github.com/Bozar/AxeMan/blob/master/manual.md).

The game is inspired from *Baba Is You*, *Into The Breach*, *Captain Forever
Remix* and *Space Grunts*. The ☼masterful☼ tileset, `curses_vector`, is created
by DragonDePlatino for [Dwarf
Fortress](http://www.bay12forums.com/smf/index.php?topic=161328.0). The color
theme, `One Dark Pro`, is created by binaryify for [Visual Studio
Code](https://marketplace.visualstudio.com/items?itemName=zhuangtongfa.Material-theme).

You might be interested in [other
games](https://github.com/Bozar/DevBlog/wiki/GameList) made by me.

## Key Binding

Main mode:

* Arrow keys, `hjkl`: Move PC around. Bump attack an enemy. Active an altar.
  Trigger a trap.
* `x`: Enter examine mode.
* `qwer`: Enter aim mode.
* `m`: View game log.
* Space: [Developer] Reload game.

Examine mode:

* Arrow keys, `hjkl`: Move examine cursor around.
* Esc: Exit examine mode.

Aim mode:

* Arrow keys, `hjkl`: Move aim cursor around.
* `qwer`: Switch to another skill.
* Esc: Exit aim mode.
* Space: Use the selected skill, which might be `Q`, `W`, `E` or `R`.

Please beware that *Axe Man* does not have a waiting key. This is an intended
feature and the idea comes from *Space Grunts*.

## Effect List

Effects appear in many places of the game. Here is the full effect list
abstracted from `manual.md` for quick reference.

Fire:

* Fire Merit: T x 2.
    + Skill cooldown reduces by 2 instead of 1 for 2 turns.
* Fire Flaw: T x 2.
    + All flaws' duration (except Fire Flaw itself) dose not count down for
      2 turns.

Water:

* Water Merit: T x 2.
    + All merits' duration (excpet Water Merit itself) doses not count down for
      2 turns.
* Water Flaw: T x 2.
    + PC's skill cooldown remains unchanged for 2 turns. 
    + NPC's restoration cooldown remains unchanged for 2 turns. 

Air:

* Air Merit: 1 x 2.
    + Both move distance and attack range are increased by 1 for 2 turns. 
* Air Flaw: 1 x 2.
    + Take 1 more damage when being attacked for 2 turns.

Earth:

* Earth Merit: 1 x 2.
    + Reduce 1 damage when being attacked for 2 turns.
* Earth Flaw: 1 x 2.
    + Both move distance and attack range are decreased by 1 for 2 turns. 

vim: set tw=80:
