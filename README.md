# Trip Harmony Simulator
A simulation game built in Unity that models how characters with different personalities experience a shared trip. Each character’s happiness evolves over time based on individual preferences, emotional states, and travel conditions. Development prioritized scalability to support adding new characters and interactions easily.


## Overview
The simulator supports:
* Arbitrarily many characters
* Arbitrarily many travel locations
* Independent emotional and preference-driven state variables per character
* Dynamic happiness evaluation as conditions change


## Core Features
### Scalable Simulation Engine
* No hard-coded limits on characters or destinations
* Each character maintains an independent internal state
* Built to easily add new stats, mechanics, or behaviors

### Character Emotional States
Characters track dynamic stats such as:
* Happiness
* Energy
* Restroom Urge
* Hunger

These values evolve independently throughout the trip.

### Modular Happiness System
* Happiness is computed using a weighted preference model
* Each character values different factors differently


## Technologies Used
* Unity
* C#


## License
MIT License
