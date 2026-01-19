# ARTIFY Spatial Prototyping Framework

![ARTIFY 1 Page-1](https://github.com/user-attachments/assets/28f41aeb-914c-4853-b759-1be4427750f1)

**ARTIFY Spatial Prototyping Framework Workflow & Guide**
*Contents*
ARTIFY Spatial Prototyping Framework Workflow & Guide	
What is ARTIFY?	
How To Create and Use ARTIFY Prototype Diagrams in AR/VR	
How To Prepare ARTIFY Prototype Diagrams in Unity	
How to Add New Shape Assets to an ARTIFY Diagram	
How to Add New User Selectable Shapes to ARTIFY	

![model2](https://github.com/user-attachments/assets/9f98beb3-34cc-4b02-b8ee-9585ecfc3002)
![example layout](https://github.com/user-attachments/assets/1db5dc3b-13ed-463f-b069-4cdf4df3c688)


**What is ARTIFY?**
Augmented Reality for Trans-Disciplinary Design of ReconFigurable Manufacturing Systems (ARTIFY) is a spatial augmented reality prototyping framework that is designed to fill the gap between 1.) flexible, low fidelity paper prototypes and diagrams and 2.) high fidelity inflexible physical or virtual system models (see Fig. 1). 

  ![interactionOverviewDiagram](https://github.com/user-attachments/assets/e6d9362e-76a4-45ac-992b-838d0c46e95e)
  
_Fig. 1) Examples of low fidelity paper prototypes system models._

By filling this gap, ARTIFY aims to allow the creation of system models that are as easy to understand and edit by trans-disciplinary teams as paper prototypes but allow those teams to experience and interact with those prototypes as spatial models in real or virtual spaces. This makes the ARTIFY framework ideal in facilitating the creation of complex physical installations, such as manufacturing pipelines.
In simple terms, ARTIFY is a framework of six blocks that can be assembled into flowcharts. All blocks have a name, some have required input(s) and some produce output(s). By connecting and customising these blocks, systems can be modelled, from simple manufacturing pipelines (e.g., building a table, see Fig. 2.) to high complex procedures (e.g., semiconductor manufacturing, see Fig. 3.). Unlike traditional system diagrams drawn up on paper on screen-based software, however, ARTIFY displays these blocks as objects that can be displayed, actuated and edited in real spaces using augmented reality (see Fig. 4.).
 
  ![ARTIFYConcept](https://github.com/user-attachments/assets/61f750d5-d56f-445b-84a8-818df9968aa7)
  
_Fig. 2.) Six ARTIFY block types and their assembly into a simple manufacturing pipeline._
 
 ![semicon_case(1)](https://github.com/user-attachments/assets/ad500bb1-f0f9-46df-a38b-b39b5116c12e)
 
_Fig. 3.) ARTIFY blocks used to model complex semiconductor manufacturing._

![arty](https://github.com/user-attachments/assets/33785dc9-c988-49af-831e-b47c5ac2930f)![edit](https://github.com/user-attachments/assets/f9331bce-9796-4dae-a31f-ece4f34673d0)

_Fig. 4.) ARTIFY blocks set out in an office room. The name and type of the block is shown above each block. Blocks are connected by lines. A software keyboard that is used to edit the blocks’ names, inputs and outputs is shown. The colour of the titles above blocks indicates their state.
Green = The block is operating normally with its inputs fulfilled and its outputs produced.
Red = This block is not receiving its requiring input and therefore is not producing its output.
Grey = This block has been turned off and is not producing its output._

ARTIFY is driven entirely by text-matching logic, to make interaction simple and consistent across the system. Blocks are connected to other blocks by entered the name of one connecting block into the other block. Block titles required input and produced output are also all configured by entering semantic text.
If the required input text of one block matches the output text produced by a block it is connected to, this input requirement is fulfilled, and the block will show a green title bar. Blocks will only produce their output text when their input is fulfilled, otherwise they will show a red title bar. Thus, an ARTIFY diagram will effectively reflect its own state: if a key block is missing or disabled the subsequent blocks that rely on it will show a red title bar to indicate that the system’s dependencies are not fulfilled.
Using ARTIFY, teams can position customise blocks to change their name, required inputs and produced outputs, as well as their orientation, text, size, rotation and spatial position to produce an immersive and interactive 3D augmented reality model of a prototyped installation. Users can also change the visible shape of blocks, from abstract shapes like cubes and cylinders, to realistic and immersive 3D models (see Fig. 5.). 

 ![objects](https://github.com/user-attachments/assets/5766b159-27f1-4ba6-acf3-f23266f8de30)
 
_Fig. 5.) Examples of ARTIFY blocks featuring 3D immersive shapes._

**How To Create and Use ARTIFY Prototype Diagrams in AR/VR**  
ARTIFY requires the use of a Meta Quest Pro, Meta Quest 3 or a more recent model that allows for full colour HD Passthrough. When interacting in ARTIFY, users are required to use the Meta Quest controllers. Controllers can ray casters to select blocks and UI elements.
ARTIFY features two example scenes, a sample scene with a diagram of blocks representing soup manufacturing, and an empty scene with no blocks present. 
Once inside a scene, users can access its functions in the several ways, detailed below and shown in the following video (Fig. 8.):
1.	Move Blocks: Use the ray caster cursor and right index trigger to grab and move blocks.
2.	Actuate Blocks: Turn blocks on/off by clicking the power button on their title bar.
3.	Open Edit Menu: Open a block’s edit menu using the ray caster and trigger.
4.	Edit Block: With the edit menu open (Fig. 6.):
a.	Use controller sticks to rotate, resize, or move blocks.
b.	Edit block title, input, output, or connection by clicking an pencil button next to the associated text. Use the software keyboard to input text and save changes with the > button.
5.	Change Shape: Press the 'Change Shape' button, select a new shape, and close the menu with the X button.
6.	Get Help: Press the ? button for instructions. A controls banner is shown in the scene.
 
<img width="734" alt="menus" src="https://github.com/user-attachments/assets/c85b17e1-86b1-4f05-af71-2f77a5f65957">

_Fig. 6.) A processor block’s edit menu, help menu and shape change menu displayed._

