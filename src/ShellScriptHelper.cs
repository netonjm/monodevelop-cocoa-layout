using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoDevelop.Debug.CocoaLayout
{
	public static class ShellScript
	{
		#region Public helpers

		public static bool GetDefaultBooleanKey (string domain, string key)
		{
			return GetDefaultKeyCommand (domain, key).ExecuteBash (ignoreError: true) == "YES";
		}

		public static void SetDefaultBooleanKey (string domain, string key, bool value)
		{
			GetSetDefaultBooleanKeyCommand (domain, key, value).ExecuteBash (ignoreError: true);
		}

		static string GetDefaultKeyCommand (string domain, string key)
		{
			return $"defaults read {domain} {key}";
		}

		static string GetSetDefaultBooleanKeyCommand (string domain, string key, bool value)
		{
			var actualValue = value ? "YES" : "NO";
			return $"defaults write {domain} {key} {actualValue}";
		}

		static string ExecuteBash (this string sender, string workingDirectory = null, bool ignoreError = false, bool returnsPid = false,
		                                  bool handleOutput = true, Dictionary<string, string> environmentVariables = null)
		{
			string wait = !handleOutput ? " >> /dev/null 2>&1 &" : "";
			return "/bin/bash".ExecuteCommand ($"-c \"{sender}{wait}\"", ignoreError, workingDirectory, environmentVariables, returnsPid);
		}

		static string ExecuteCommand (this string sender, string arguments = "", bool ignoreError = true, string workingDirectory = null, Dictionary<string, string> environmentVariables = null, bool returnsPid = false)
		{
			string output, error;
			ShellScript.ExecuteCommand (sender, out output, out error, arguments, workingDirectory, environmentVariables);
			if (!ignoreError && !string.IsNullOrEmpty (error)) {
				throw new ArgumentException (error);
			}
			return output;
		}

		static void ExecuteCommand (string fileName, out string output, out string error, string arguments = "", string workingDirectory = null,
										   Dictionary<string, string> environmentVariables = null, bool returnsPid = false)
		{
			using (var process = new Process ()) {
				process.StartInfo.FileName = fileName;
				process.StartInfo.Arguments = arguments; // Note the /c command (*)
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;

				if (workingDirectory != null)
					process.StartInfo.WorkingDirectory = workingDirectory;

				if (environmentVariables != null) {
					foreach (var item in environmentVariables) {
						process.StartInfo.EnvironmentVariables [item.Key] = item.Value;
					}
				}

				process.Start ();
				//* Read the output (or the error)
				if (returnsPid) {
					output = process.Id.ToString ();
				} else {
					output = process.StandardOutput.ReadToEnd ().Trim ();
				}

				error = process.StandardError.ReadToEnd ().Trim ();
				process.WaitForExit ();
			};
#if DEBUG
			Console.WriteLine ($"{fileName} {arguments}");
			Console.WriteLine ($"{output}");
			if (!string.IsNullOrEmpty (error))
				Console.WriteLine ($"ERROR: {output}");
#endif
		}

		#endregion
	
	}
}
