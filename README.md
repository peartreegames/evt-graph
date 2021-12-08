## EvtGraph
Code for the "Creating a Serialized Conditional Reaction Sequence Graph in Unity" [YouTube videos](https://www.youtube.com/watch?v=KJ_ba50nooQ)

Repository has been converted to a package module. The `video-end` branch contains the project at the time of the part 2 completion.

---
## Installation
Can be installed via the Package Manager > Add Package From Git URL...

This repo has a dependency on the EvtVariable package which *MUST* be installed first. (From my understanding Unity does not allow git urls to be used as dependencies of packages)
`https://github.com/peartreegames/evt-variables.git`

then the repo can be added

`https://github.com/peartreegames/evt-graph.git`

---
## Architectural Overview
![Architecture](./Documentation/Architecture.png)

---
## Todos
 - [ ] Zoom Persistence
 - [ ] Foldout Persistence
 - [ ] Rest of EvtTriggers
 - [ ] Standard set of Condition/Reactions
