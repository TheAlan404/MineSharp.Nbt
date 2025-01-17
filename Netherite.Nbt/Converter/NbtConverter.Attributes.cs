﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netherite.Nbt
{
	[AttributeUsage(AttributeTargets.All)]
	public sealed class NbtIgnoreAttribute : Attribute
	{
		public NbtIgnoreAttribute() { }
	}

	[AttributeUsage(AttributeTargets.All)]
	public sealed class NbtPropertyAttribute : Attribute
	{
		public string? TagName;

		public NbtPropertyAttribute(string? tagName = null)
		{
			TagName = tagName;
		}
	}

	public enum NbtMemberSerialization
	{
		OptIn,
		OptOut,
	}

	[AttributeUsage(AttributeTargets.Class)]
	public sealed class NbtDocumentAttribute : Attribute
	{
		public NbtMemberSerialization Serialization = NbtMemberSerialization.OptOut;
		public bool StrictCase = false;

		public NbtDocumentAttribute(NbtMemberSerialization serialization = NbtMemberSerialization.OptOut)
		{
			Serialization = serialization;
		}
	}
}
