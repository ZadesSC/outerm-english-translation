# Outerm English Translation

English translation patch for `[Aya] Outerm Race` on RimWorld 1.6.

This repository contains:

- English `DefInjected` translation files for the Outerm mod.
- A small companion DLL that fixes several strings that are hardcoded or not exposed cleanly through the original translation files.
- A custom preview image for the translation patch.

## Why This Exists

The translation work in this mod was produced with the help of LLM GPT-5.4.

I made this patch because the Translation mod by WRelick is not updated yet:

- https://steamcommunity.com/sharedfiles/filedetails/?id=2839446088

I also needed to understand what the Outerm mod was doing while working on the Combat Extended patch in my Ayameduki CE addon:

- https://steamcommunity.com/sharedfiles/filedetails/?id=3105126426&searchtext=ayameduki

## Dependency

This mod requires the original workshop mod:

- `[Aya] Outerm Race`
- Steam Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=3675887482

## What This Mod Translates

- Race and xenotype text
- Faction and incident text
- Apparel, weapons, items, and research
- Hediffs, genes, hairs, body defs, backstories, and related content
- Selected hardcoded UI text from the original DLL

## DLL Fixes Included

The bundled translation DLL currently patches:

- The growing-weapon message so it displays in English
- Outerm skill gizmo cooldown text
- Outerm skill gizmo unlock requirement text

## Installation

1. Subscribe to or install `[Aya] Outerm Race`.
2. Place this mod after the original Outerm mod.
3. Enable both mods in RimWorld.

The mod metadata already declares the dependency and loads after the original mod.

## Notes

- Supported version: RimWorld 1.6
- Package ID: `zades.outermenglishtranslation`
- This is a fan translation patch and is not the original Outerm mod
