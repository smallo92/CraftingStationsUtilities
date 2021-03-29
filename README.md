# Crafting Station Utilities
This mod gives you a bunch of config option related to Crafting Stations (Workbench, Forge, Stonecutter, Artisan Table and sometimes the Cauldron)

##### Note:
If you are using my [CraftingStationsLevelBasedBuildRange](https://valheim.thunderstore.io/package/Smallo/CraftingStationsLevelBasedBuildRange/) mod then please make sure the `Extended Crafting Station Ranges` config option is set to false

## Manual Installation
To install this mod, you need to have BepInEx. After installing BepInEx, extract CraftingStationUtilities.dll into games install **"\Valheim\BepInEx\plugins"**

## Config
Before the config file is generated, you must run the game with the mod installed. The config file is located at **"\Valheim\BepInEx\config\smallo.mods.craftingstationutilities.cfg"**

#### There are serveral config options available;

## Main Toggles;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Enable Mod | bool | true | Enable or disable the mod |
| Show Bench Level On Hover | bool | true | Show the bench level on hover |
| Hide Hover Level On Child Stations | bool | true | Hides the level hover on Artisan Stations, Stonecutters and Cauldrons since they can't go past level 1 (Left this as an option incase these stations get levels in the future) |
| Require Crafting Station Roof | bool | true | Whether or not to require Crafting Stations to have roofs |
| Extended Crafting Station Ranges | bool | false | Define custom build ranges for crafting stations |
| Custom Use Ranges | bool | false | Define custom use ranges for crafting stations |
| Custom Extension Ranges | bool | false | Define custom extension ranges for connecting to crafting stations |
| Allow No Fire For Cauldron | bool | false | Allows a Cauldron to work with no fire |

## Extended Crafting Station Ranges;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Workbench | double | 20.0 | Workbench build range |
| Forge | double | 20.0 | Forge build range |
| Stonecutter | double | 20.0 | Stonecutter build range |
| Artisan Table | double | 20.0 | Artisan Table build range |

## Custom Use Ranges;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Workbench | double | 2.0 | Workbench use range |
| Forge | double | 1.7 | Forge use range |
| Stonecutter | double | 2.0 | Stonecutter use range |
| Artisan Table | double | 2.0 | Artisan Table use range |
| Cauldron | double | 1.9 | Cauldron use range |

## Custom Extension Ranges;
| Config Option | Type | Default Value | Description |
|:-------------:|:-----------:|:-----------:|:-----------|
| Workbench Chopping Block | double | 5.0 | Chopping Block connection range |
| Workbench Tanning Rack | double | 5.0 | Tanning Rack connection range |
| Workbench Adze | double | 5.0 | Adze connection range |
| Workbench Tool Shelf | double | 5.0 | Tool Shelf connection range |
| Forge Bellows | double | 2.0 | Forge Bellows connection range |
| Forge Anvil | double | 5.0 | Anvil connection range |
| Forge Grinding Wheel | double | 5.0 | Grinding Wheel connection range |
| Forge Smiths Anvil | double | 5.0 | Smiths Anvil connection range |
| Forge Cooler | double | 5.0 | Cooler connection range |
| Forge Toolrack | double | 5.0 | Toolrack connection range |

If you have any suggestions, feel free to let me know!

## Changelog

#### v1.0;
* Initial Upload