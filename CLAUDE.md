# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Unity **6000.4.0f1** project, 3D, **URP**. As of writing, the repo contains no gameplay code yet — only Unity template assets (`Assets/Scenes/SampleScene.unity`, `Assets/InputSystem_Actions.inputactions`, `Assets/TutorialInfo/`) and the rule docs below. Architecture decisions are still open; when one is made, capture it here.

## Authoritative rule docs — read these first

Two docs in `Docs/` define how this codebase must be written. They take precedence over assumptions:

- **`Docs/Directives.md`** — project-wide rules (async, input, UI, animation, asset management, coding practices)
- **`Docs/CodingConventions.md`** — naming, formatting, file structure, serialization, type/member conventions

The most important constraints (so they aren't missed):

- **Async:** UniTask only. Every async method takes a `CancellationToken`; on `MonoBehaviour`s link it to `destroyCancellationToken`; all tokens link to `Application.exitCancellationToken`.
- **Input:** new Input System only. Use the **generated C# class** from `Assets/InputSystem_Actions.inputactions` — never reference the asset directly, never look up actions/bindings by string.
- **Assets:** no Addressables. Use the `Resources` folder, load async, configure via `ScriptableObject`s.
- **UI:** Canvas UI + TextMeshPro. Split canvases by update frequency to avoid rebuilds.
- **Animation:** `Animator` for state-driven; custom UniTask tweens for programmatic — no third-party tween libs.
- **Component access:** no `GetComponent` / `Find*` — use injection or direct serialized references. Never build hierarchy at runtime to support behavior; create it in edit mode.
- **Serialization:** no public fields. Use `[SerializeField] private` for editor-exposed fields, or `[field: SerializeField]` on a property when public access is needed.

## Working rules

- **Don't add packages or dependencies without asking first.** Current deps are pinned in `Packages/manifest.json`; UniTask comes via the OpenUPM scoped registry.
- **Don't add comments, XML docs, console logging, or custom inspectors** unless explicitly asked. Code should be self-explanatory.
- **Keep changes focused** — don't refactor or "improve" unrelated code.
- **When uncertain about an architectural decision, ask** rather than guess. If a well-established library solves the problem, propose it instead of reimplementing.
- **Update this file** (`CLAUDE.md`) on any significant change to code or architecture — `Docs/Directives.md` requires it.
- **Every change should be properly committed.** The working directory is *not* currently a git repo — if you're about to make changes, surface this so the user can `git init` (or confirm intent) before work piles up uncommitted.

## Build / run / test

Unity-only workflow — open the project in the Unity Editor (6000.4.0f1) to build, play, and run tests. Tests use `com.unity.test-framework` (Unity Test Runner under **Window → General → Test Runner**). No CLI build scripts exist; don't invent any.
