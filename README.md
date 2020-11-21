# Impostinator

An external hack for Among Us written in C#.

## What it can do

This hack is currently able to change the following game settings
to any value:

- Confirm Ejects (On/Off)
- Emergency Meeting Count
- Anonymous Votes (On/Off)
- Emergency Cooldown
- Discussion Time
- Voting Time
- Player Speed
- Crewmate Vision
- Impostor Vision
- Kill Cooldown
- Kill Distance (Short/Medium/Long)
- Taskbar Updates (Always/Meetings/Never)
- Visual Tasks (On/Off)
- Common Task Count
- Long Task Count
- Short Task Count

## Use

Impostinator has a very simple UI. Just open the dropdown to view a
list of settings it can modify, change the value with the box on the
right, and then press the "Change Setting" button at the bottom.

## Offsets

Impostinator gets its pointer offsets from an `offsets.json` file,
which is located in the src directory. When running Impostinator.exe,
**the offsets file must be moved to the same directory as Impostinator.exe.**
The program won't look for the file in other directories.

## Updating settings for other players

If you change a setting with the program while there are other players
in the lobby, the change will not take effect for them. To update the
settings for other players, just update the settings with the program,
then change an unrelated setting from the menu in-game. This will cause
an update to the settings for all connected players.

## Building

To build the project yourself, you need
[Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
To build it, open the `.sln` file in the src/ directory, then run
Build > Build Impostinator from the menu bar or press `Ctrl + B`.

## License

This software is licensed under the
[GNU General Public License v3](https://choosealicense.com/licenses/gpl-3.0/).
