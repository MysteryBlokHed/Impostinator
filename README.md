# Impostinator

An external hack for Among Us written in C#.

## What it can do

This hack is currently able to change the following game settings
past their limits:

- Emergency Meeting Count
- Emergency Cooldown
- Discussion Time
- Voting Time
- Player Speed
- Crewmate Vision
- Impostor Vision
- Kill Cooldown
- Common Task Count
- Long Task Count
- Short Task Count

## Use

Impostinator has a very simple UI. Just open the dropdown to view a
list of settings it can modify, change the value with the box on the
right, and then press the "Change Setting" button at the bottom.

### Updating settings for other players

If you change a setting with the program while there are other players
in the lobby, the change will not take effect for them. To update the
settings for other players, just update the settings with the program,
then change an unrelated setting from the menu in-game. This will cause
an update to the settings for all connected players. 

## Building

To build the project yourself, you need
[Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
To build it, open the `.sln` file in the src/ directory, then run
Build > Build Impostinator from the menu bar. Make sure that you are
building for x86 and not x64.

## License

This software uses the
[GNU General Public License v3](https://choosealicense.com/licenses/gpl-3.0/).
