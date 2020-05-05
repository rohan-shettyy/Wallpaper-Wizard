# Wallpaper-Wizard

Wallpaper Wizard is a tool that allows you to change your wallpaper based on the weather and time of day.

## Installation

Just download and run the installer from the [itch.io](https://cyndakwil.itch.io/wallpaper-wizard) page.

## Importing a theme

1. Organize all your images into a folder.
2. In the same folder, add a `theme.json` file.
3. Add the appropriate values into the json file. The following is an example of a theme file with all required values filled.
```
{
    "Title": "Konbini",
    "Day": "Konbini_Day.jpg",
    "Night": "Konbini_Night.jpg",
    "Sunset": "Konbini_Sunset.jpg",
    "Rain": "Konbini_Rain.jpg",
    "Thunderstorm": "Konbini_Rain.jpg",
    "Clouds": "Konbini_Day.jpg",
    "Snow": "Konbini_Rain.jpg",
    "Fog": "Konbini_Day_Foggy.jpg"
}
```

Make sure all filenames and values are spelled correctly with the correct capitalization.
4. Send all files in the folder (not the folder itself) into a zip file.
5. Open Wallpaper Wizard and select "Import Theme".
6. Select your zip file in the file selector.
7. Select and apply your newly imported theme.
8. Profit.
