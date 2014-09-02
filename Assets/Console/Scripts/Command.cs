using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Wenzil.Console {
	public struct CommandInfo {
		public string description { get; private set; }
		public string usage { get; private set; }
		public ConsoleCommand callback { get; private set; }

		public CommandInfo(string description, string usage, ConsoleCommand callback) {
			this.description = (string.IsNullOrEmpty(description.Trim()) ? "NO DESCRIPTION PROVIDED" : description);
			this.usage = (string.IsNullOrEmpty(usage.Trim()) ? "NO USAGE STRING PROVIDED" : usage);
			this.callback = callback;
		}
	}
}