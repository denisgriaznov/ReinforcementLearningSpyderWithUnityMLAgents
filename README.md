## Create and train custom locomotion robot with ML Agents and Gym


This spider was created in Unity using [ArticulationBody](https://docs.unity3d.com/Manual/class-ArticulationBody.html) physics, which creates realistic interactions. 
Despite its cartoonish appearance, the spider's gait is not controlled by animation or pre-written code, but by a neural network that was trained with [Gym](https://gymnasium.farama.org/index.html) using reinforcement learning algorithms.


<img width="400" height="300" src="media/result.gif">


## Enviroment

The environment is a spider robot with four legs - three motors (rotating joint) on each. In this configuration, the middle joint of each leg is fictitious (the rotation limit is set to 0 degrees) for visual demonstration and faster training. But it is possible to change the restrictions for all joints.

The spider's goal is to move forward as quickly as possible.

### Observation space:

36 values - angle velocity and position for each 12 joints as well as 6 coordinates and 6 velocities (position and Euler angle) for robot head.

### Action space:

12 values - discrete direction of movement for each rotating joint: -1 for moving backward, 0 for standing still and 1 for moving forward.

### Reward function:

If the speed in the forward direction speed_forward is greater than 0.1, then **Rforward = speed_forward**, otherwise **Rforward = speed_forward - 0.1**
If the magnitude of the lateral velocity is greater than 2, **Rside = -0.5**
If the robot's head is in contact with the floor, **Rfail = -4** (and also the episode ends)
Sum reward:
**R = Rforward + Rside + Rfail**






