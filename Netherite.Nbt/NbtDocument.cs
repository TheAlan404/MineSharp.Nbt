using Netherite.Nbt.Binary;
using Netherite.Nbt.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netherite.Nbt
{
	/// <summary>
	/// Main class for Nbt data. Intended to be used instead of other classes such as NbtReader/Writer
	/// </summary>
	public class NbtDocument : NbtCompound
	{
		internal override bool isRoot => true;

		public NbtDocument() { }

		/// <summary>
		/// Creates an NbtDocument from an NbtCompound. Note that this does not deep copy
		/// </summary>
		/// <param name="compound">The compound to use as the base</param>
		public NbtDocument(NbtCompound compound)
		{
			Name = compound.Name;
			AddRange(compound.Tags);
		}

		#region conv

		public byte[] ToBinary()
		{
			var ms = new MemoryStream();
			new NbtWriter(ms).Write(this);
			return ms.ToArray();
		}

		public static NbtDocument FromBinary(byte[] data)
			=> FromBinary(new MemoryStream(data));

		public static NbtDocument FromBinary(Stream data)
		{
			return new NbtReader(data).Read();
		}

		#endregion
	}
}
