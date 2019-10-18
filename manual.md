# Axe Man User Manual

## Table Of Contents

* Introduction
* Roadmap
* Key Binding
* Main Screen
* Dungeon Building
* PC Attribute
* NPC Attribute
* Status
* Skill

## Introduction

> Axe Man, Axe Man, does whatever an axe can!

*Axe Man* is a Unity Roguelike game. You need to build four skills with effect
blocks and fight your way through the dungeon. A successful run takes about
fifteen minutes.

The game is inspired from *Baba Is You*, *Into The Breach*, *Captain Forever
Remix* and *Space Grunts*. The ☼masterful☼ tileset, `curses_vector`, is created
by DragonDePlatino for [Dwarf
Fortress](http://www.bay12forums.com/smf/index.php?topic=161328.0). The color
theme, `One Dark Pro`, is created by binaryify for [Visual Studio
Code](https://marketplace.visualstudio.com/items?itemName=zhuangtongfa.Material-theme).

You might be interested in [other
games](https://github.com/Bozar/DevBlog/wiki/GameList) made by me.

## Roadmap

Since I have been playing *Titanfall 2* and *RDR2* a lot recently and *The Outer
Worlds* will be released next week (10/25/2019), I might not have enough time
for programming in a few weeks. I am looking forward to publish the final
version of *Axe Man* by the end of April, 2020.

According to my plan, the game will have three difficulties: Easy, Normal and
Hard. It is meant to be played on Hard. Easy and Normal act as tutorials for new
players. A successful run takes about fifteen minutes. In order to survive the
combat, players have to build four skills with effect blocks themselves. You can
try it by editing `Data/skillTemplate.xml`. There will be an in-game UI to guide
player through this.

## Key Binding

Main mode:

* Arrow keys, `hjkl`: Move PC around. Bump attack an enemy. Active an altar.
  Trigger a trap.
* `x`: Enter examine mode.
* `qwer`: Enter aim mode.
* Space: [Developer] Reload game.

Examine mode:

* Arrow keys, `hjkl`: Move examine cursor around.
* Esc: Exit examine mode.

Aim mode:

* Arrow keys, `hjkl`: Move aim cursor around.
* Esc: Exit aim mode.
* Space: Use the selected skill, which might be `Q`, `W`, `E` or `R`.

Please beware that *Axe Man* does not have a waiting key. This is an intended
feature and the idea comes from *Space Grunts*.

## Main Screen

    |---------------|
    | World | Help  |
    |---------------|
    |       | PC    |
    | Board |-------|
    |       | Msg   |
    |---------------|

The main screen is divided into five sections.

Combats happen on Dungeon Board. In addition to PC and NPCs, there are two types
of buildings: altar (diamond) and trap (grey upper-case alphabet). Refer to
*Dungeon Building* for more information.

PC Attribute Panel shows your HP, skill type and cooldown, and active status.
When in aim mode, the panel shows the detailed data of a skill. Refer to *PC
Attribute*, *Status* and *Skill* for more information.

Message Panel prints combat log in normal mode. In examine or aim mode, it shows
the detailed data of a target. Refer to *NPC Attribute* for more information.

World Panel shows version number, seed, game difficulty, altar cooldown and
remaining enemies. Help Panel shows available key inputs for current game mode.

## Dungeon Building

There are two types of buildings in the game: four altars (diamond symbol)
surrounding the center of the dungeon and various traps (grey upper-case
alphabet) on the floor.

Altars block movement. Bumping into an altar grants you 5 HP and it becomes
inactive for 10 turns. Moving towards an inactive altar has no effect. If you
stand adjacent to an altar (north, south, east and west) and kill an enemy, all
altars will be upgraded after a few turns. Altars can be upgraded twice. A high
level altar grants you more HP and has longer cooldown duration.

    |---------------|
    | HP    | CD    |
    |---------------|
    | 5     | 10    |
    | 10    | 15    |
    | 15    | 20    |
    |---------------|

Traps do not block movement. Stepping onto a trap triggers it. The trap
disappears and you will have a negative effect. There are four types of traps:
(F)ire, (W)ater, (A)ir and (E)arth.

You can view altar and trap data in aim or examine mode by locking a target with
cursor. Refer to *Status* for more information.

## PC Attribute

PC Attribute Panel has three columns. The left column shows HP and four skills,
including skill name, cooldown and type. The middle column shows active status,
which might be positive or negative.

Enter aim mode by pressing `q`, `w`, `e` or `r`. In this mode, the left column
shows skill data and the right column shows negative effects (if there are any)
that will be applied to you after using the skill. Refer to *Skill* and *Status*
for more information.

## NPC Attribute

NPC Attribute Panel also has three columns. The left column shows HP, move
distance, attack range and restoration cooldown. Whenever the restoration
cooldown reaches 0, NPC restores 2 HP. The middle cloumn shows attack damage.
Some enemies apply a negative effect to PC by cursing, which is also displayed
here. The right column shows NPC's active effects. Refer to *Status* for more
information.

## Status

### Description Format

Actor status is described in this form:

    [Effect name][Effect type]: [Power] x [Duration]

There are four names (Fire, Water, Air and Earth) and three types (Merit, Flaw
and Curse). As you will see in *Skill* section, Curse is a special type of Flaw.
So we have 8 distinct status: 4 names combined with 2 types.

Effect duration usually counts down by 1 every turn. When the duration reaches
0, the status will be removed at the start of next turn. Effect power will be
described in the next part.

    |----------------------------------|
    | Name  | Merit  | Flaw   | Curse  |
    |----------------------------------|
    | Fire  | Fire+  | Fire-  | Fire?  |
    | Water | Water+ | Water- | Water? |
    | Air   | Air+   | Air-   | Air?   |
    | Earth | Earth+ | Earth- | Earth? |
    |----------------------------------|

### Effect List

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

### Merge Effects

When you have two effects of the same name and type, their power and duration
adds up separately.

* `Fire Merit: T x 1` and `Fire Merit: T x 3` results in `Fire Merit: T x 4`.
* `Earth Flaw: 2 x 3` and `Earth Flaw: 2 x 4` results in `Earth Flaw: 4 x 7`.

When you have two effects of the opposite name and type, the effect with longer
duration remains and their durations negate each other.

* `Fire Merit: T x 3` and `Water Flaw: T x 1` results in `Water Flaw: T x 2`.
    + `Water Flaw` has longer duration. The difference of duration between two
      effects is 2.
* `Air Merit: 2 x 3` and `Earth Flaw: 5 x 2` results in `Air Merit: 2 x 1`.
    + `Air Merit` has longer duration. The difference of duration between two
      effects is 1.
* `Air Merit: 4 x 3` and `Earth Flaw: 3 x 3` cancels each other because they
  have the same duration.

Merits and their oppositing flaws:

    |-------------------------------|
    | Merit         | Flaw          |
    |-------------------------------|
    | Fire Merit    | Water Flaw    |
    | Water Merit   | Fire Flaw     |
    | Air Merit     | Earth Flaw    |
    | Earth Merit   | Air Flaw      |
    |-------------------------------|

## Skill

You can always move 1 step or bump attack an adjacent enemy and deals at least
1 damage. An enemy who has Air Flaw suffers more damage from bump attack.

To use one of the four special skills, first you need to press `q`, `w`, `e` or
`r`. Then move aim cursor to a valid target and press Space to confirm. All
skills have range and cooldown. You cannot use a skill when the aim cursor is
out of range or the skill is in cooldown. Skill type defines which target is
valid. There are four of them: Attack, Curse, Move and Buff.

* Attack: Deal damage to an NPC.
* Curse: Apply negative effects to an NPC.
    + Such effects are called Curse, for example, Fire Curse (Fire?). NPC will
      gain a related negative effect (Fire Flaw in our case) after you use
      a Curse skill.
* Move: Teleport yourself to a position that is not occupied by PC, NPC or
  altar.
* Buff: Apply positive effects to yourself.

Sometimes using a skill will grant yourself negative effects. They are displayed
in the right column of PC Status Panel in aim mode.

vim: set tw=80:
