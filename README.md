# Mini Vampire Survivor

A prototype game replicating the core mechanics of Vampire Survivor, with simplified and streamlined gameplay elements.

## Game Overview

In Mini Vampire Survivor, the player must survive waves of enemies for 5 minutes to win. The game features:
- **3 Enemy Types**: Normal, Fast, and Big
- **Player's Weapon**: Magic Wand
- **Collectible**: XP Gems (spawned after enemy death, used for leveling up)
- **Level Up**: Choose from 3 power-ups (Health, Damage, Fire Rate) each level

### Objectives
- **Win**: Survive for 5 minutes
- **Game Over**: Player's health drops below 0

## Programming Patterns Used

### Composition Over Inheritance
- **Reason**: Avoid long hierarchies, facilitate easy component reuse

### Observer Pattern
- **Purpose**: Decouple systems, centralized event handling
- **Implementation**: `EventManager` class (singleton)

### Finite State Machine (FSM) Pattern
- **Purpose**: Manage states of Player and Enemies
- **Examples**: `PlayerFSM` (Idle, Surviving, Died), `EnemyFSM` (Idle, Chase, Attack, Died)
- **Flexibility**: Add/remove behaviors without affecting existing ones

### Flyweight Pattern
- **Purpose**: Optimize memory usage by sharing data
- **Usage**: Store sensitive data in ScriptableObjects
  - **Editable Configurations**:
    - Player Config (max health, start level, move speed, primary weapon)
    - XP Level Config (levels data, power-ups, gems)
    - Spawn Config (wave details, enemy types)
    - Enemy Config (health, attack range, damage, move speed)

### Singleton Pattern
- **Purpose**: Ensure a single instance accessible globally
- **Examples**: `StatesManager` (game states like surviving time, enemy kill count)

### Mediator Pattern
- **Purpose**: Facilitate interaction between high-level controllers
- **Usage**: Mediator holds references to GameManager, Player, XPLevelUpManager, etc.

## Game Flow

1. **Game Start**: `GameManager` fires `OnGameStart` event
   - Activates systems: `PlayerFSM`, `SpawnManager`, `StatesManager`, `UIManager`
2. **Enemy Spawning**: `SpawnManager` spawns enemies per wave config and level
3. **Gameplay**:
   - Player moves, kills enemies, and collects XP Gems
   - Levels up and selects power-ups
4. **Game End**:
   - Survive for 5 minutes or health drops to 0
   - `GameManager` fires `GameComplete` event
   - Systems deactivate
   - Game Over UI displayed

## Installation and Setup

1. Clone the repository
2. Open the project in your preferred game development environment
3. Run the game from the main scene

## Contribution

Feel free to open issues and submit pull requests. Contributions are welcome!

## License

This project is licensed under the MIT License.

---

Enjoy playing Mini Vampire Survivor! If you have any questions or feedback, please reach out.
