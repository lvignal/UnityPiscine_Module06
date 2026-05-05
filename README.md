# John Lemon in Nightmareland

This is the last module of 42 school Unity Piscine : 7 modules made to learn Unity fundamentals.  

## Game Overview
- 3D escape game. You are John Lemon waking up in a scary mansion, haunted by ghosts and gargoyles
- Objective: find 3 keys needed to open a bedroom door where there is the exit wardrobe
- If a ghost catches you, you die and level restarts 

### Controls
- Movement: WASD or arrows
- Turn camera: AD or left/right arrows in TPS view / mouse in FPS view
- Switch camera between TPS and FPS view: C

### Enemies
- Ghost: if the player enters its detection zone, it starts chasing him for 15s
- Gargoyle: if the player is detected by its red torchlight, all ghosts are alerted and move toward him

---

## Technical Details
### Unity Features Used
- NavMesh, NavMeshAgent
- Cinemachine virtual cameras
- Lighting and post-processing
- Animator
- Audio system
- Trigger detection

### What I learned
- Designing a large 3D environment (organized scene tree, reusable prefabs)
- Implementing a NavMesh and setting agents destinations
- Managing 2 cameras using Cinemachine
- Creating atmosphere by setting lighting and post-processing

## Preview
### Scene view
<img width="544" height="473" alt="image" src="https://github.com/user-attachments/assets/ed244859-24f1-4512-9d9f-f8b29378d53d" />

### In game view
<img width="799" height="451" alt="image" src="https://github.com/user-attachments/assets/4c07013d-f3e7-44aa-8e8b-32f35aae1183" />  

<img width="1590" height="895" alt="image" src="https://github.com/user-attachments/assets/e8faca5a-b54c-4504-b7ed-7264ff3cc014" />





