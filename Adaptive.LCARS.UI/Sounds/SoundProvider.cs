using System.Collections.ObjectModel;
using System.Media;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Adaptive.LCARS.UI
{
	/// <summary>
	/// Provides static methods, functions, and properties for playing LCARS and related sounds.
	/// </summary>
	public static class SoundProvider
	{
		#region Private Static Member Declarations
		private static Stream _accessing = Properties.Resources.AccessingLibraryComputerData;
		private static Stream _affirmative = Properties.Resources.Affirmative;
		private static Stream _button1 = Properties.Resources.Button1;
		private static Stream _button2 = Properties.Resources.Button2;
		private static Stream _button3 = Properties.Resources.Button3;
		private static Stream _button4 = Properties.Resources.Button4;
		private static Stream _button5 = Properties.Resources.Button5;
		private static Stream _defineParameters = Properties.Resources.DefineParametersProgram;
		private static Stream _errror1 = Properties.Resources.ErrorLevel1;
		private static Stream _exitApp = Properties.Resources.ExitApp;
		private static Stream _longButton = Properties.Resources.LongButton;
		private static Stream _mediumError = Properties.Resources.MediumError;
		private static Stream _miscButton = Properties.Resources.MiscButton;
		private static Stream _processing1 = Properties.Resources.Processing1;
		private static Stream _processing2 = Properties.Resources.Processing2;
		private static Stream _scrolling = Properties.Resources.Scroll;
		private static Stream _transfer = Properties.Resources.TransferComplete;
		private static Stream _unableToComply = Properties.Resources.UnableToComply;
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Plays the "Accessing Computer Library ..." sound.
		/// </summary>
		public static void PlayAccessingSound()
		{
			PlaySound(_accessing);
		}
		/// <summary>
		/// Plays the "Affirmative" sound.
		/// </summary>
		public static void PlayAffirmativeSound()
		{
			PlaySound(_affirmative);
		}
		/// <summary>
		/// Plays the button 1 sound.
		/// </summary>
		public static void PlayButton1()
		{
			PlaySound(_button1);
		}
		/// <summary>
		/// Plays the button 2 sound.
		/// </summary>
		public static void PlayButton2()
		{
			PlaySound(_button2);
		}
		/// <summary>
		/// Plays the button 3 sound.
		/// </summary>
		public static void PlayButton3()
		{
			PlaySound(_button3);
		}
		/// <summary>
		/// Plays the button 4 sound.
		/// </summary>
		public static void PlayButton4()
		{
			PlaySound(_button4);
		}
		/// <summary>
		/// Plays the button 5 sound.
		/// </summary>
		public static void PlayButton5()
		{
			PlaySound(_button5);
		}
		/// <summary>
		/// Plays the exit application sound.
		/// </summary>
		public static void PlayExit()
		{
			PlaySound(_exitApp);
		}
		/// <summary>
		/// Plays the scrolling sound.
		/// </summary>
		public static void PlayScroll()
		{
			PlaySound(_scrolling);
		}
		/// <summary>
		/// Plays the "define parameters..." sound.
		/// </summary>
		public static void PlayDefineParamters()
		{
			PlaySound(_defineParameters);
		}
		/// <summary>
		/// Plays the error 1 sound.
		/// </summary>
		public static void PlayError1()
		{
			PlaySound(_errror1);
		}
		/// <summary>
		/// Plays the long button press sound.
		/// </summary>
		public static void PlayLongButton()
		{
			PlaySound(_longButton);
		}
		/// <summary>
		/// Plays the medium error sound.
		/// </summary>
		public static void PlayMediumError()
		{
			PlaySound(_mediumError);
		}
		/// <summary>
		/// Plays the misc-button sound.
		/// </summary>
		public static void PlayMiscButton()
		{
			PlaySound(_miscButton);
		}
		/// <summary>
		/// Plays the processing 1 sound.
		/// </summary>
		public static void PlayProcessing1()
		{
			PlaySound(_processing1);
		}
		/// <summary>
		/// Plays the processing 2 sound.
		/// </summary>
		public static void PlayProcessing2()
		{
			PlaySound(_processing2);
		}
		/// <summary>
		/// Plays the "Transfer Complete" sound.
		/// </summary>
		public static void PlayTransferComplete()
		{
			PlaySound(_transfer);
		}
		/// <summary>
		/// Plays the "Unable To Comply" sound.
		/// </summary>
		public static void PlayUnableToComply()
		{
			PlaySoundSync(_unableToComply);
		}
		public static void Speak(string text)
		{
			SpeechSynthesizer synth = new SpeechSynthesizer();

			// Configure the audio output.
			synth.SetOutputToDefaultAudioDevice();
			synth.SelectVoice("Microsoft Zira Desktop");
			synth.Speak(text);
			synth.Dispose();
		}
		#endregion

		#region Private Static Methods		
		/// <summary>
		/// Attempts to play the WAV file in the specified stream.
		/// </summary>
		/// <param name="stream">
		/// The <see cref="Stream"/> containing the WAV file data.
		/// </param>
		private static void PlaySound(Stream stream)
		{
			if (stream != null)
			{
				// Be kind, rewind.
				stream.Seek(0, SeekOrigin.Begin);

				try
				{
					SoundPlayer player = new SoundPlayer(stream);
					player.Play();
					
				}
				catch
				{

				}
			}
		}
		/// <summary>
		/// Attempts to play the WAV file in the specified stream.
		/// </summary>
		/// <param name="stream">
		/// The <see cref="Stream"/> containing the WAV file data.
		/// </param>
		private static void PlaySoundSync(Stream stream)
		{
			if (stream != null)
			{
				// Be kind, rewind.
				stream.Seek(0, SeekOrigin.Begin);

				try
				{
					SoundPlayer player = new SoundPlayer(stream);
					player.PlaySync();
					player.Dispose();
				}
				catch
				{

				}
			}
		}
		#endregion

	}
}
