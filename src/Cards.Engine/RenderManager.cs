namespace Cards.Engine;

/// <summary>
/// Represents a single 2-D position on screen (in pixels).
/// </summary>
public readonly struct Position
{
    public float X { get; }
    public float Y { get; }

    public Position(float x, float y) { X = x; Y = y; }

    public override string ToString() => $"({X}, {Y})";
}

/// <summary>
/// Manages rendering of game objects to the screen.
/// Acts as the abstraction layer over the underlying graphics API
/// (DirectX / XAML / MonoGame SpriteBatch, etc.).
/// </summary>
public sealed class RenderManager
{
    private readonly List<string> _drawLog = new();

    public int Width { get; }
    public int Height { get; }

    public RenderManager(int width = 1920, int height = 1080)
    {
        Width = width;
        Height = height;
    }

    /// <summary>Clears the back-buffer at the start of each frame.</summary>
    public void BeginFrame() => _drawLog.Clear();

    /// <summary>
    /// Queues a sprite draw call. Replace the body with real GPU calls when
    /// integrating a graphics API.
    /// </summary>
    public void DrawSprite(string spriteName, Position position) =>
        _drawLog.Add($"Sprite '{spriteName}' at {position}");

    /// <summary>Queues a text draw call.</summary>
    public void DrawText(string text, Position position) =>
        _drawLog.Add($"Text '{text}' at {position}");

    /// <summary>Presents the back-buffer. In a real implementation this calls Present() or equivalent.</summary>
    public void EndFrame() { /* swap buffers */ }

    /// <summary>Returns the draw calls queued this frame (useful for testing).</summary>
    public IReadOnlyList<string> GetFrameDrawLog() => _drawLog.AsReadOnly();
}
