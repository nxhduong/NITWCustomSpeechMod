# NITWCustomSpeechMod
Simple BepInEx plug-in for *Night In The Woods*, a game by Infinite Fall. With this plug-in, Mae can say user-defined quotes when a pre-configured trigger key is pressed.
## Prerequisites
- Base game (plug-in tested on *Weird Autumn Edition*)
- `BepInEx` (tested on version `5.4.23.3`)
- `.NET SDK >= 8.0` if building from source.
## Installation and usage
- Build the plug-in from source with `dotnet build` (note that the source folder must be placed inside `<GAME_ROOT_DIRECTORY>/`), or download the pre-built one if available 
- Put this plug-in folder inside `<GAME_ROOT_DIRECTORY>\BepInEx\plugins`
- Run the game to test whether this plug-in is working correctly. The default key for displaying, advancing, and closing custom speech bubbles (trigger key) is `j`
- Quotes and trigger key for quotes can be configured by modifying respective entries in `<GAME_ROOT_DIRECTORY>\BepInEx\config\nxhd.nitwcustomspeechmod.cfg`. Individual quotes should be separated by `;`, and trigger key should be in `lowercase`
- Errors, if there are any, are logged in `<GAME_ROOT_DIRECTORY>\BepInEx\LogOutput.log`.
## Tools used during plugin development
- `BepInEx` with `UnityExplorer` plugin
- `dnSpy`
- `AssetStudio`
- `NITW Dialogue Tool`.