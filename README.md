# H3Roguelike
### <span style="color:#C39BD3 ">Scrum roles:</span>

Product Owner: Michael.</br>
Developer: Victor.</br>
Scrum Master: Pierre.

### <span style="color:#C39BD3 ">End Product:</span>

Small procedural generated Rogue-lite game.

### <span style="color:#C39BD3 ">Definition of RogueLike:</span>

* Random level / dungeon creation
* Permadeath
* Turn-based
* Non-modal (Every action available regardless of location)
* Complete goals in multiple ways / emergent gameplay
* Stamina decay & strict ressource management
* Hack-N-Slash
* World exploration

https://en.wikipedia.org/wiki/Roguelike

### <span style="color:#C39BD3 ">Created with:</span>
* Robert Nystrom https://gameprogrammingpatterns.com/
* C# in Visual Studio https://en.wikipedia.org/wiki/C_Sharp_(programming_language)
* Raylib-cs https://github.com/ChrisDill/Raylib-cs
* MongoDB https://www.mongodb.com/
* Kenney Assets https://kenney.nl/assets/micro-roguelike
* 8-bit Portrait Pack https://itchabop.itch.io/8bit-portrait-pack


<details open><summary><span style="color:#E74C3C ">Game Design Document</span></summary>

### <span style="color:#E74C3C ">Description:</span>
* Small Roguelite, where the player controls an adventure in a dark & dangerous world,
the player needs to learn the attack pattern of the worlds creature, level up their character & collect magical items to survive the world.

### <span style="color:#E74C3C ">Controls:</span>
Movement - WASD or Arrow Keys</br>
Interaction - E</br>
Inventory Screen - I</br>
Character Screen - C</br>
Pause Menu - P</br>
Quick Slot Menu - F Keys</br>
Map - M</br>

### <span style="color:#E74C3C ">Unique Selling Points:</span>
* The enemies have a specific attack pattern, this introduces a opportunity for the player to grow their skill.</br>
* Survival elements, everything looses durability, the players character looses max stamina every turn, this forces the player to look for items to regain durability & max stamina, like wood/iron & food.</br>

### <span style="color:#E74C3C ">Core Mechanics:</span>
* Player will have the ability to choose to attack, block against a choosen entity or wait a turn, 
this will take a turn & then give the aggroed enemies their turn.</br>
* The players character will earn XP for killing enemies and/or completing quests, after X amount of XP the character will level up, giving the player X stat point to increase their characters stats making them stronger.</br>
* When the players characters health reached 0 or below it dies, this is permanent & will prompt the player to create a new character.</br>
* The player will after their character dies keep some part of the progress, this will make their next run a bit easier.</br>
* World will be created procedurally with small chunks handmade(Dungeons, Towns, Mountains, etc.).</br>
* Items will be randomly generated.</br>
* Everything decays, items, Max stamina on Player.</br>
* Player have a range around their character where they can tab between entities to choose which entity to interact with(Trade,Talk,Attack, etc.),
this range can be increased by increasing the characters perception stat</br>

### <span style="color:#E74C3C ">Weapon stats</span>
* Minimum Damage
* Maximum Damage
* Critical Chance
* Critical Damage
* Range
* Flat Penetration
* Percent Penetration

### <span style="color:#E74C3C ">Creature stats & skills</span>
### Stats:
* Strength - Used to modify damage done.
* Agility - Modifies chance to hit.
* Toughness - Determines health pool.
* Perception - Determines at what range the player can interaact with the world.
* Charisma - Modifies sell/buy price, chance to convert creature from hostile to non-hostile.
### Skills:
* Create bonfire - Used to cook food.
* Hide items - Can dig a hole in the world and hide choosen items, only if player have a shovel in inventory.

### <span style="color:#E74C3C ">Game Progression:</span>
* Player will explorer the world, where they will collect items, gold & experience, some they can choose to save for their next character when the current one dies.</br>
* Player will be in posseion of a journal where they will collect info on the creatures of the world, this will help them get better at surviving the fights.</br>
* Through the journal & time spend in the game should result in the player getting better at the game & more knowledgeable about the world & the games mechanincs.</br>

### <span style="color:#E74C3C ">Graphics:</span>
* Crisp 8 bit art pixel art.</br>
* Spritesheet from Kenney.nl, which we will extend https://kenney.nl/assets/micro-roguelike</br>
* Potraits package from itchabop https://itchabop.itch.io/8bit-portrait-pack</br>

### <span style="color:#E74C3C ">Audio:</span>
* Chiptune, probably made "in-house".

### <span style="color:#E74C3C ">Platform:</span>
* PC - Itch.io, Steam.

</details>

