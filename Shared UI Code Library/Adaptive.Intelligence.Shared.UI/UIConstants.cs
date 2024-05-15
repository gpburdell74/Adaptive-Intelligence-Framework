﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adaptive.Intelligence.Shared.UI
{
	/// <summary>
	/// Provides the constants definitions for UI controls, dialogs, and other operations.
	/// </summary>
	public static class UIConstants
	{
		#region Static Member Variables
		/// <summary>
		/// The current process name.
		/// </summary>
		private static readonly string? _processName;
		#endregion

		#region Static Constructor
		/// <summary>
		/// Initializes the <see cref="UIConstants"/> class.
		/// </summary>
		static UIConstants()
		{
			// Get the process name so we know if we are in design mode.
			if (string.IsNullOrEmpty(_processName))
				_processName = _processName ?? System.Diagnostics.Process.GetCurrentProcess().ProcessName;
		}
		#endregion

		#region Constants

		#region Font Constants		
		/// <summary>
		/// The standard font name.
		/// </summary>
		public const string StandardFontName = "Segoe UI";
		/// <summary>
		/// The standard font size.
		/// </summary>
		public const float StandardFontSize = 9.75f;
		#endregion

		#region Process Name Constants
		/// <summary>
		/// Development environment process name.
		/// </summary>
		public const string DevProcessNameWithExtension = "devenv.exe";
		/// <summary>
		/// Development environment process name.
		/// </summary>
		public const string DevProcessName = "devenv";
		#endregion

		#region Logging Constants		
		/// <summary>
		/// The is-handle-created log text.
		/// </summary>
		private const string HandleCreatedText = @"IsHandleCreated: ";
		/// <summary>
		/// The method divider text.
		/// </summary>
		private const string MethodDivider = "::";
		/// <summary>
		/// The method parenthesis text.
		/// </summary>
		private const string MethodParens = "();";
		#endregion

		#endregion

		#region Public Static Methods / Functions		
		/// <summary>
		/// Creates the standard font.
		/// </summary>
		/// <returns>
		/// A <see cref="Font"/> instance using the default font name and size.
		/// </returns>
		public static Font CreateStandardFont()
		{
			return new Font(StandardFontName, StandardFontSize);
		}
		/// <summary>
		/// Determines if the application is executing in Visual Studio design mode.
		/// </summary>
		/// <remarks>
		/// This is referenced by the <see cref="AdaptiveControlBase"/> and <see cref="AdaptiveDialogBase"/>
		/// classes.
		/// </remarks>
		/// <returns>
		/// <b>true</b> if the current executing process is the Visual Studio IDE; otherwise,
		/// returns <b>false</b>.
		/// </returns>
		public static bool InDesignMode()
		{
			return ((_processName == DevProcessName) ||
			 (_processName == Path.GetFileNameWithoutExtension(DevProcessName)));
		}
		/// <summary>
		/// Gets the log text for whether or not hte UI handle is created for the calling control or dialog.
		/// </summary>
		/// <param name="isHandleCreated">
		/// <b>true</b> if the calling control or dialog has a handle created by Windows; otherwise, <b>false</b>.
		/// </param>
		/// <returns>
		/// A string containing the text to be logged.
		/// </returns>
		public static string IsHandleCreatedText(bool isHandleCreated)
		{
			return HandleCreatedText + isHandleCreated.ToString();
		}
		/// <summary>
		/// Gets the log text for when a method is invoked.
		/// </summary>
		/// <param name="typeName">
		/// A string containing hte type or instance name for the calling object.
		/// </param>
		/// <param name="methodName">
		/// A string containing the method name.
		/// </param>
		/// <returns>
		/// A string containing the text to be logged.
		/// </returns>
		public static string MethodStartText(string? typeName, string? methodName)
		{
			string text = string.Empty;

			if (!(string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(methodName)))
			{
				if (string.IsNullOrEmpty(typeName))
					text = MethodDivider + methodName + MethodParens;
				else if (string.IsNullOrEmpty(methodName))
					text = typeName + MethodDivider + MethodParens;
				else
					text = typeName + MethodDivider + methodName + MethodParens;
			}
			return text;
		}
		#endregion
	}
}
