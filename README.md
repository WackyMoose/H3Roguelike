# H3Roguelike
### <span style="color:#C39BD3 ">Scrum roles:</span>

Product Owner: Michael.</br>
Developer: Victor.</br>
Scrum Master: Pierre.

### <span style="color:#C39BD3 ">End Product:</span>

Small procedural generated Rogue-lite game.

### <span style="color:#C39BD3 ">Created with:</span>
* Robert Nystrom https://gameprogrammingpatterns.com/
* C# in Visual Studio https://en.wikipedia.org/wiki/C_Sharp_(programming_language)
* Raylib-cs https://github.com/ChrisDill/Raylib-cs
* MongoDB https://www.mongodb.com/
* Kenney Assets https://kenney.nl/assets/micro-roguelike
* 8-bit Portrait Pack https://itchabop.itch.io/8bit-portrait-pack


<details open><summary><span style="color:#E74C3C ">Game Design Document</span></summary>

### <span style="color:#E74C3C ">Description:</span>
Small Roguelite, where the player controls an adventure in a dark and dangerous world,
the player needs to learn the attack pattern of the worlds creature, level up their character & collect magical items to survive the world.

### <span style="color:#E74C3C ">Controls:</span>
Movement - WASD or Arrow Keys</br>
Interaction Menu - Space</br>
Inventory Screen - I</br>
Character Screen - C</br>
Pause Menu - P</br>

### <span style="color:#E74C3C ">Unique Selling Points:</span>
The enemies have a specific attack pattern, this introduces a opportunity for the player to grow their skill.</br>

### <span style="color:#E74C3C ">Core Mechanics:</span>
Player will have the ability to choose to attack, block against a choosen entity or wait a turn, 
this will take a turn and then give the aggroed enemies their turn.</br>
The players character will earn XP for killing enemies and/or completing quests, after X amount of XP the character will level up, giving the player X stat point to increase their characters stats making them stronger.</br>
When the players characters health reached 0 or below it dies, this is permanent and will prompt the player to create a new character.</br>
The player will after their character dies keep some part of the progress, this will make their next run a bit easier.</br>
World will be created procedurally with small chunks handmade(Dungeons, Towns, Mountains, etc.).</br>
Items will be randomly generated.</br>
Everything decays, items, Max stamina on Player.</br>
Player have a range around their character where they can tab between entities to choose which entity to interact with(Trade,Talk,Attack, etc.),
this range can be increased by increasing the characters perception stat</br>

### <span style="color:#E74C3C ">Game Progression:</span>


### <span style="color:#E74C3C ">Graphics:</span>
Crisp old school 8 bit art.</br>
Sprites from Kenney.nl and itch.io.

### <span style="color:#E74C3C ">Audio:</span>


### <span style="color:#E74C3C ">Platform:</span>
PC - Itch.io, Steam.

</details>


### <span style="color:#E74C3C ">Definition of RogueLike:</span>

* Random level / dungeon creation
* Permadeath
* Turn-based
* Non-modal (Every action available regardless of location)
* Complete goals in multiple ways / emergent gameplay
* Stamina decay and strict ressource management
* Hack-and-Slash
* World exploration

https://en.wikipedia.org/wiki/Roguelike

