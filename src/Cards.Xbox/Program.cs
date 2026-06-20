using Cards.Engine;
using Cards.Xbox;

// Entry point for "The Ultimate Card Game Collection" on Xbox.
// In a real Xbox build this would be the UWP or GDK app entry point.

var input = new InputManager();
var renderer = new RenderManager();
var game = new CardCollectionGame(playerCount: 2, input, renderer);
var loop = new GameLoop(game);

Console.WriteLine($"Starting {game.Name}...");

// For a headless demo, run a single initialization cycle then exit.
game.Initialize();
game.Deal();
game.Update();

Console.WriteLine("Architecture initialized successfully.");
