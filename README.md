# Memory-Match 🎴

A simple, modular, and testable **card-matching game** built in **Unity 2021 LTS**.  
This project was developed as a prototype to demonstrate gameplay mechanics, clean architecture, and coding practices.

---

## 📌 Features
- Smooth **card flipping animations** with continuous flipping support.
- Multiple **board layouts** (2x2, 2x3, 5x6, etc.) with automatic scaling.
- **Scoring system** with optional combo support.
- **Save/Load system** to persist progress between sessions.
- **Sound effects** for flip, match, mismatch, and game over.
- Runs on **Desktop** and **Mobile (Android/iOS)**.
- Clean, modular code with **design patterns** (Observer, Factory, State).
- Easily **unit tested** using Unity Test Framework (NUnit).

---

## 🛠️ Project Structure

Assets/ ├── Scripts/ │   ├── Managers/ # GameManager, BoardManager, ScoreManager, AudioManager, SaveSystem │    ├── Cards/ # CardController │    ├── UI/ # UIManager │ └── Utilities/ # Helpers & extensions ├── Prefabs/ # Card prefab ├── Audio/ # Sound effects ├── Scenes/ # MainScene.unity


---

## 🚀 Getting Started

### Prerequisites
- Unity **2021 LTS** (or later).
- Git for version control.

### Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/<your-obscure-repo-name>.git
2.Open the project in Unity.
3.Run the MainScene to start the game.
