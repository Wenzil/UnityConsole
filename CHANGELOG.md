# 0.1.0

### Migrating from a previous version
0.1.0 modifies the Console prefab. If you want to upgrade an existing project, make sure you use the updated prefab.

## Changes
- Fix incompatibility with Unity 5 and drop Unity 4.6 support
- Fix console preventing user to select any other input field
- Fix scrolling not being enabled by default and weird scrolling behavior
- Fix the input field sometimes losing focus randomly
- Fix the console output text overflowing its rectangle when viewed from behind in World Space render mode
- Fix changing Time.timeScale messing up input field reactivation when submitting a command or reopening the console.
- Fix a strange bug when Unity rebuilds the project while in play mode
- Make input history capacity a constant rather than a inspector-editable value (helps fix the previous bug)
- Add some helpful text to each sample scene
- Overhaul cursor locking in the world space sample scene
