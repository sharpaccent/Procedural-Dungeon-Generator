# Procedural-Dungeon-Generator

The following is a procedural dungeon generator developed for my tutorial series which you can find here: https://sharpaccent.com/?c=course&id=25 

It uses the Delanauy triangulation library from here: https://github.com/adamgit/Unity-delaunay

You can see the process here

![Process](https://media.giphy.com/media/3oxQNsKO18EWdfnbIk/giphy.gif)

The main concept for it is:
1. Create cells and distribute them randomly. 
2. Separate those cells to make sure there's no overlaping cells
3. Filter the rooms based on size and then triangulate between the ones we keep
4. Create a spanning tree between them to make sure we can reach all cells in our generator
5. Strict the spanning tree into horizontal and vertical lines to make paths.
6. Visualize the results
7. Profit

The concept for this was inspired by:
https://www.gamasutra.com/blogs/AAdonaac/20150903/252889/Procedural_Dungeon_Generation_Algorithm.php
https://www.reddit.com/r/gamedev/comments/1dlwc4/procedural_dungeon_generation_algorithm_explained/

# How to use

The project will rebuild libraries the first time you open it, this should take less than a minute.
Afterwards open scene1 and hit play. It will generate a dungeon. You can enable debug mode from the generator asset, if you enable the debugview component you can take a look on the steps. Watch the videos on my site to see how to customize further and/or create a game out of it. 

# License
CC-BY-4.0
If you use this in your project provide proper attribution to:

Athos Kele www.sharpaccent.com 

The git for the Delanauy triangulation (that one is under MIT license but be good sports and credit it regardless of that)
https://github.com/adamgit/Unity-delaunay


# Patreon
If you like more timer saver scripts and game development tutorials, consider supporting my cause on Patreon https://www.patreon.com/csharpaccent?

# Follow me at
https://sharpaccent.com/ 
https://twitter.com/AccentTutorials

