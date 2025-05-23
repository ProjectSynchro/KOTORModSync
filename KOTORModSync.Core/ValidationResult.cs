﻿// Copyright 2021-2023 KOTORModSync
// Licensed under the GNU General Public License v3.0 (GPLv3).
// See LICENSE.txt file in the project root for full license information.

using System;
using JetBrains.Annotations;

namespace KOTORModSync.Core
{
	public class ValidationResult
	{
		public ValidationResult(
			[NotNull] ComponentValidation validator,
			[NotNull] Instruction instruction,
			[NotNull] string message,
			bool isError
		)
		{
			if ( validator == null )
				throw new ArgumentNullException(nameof( validator ));
			if ( string.IsNullOrWhiteSpace(message) )
				throw new ArgumentException(message: "Value cannot be null or whitespace.", nameof( message ));

			Component = validator.ComponentToValidate;
			Instruction = instruction ?? throw new ArgumentNullException(nameof( instruction ));
			InstructionIndex = Component.Instructions.IndexOf(instruction);
			Message = message;
			IsError = isError;

			string where = $"{Environment.NewLine}Name: {Component.Name}{Environment.NewLine}Instruction #{InstructionIndex + 1}: '{instruction.Action}'";
			if ( IsError )
			{
				Logger.LogError(where + Environment.NewLine + "Issue: " + message);
			}
			else
			{
				Logger.LogWarning(where);
				Logger.LogWarning(message);
			}
		}

		public int InstructionIndex { get; }
		public Instruction Instruction { get; }
		public Component Component { get; }
		public string Message { get; }
		public bool IsError { get; }
	}
}
