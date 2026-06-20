namespace Cards.Engine;

/// <summary>
/// Represents the state of a single input button or key.
/// </summary>
public enum ButtonState
{
    Released,
    Pressed,
    Held
}

/// <summary>
/// Xbox controller button mappings supported by the input manager.
/// </summary>
public enum XboxButton
{
    A,
    B,
    X,
    Y,
    Start,
    Back,
    DPadUp,
    DPadDown,
    DPadLeft,
    DPadRight,
    LeftShoulder,
    RightShoulder
}

/// <summary>
/// Tracks per-frame input state for Xbox controller buttons.
/// Call <see cref="Update"/> once per frame before reading button states.
/// </summary>
public sealed class InputManager
{
    private readonly Dictionary<XboxButton, ButtonState> _current = new();
    private readonly Dictionary<XboxButton, ButtonState> _previous = new();

    /// <summary>
    /// Advances the input state. In a real Xbox SDK integration this method
    /// would poll the gamepad; here it provides the hookpoint for that logic.
    /// </summary>
    public void Update()
    {
        foreach (XboxButton btn in Enum.GetValues<XboxButton>())
        {
            _previous[btn] = _current.GetValueOrDefault(btn, ButtonState.Released);
        }
    }

    /// <summary>Simulates a button press (used by tests and the game layer).</summary>
    public void SetButtonState(XboxButton button, ButtonState state) =>
        _current[button] = state;

    /// <summary>Returns true during the single frame the button transitioned to Pressed.</summary>
    public bool IsJustPressed(XboxButton button) =>
        _current.GetValueOrDefault(button) == ButtonState.Pressed &&
        _previous.GetValueOrDefault(button) != ButtonState.Pressed;

    /// <summary>Returns true while the button is Pressed or Held.</summary>
    public bool IsDown(XboxButton button)
    {
        var state = _current.GetValueOrDefault(button);
        return state == ButtonState.Pressed || state == ButtonState.Held;
    }
}
