# SwingBound

A 3D grapple-and-momentum platformer built in Unity. You play a sphere-bot navigating floating levels with a tether gun — latch onto platforms, sling yourself across gaps, dash mid-air, dodge spikes, grab loot, and reach the chest at the end of the level.

Built with **Unity 6000.4.5f1** (Universal Render Pipeline) and playable in the browser via WebGL.

## Gameplay

- **Tether gun** — aim at a grapple platform and fire to pull yourself toward it. Aim assist gives you a latch radius so you don't have to hit dead center, and the tether auto-releases once you arrive.
- **Momentum movement** — camera-relative movement with a brake multiplier for crisp stops, a double jump, and an air dash that recharges when you land.
- **Hazards** — spikes cost health. You have 2 hit points and a short invincibility window after taking damage. Lose them all and the level restarts.
- **Loot & score** — pickups scattered through each level add to your score.
- **NPCs** — walk up to an NPC and press E for dialogue delivered with a typewriter effect.
- **Chest** — touching the chest ends the level and unlocks the next one.

## Controls

| Action | Input |
| --- | --- |
| Move | `WASD` / left stick |
| Jump / double jump | `Space` |
| Air dash | `Left Shift` (airborne only) |
| Fire tether | `Left Mouse Button` (hold) |
| Release tether | Release `LMB` or press `R` |
| Interact with NPC | `E` |
| Pause | `Esc` |

## Levels

| Scene | Purpose |
| --- | --- |
| `MainMenu.unity` | Title screen with animated menu camera |
| `Tutorial.unity` | Teaches movement and the tether gun |
| `Level1.unity` | First full level |
| `Level2.unity` | Second full level; loops back to Level 1 on completion |

## Running the game

### In the Unity Editor

1. Install **Unity 6000.4.5f1** (or the closest Unity 6 release) via Unity Hub.
2. Clone the repo:

```bash
git clone https://github.com/cha248rles/SwingBound.git
```

3. Open the project folder in Unity Hub and let it import — the first import takes a few minutes.
4. Open `Assets/SwingBound_Assets/Scenes/MainMenu.unity` and press Play.

### In a browser

A prebuilt WebGL export lives in `WebGL Builds/`. It needs to be served over HTTP rather than opened as a file:

```bash
cd "WebGL Builds" && python3 -m http.server 8000
```

Then visit `http://localhost:8000`.

## Project structure

```
Assets/
  SwingBound_Assets/
    Scenes/        Level1, Level2, MainMenu, Tutorial
    Scripts/       All gameplay C#
    Prefabs/       Player, platforms, loot, NPCs, traps
    Animation/     Chest, NPC, and menu camera animation
    Audio/ BGM/    SFX and music
    Material/ Textures/ Skyboxes/
    Objects/ Fruits/ RobotSphere/ A_piece_of_nature/ AurynSky/
  Quirky Series Ultimate/   Third-party art pack
  TextMesh Pro/
Packages/          Unity package manifest
ProjectSettings/   Unity project configuration
WebGL Builds/      Prebuilt browser export
```

## Scripts

| Script | Responsibility |
| --- | --- |
| `PlayerController.cs` | Camera-relative movement, jump, double jump, air dash |
| `TetherGun.cs` | Grapple raycast with aim assist, pull force, speed cap, line rendering |
| `TetherFollow.cs` | Keeps the tether visual anchored to its attach point |
| `PlayerHealth.cs` | Health, invincibility frames, death |
| `LevelManager.cs` | Score, win/lose state, scene progression |
| `LootBehavior.cs` | Collectible pickups and score values |
| `ChestBehavior.cs` | End-of-level trigger |
| `SpikeTrapDemo.cs` | Spike hazard behavior |
| `NPCBehavior.cs` | Proximity prompts and dialogue triggers |
| `TypewriterEffect.cs` | Character-by-character dialogue reveal |
| `CameraTarget.cs` | Third-person camera follow target |
| `CrosshairBehavior.cs` | Crosshair feedback for grapple targets |
| `MenuManager.cs` / `MenuCameraAnimation.cs` | Main menu UI and camera |
| `PauseMenuBehavior.cs` | Pause, resume, quit |
| `TextFollowCamera.cs` | Billboards world-space text toward the camera |

## Credits

Art packs: Quirky Series Ultimate, A Piece of Nature, AurynSky, Robot Sphere. Text rendering by TextMesh Pro.
