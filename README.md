Implemented ambient, diffuse, and specular components of lighting on objects in vertex and fragment shaders. Phong interpolation is used for shading and Blinn's model is used to compute specular intensities.

3D Scene:

Scene depicts a dungeon room. Walls, ceiling, and floor are made of stone. There are axes swinging back and forth from
the ceiling on one side of the room. In the center there is a pit of liquid "sludge" with a cube hovering over it.

-Cube floating above sludge is defined algorithmically

-Sludge is defined algorithmically and is animated over time using overlapping sin waves

-Swinging of axes is animated through script

Interaction:

-When using the static camera, user can click and drag on cube to rotate it via the rolling ball algorithm

-User can click on the roaming light source signified by the white sphere to toggle its movement on and off

Camera:

-Scene starts with a first person camera where user can look around with mouse and walk with WASD keys

-Left clicking on the cube in the center of the room changes the camera to a static view of the room

Illumination:

-All objects in scene are illuminated through my own shaders using ambient, diffuse, and specular components.
I also added distance attenuation to all light sources.

-Light signified by white sphere roams about the room. Can be toggled on/off with Q key

-Torch user carries also emits light. Can be toggled on/off with E key. The torch samples perlin noise to alter
the distance attenuation of the light randomly in order to create a flame-like effect on the light

Mapping:

-The ceiling, walls, and floor of the dungeon have a stone texture mapped to them

-The torch and the handle of the axes have a wood texture mapped to them

-The axe heads have two different metal textures mapped to them

Help Screen:

-Toggled with H key


REFERENCES

Axe Model: https://rafilly.itch.io/low-poly-double-axe
Textures: https://ambientcg.com