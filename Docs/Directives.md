# AI Directives

# About This Document

This document was created before any code was added to the repository. It provides guidelines for how to work with the project — for both human developers and AI agents.

# General

- The game is 3D and uses URP
- Always follow coding and naming conventions outlined in `Docs/Coding Conventions.md`
- On any significant or important change to the code or architecture, update the `Claude.md` file
- Avoid comments — code should be self-explanatory (with reasonable exceptions)
- Don't use XML documentation unless explicitly asked for
- Don't add new packages or dependencies without asking first
- When uncertain about an architectural decision, ask rather than guess
- If a well-established library already solves the problem at hand, propose using it instead of reimplementing the functionality from scratch
- Every change should be properly commited

# Async Code

- Use `UniTask` for all async work
- Async methods should accept cancellation tokens
- Async methods on MonoBehaviours should link the cancellation token to `destroyCancellationToken`
- All cancellation tokens in the project should be linked to `Application.exitCancellationToken`

# Disposal and Cleanup

- UniTask-based async operations should always be tied to a cancellation token to ensure they stop when their context is destroyed

# Input

- Use only the new Input System — no legacy Input Manager
- Use the generated C# input class — don't reference the Input Action Asset directly
- Don't look up actions or bindings by string — use the generated class's strongly-typed properties

# UI

- Use Canvas UI
- Use TextMeshPro for all text elements
- Use separate canvases for elements that update at different frequencies to avoid unnecessary canvas rebuilds

# Animation

- Use Animator components for state-driven animations (e.g., character animations, UI state transitions)
- For tweens and programmatic animations, write custom tween implementations using UniTask — don't use third-party tweening libraries

# Asset Management

- Don't use Addressables for asset management
- Use the `Resources` folder
- Asset loading should always be asynchronous
- Use ScriptableObjects for configuration data

# Coding Practices

- Keep changes focused — don't fix or "improve" unrelated code
- Use object pooling for frequently instantiated and destroyed objects (e.g., projectiles, VFX, UI elements)
- Don't use methods like `GetComponent`, `FindComponent`, etc.
    - Always prioritize injection or direct references
    - Use those methods only if there's no other option
- Never dynamically create hierarchy to facilitate behavior
    - Example: If a Player component requires an internal camera-follow object, it should be created during edit mode — not at runtime
- Don't create custom inspectors unless explicitly asked for
- Don't add console logging unless explicitly asked for or during an active debug session