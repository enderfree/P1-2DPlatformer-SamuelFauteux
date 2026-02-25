# P1-2DPlatformer-SamuelFauteux
Scripting 2 Platformer Assignment
By: Samuel Fauteux 2530005

# Controls
## Go Left
Keyboard: left key or a </br>
Controller: joystick left or d-pad left
## Go Right
Keyboard: right key or d </br>
Controller: joystick right or d-pad right
## Jump
Keyboard: up key or w or space key </br>
Controller: a/x (south button) or d-pad up
## Wall Jump
Go towards the wall to stick to it and then jump.

# Physics
I used RigidBody2D in order to be able to leverage unity's default physics. </br>
My ground detection is a box cast for it to detect my the corners. (more details in the code)

I implemented a coyote jump, and technically a jump buffer too, but I've been using the jump buffer as a flutter jump instead of giving it sensial values.