7.	ARTIFY Menu: Press B on the right controller to open/close the ARTIFY Menu (Fig. 7).
8.	Add New Block: Click one of six block type buttons to spawn new blocks.
9.	Save Scene: Click Save All to save the current scene.
10.	Clear Scene: Click Clear All to remove all blocks from the scene.
11.	Load Scene: Click Load All to restore blocks from a saved scene.
 
![saveload](https://github.com/user-attachments/assets/b02bf639-c8a4-4a7d-8f28-f8655b0c664e)

_Fig. 7.) The ARTIFY Menu, showing buttons to spawn blocks, save blocks, clear all 
blocks in the scene or load the last saved scene._

<b>Click below to watch the video</b>
[![Watch the video](https://img.youtube.com/vi/_kE7s_dnbX4/maxresdefault.jpg)](https://youtu.be/_kE7s_dnbX4)
_Fig. 8.) Video displaying all the primary interactions in ARTIFY._

**How To Prepare ARTIFY Prototype Diagrams in Unity**
If desired, users can prepare a new ARTIFY scene ahead of time using Unity. The steps for this are as follows:
1.	Make a Base Scene: make a copy of the Blank Scene. 
2.	Add blocks: drag any desired blocks from the Prefabs/Blocks/ folder.
3.	Configure the blocks: Change block text in the Inspector to change their title, inputs, outputs and connecting blocks.
4.	Change block shape: If a different block shape is desired, 
a.	Drag a shape from the Prefabs/BlockShapes/ folder into the block I the Hierarchy.
b.	Delete the old object called ‘Shape’ and rename the new shape to ‘Shape’.
c.	Ensure that the Shape position is 0, 0, 0 and scale is 1, 1, 1.
d.	In the block Inspector drag the new shape in the Shape field under the UI Elements section.
5.	Change block size/position: Adjust the block size and position using the Transform component of the block in the Inspector.
 
**How to Add New Shape Assets to an ARTIFY Diagram**
To add a new Unity asset to ARTIFY to serve as a block shape, take the follow steps:
1.	Create a new Empty object with Position 0, 0, 0 and Scale 1, 1, 1.
2.	Name the object with the desired name of the new asset.
3.	Drag the project into Prefab/BlockShapes/ folder.
4.	Drag the desired Unity asset into new Prefab. 
5.	Ensure that the asset does not exceed 1, 1, 1 in scale and set Y position to 0.5.
6.	If the asset does not already have a Material, drag a Material onto it. 
7.	Ensure that the shader for this Material is EnvironmentalDepth/OcclusionLit.
This shape can now be added to blocks from within Unity using the steps described above. 
How to Add New User Selectable Shapes to ARTIFY
To allow users within ARTIFY to select this object, further steps must be taken to edit the Change Shape Menu for each block Prefab:

_Editing the Prefab_
1.	Open the Prefab.
2.	Navigate to Shape Change Menu/Panel/ChangePanel
3.	Make a duplicate copy of one of the existing buttons, e.g., ToSphereButton.
4.	Adjust the Transform position to move into an empty space in the menu.
5.	Rename the button to ‘To<NewShape>Button’.
6.	Navigate to the Text (TMP) component of the button and change the text to the new shape.

_Editing BaseBlock.cs_
8.	Open the BaseBlock.cs script inside Scripts/Blocks/ folder.
9.	Add a new GameObject reference under the // Shapes heading in the following way:
    public static GameObject <newShape>Prefab;
12.	Add a new lines to InitializeShapePrefabs as follows:

GameObject <newShape>Shape
<newShape>Prefab = <newShape>Shape;

10.	Add a new function to the script in the following format:
 
_Back to the Prefab_
11.	Navigate to the ISDK_RayInteractable component of the block Prefab. Scroll down to WhenSelect(PointerEvent) and Change the function the button triggers from <Block>.ToSphere to <Block>.To<NewShape>.
12.	Repeat these steps for all 7 block prefabs.
