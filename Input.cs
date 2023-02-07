using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NeonVectorShooter.Entities;

namespace NeonVectorShooter;

public static class Input
{
	#region InputStates

	private static KeyboardState KeyboardState
	{
		get => _keyboardState;
		set
		{
			_lastKeyboardState = KeyboardState;
			_keyboardState = value;
		}
	}

	private static KeyboardState _keyboardState, _lastKeyboardState;

	private static MouseState MouseState
	{
		get => _mouseState;
		set
		{
			_lastMouseState = MouseState;
			_mouseState = value;
		}
	}

	private static MouseState _mouseState, _lastMouseState;

	private static GamePadState GamepadState
	{
		get => _gamepadState;
		set
		{
			_lastGamepadState = GamepadState;
			_gamepadState = value;
		}
	}

	private static GamePadState _gamepadState, _lastGamepadState;

	#endregion

	private static bool isAimingWithMouse = false;
	public static Vector2 MousePosition => new Vector2(MouseState.X, MouseState.Y);

	public static void Update()
	{
		KeyboardState = Keyboard.GetState();
		MouseState = Mouse.GetState();
		GamepadState = GamePad.GetState(PlayerIndex.One);

		// If the player pressed one of the arrow keys or is using a gamepad to aim, we want to disable mouse aiming.
		// Otherwise, if the player moves the mouse, enable mouse aiming.
		if (new[] {Keys.Left, Keys.Right, Keys.Up, Keys.Down}
		    .Any(key => KeyboardState.IsKeyDown(key) || GamepadState.ThumbSticks.Right != Vector2.Zero))
		{
			isAimingWithMouse = false;
		}
		else if (MousePosition != new Vector2(_lastMouseState.X, _lastMouseState.Y))
		{
			isAimingWithMouse = true;
		}
	}

	public static bool WasKeyPressed(Keys key) =>
		_lastKeyboardState.IsKeyUp(key) && _keyboardState.IsKeyDown(key);

	public static bool WasButtonPressed(Buttons button) =>
		_lastGamepadState.IsButtonUp(button) && _gamepadState.IsButtonDown(button);

	public static Vector2 GetMovementDirection()
	{
		Vector2 direction = _gamepadState.ThumbSticks.Left;
		direction.Y *= -1; // invert the y-axis; thumbstick up is +Y, up on screen is -Y

		if (KeyboardState.IsKeyDown(Keys.A))
			direction.X -= 1;
		if (KeyboardState.IsKeyDown(Keys.D))
			direction.X += 1;
		if (KeyboardState.IsKeyDown(Keys.W))
			direction.Y -= 1;
		if (KeyboardState.IsKeyDown(Keys.S))
			direction.Y += 1;

		// Clamp the length of the vector to a maximum of 1.
		// NOTE: Performance optimization - LengthSquared avoids the sqrt operation in Length
		if (direction.LengthSquared() > 1)
			direction.Normalize();

		return direction;
	}

	public static Vector2 GetAimDirection()
	{
		if (isAimingWithMouse)
			return GetMouseAimDirection();

		Vector2 direction = GamepadState.ThumbSticks.Right;
		direction.Y *= -1;

		if (KeyboardState.IsKeyDown(Keys.Left))
			direction.X -= 1;
		if (KeyboardState.IsKeyDown(Keys.Right))
			direction.X += 1;
		if (KeyboardState.IsKeyDown(Keys.Up))
			direction.Y -= 1;
		if (KeyboardState.IsKeyDown(Keys.Down))
			direction.Y += 1;

		return Normalize(direction);
	}

	private static Vector2 GetMouseAimDirection()
	{
		// Vector2 direction = MousePosition - PlayerShip.Instance.Position;
		Vector2 direction = PlayerShip.Instance.Position.To(MousePosition);
		return Normalize(direction);
	}

	private static bool WasBombButtonPressed()
	{
		return WasButtonPressed(Buttons.LeftTrigger)
		       || WasButtonPressed(Buttons.RightTrigger)
		       || WasKeyPressed(Keys.Space);
	}

	private static Vector2 Normalize(Vector2 vec) =>
		// If there's no aim input, return zero.
		// Otherwise, normalize the direction to have a length of 1.
		vec == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(vec);
}
