# craftersmine.Valheim.WorldCleaner

Valheim WorldCleaner, mod which allows you to clean your world from dropped items in repeating interval

--------

This mod is still in development and should be used on your own risk!
I don't care if your character, items got deleted, world became corrupted, etc. if you using development version of mod.

Mod comes with configuration, in which you can set intervals of cleaning and chat messages that will be shown, whitelist items 
that should not be removed upon cleaning, few mod config options are currently not working.

Every config option has description, so configuring it is quite easy.

You can find item IDs for whitelist on [Valheim Wiki](https://valheim.fandom.com/wiki)

## Installation
* Download mod archive [here](https://github.com/craftersmine/Valheim.WorldCleaner/releases)
* Extract DLL (mod assembly) and PDB (debugging database) in `Valheim\BepInEx\plugins\`
* You're done!

P.S. Mod was built with [BepInEx](https://github.com/BepInEx/BepInEx/releases) version 5.4.20, but latest version of BepInEx used in Valheim is 5.4.19 from Thunderstore, 
even though it might work, I recommend to update your BepInEx to latest version, I've tested and it worked fine.

## Configuration
* Launch the game for the first time after mod installaton, close the game
* Open `Valheim\BepInEx\config\craftersmine.Valheim.WorldCleaner.cfg`
* Read descriptions and change values at your will
* Save the file

* Or use [BepInEx Configuration Manager](https://github.com/BepInEx/BepInEx.ConfigurationManager), as said above I don't care, if you break your game by changing mod values when it is running, it is not tested yet