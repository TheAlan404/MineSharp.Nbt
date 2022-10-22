using Dennis.BinaryUtils;
using Netherite.Nbt.Entities;
using Netherite.Nbt.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netherite.Nbt
{
	internal static class InternalReaderExtensions
	{
		internal static NbtTagType ReadTagType(this BinaryReader reader)
		{
			int type = 0;
			try
			{
				type = reader.Read<byte>();
            }
			catch (Exception)
			{
                // Apparently, nbt document ending with the end tag is *optional*,
                // therefore we should return the End tag instead of throwing an error
                return NbtTagType.End;
            }

            if (type > (int)NbtTagType.LongArray)
            {
				long pos = -1;
				try
				{
					pos = reader.BaseStream.Position;
				} catch(Exception) { }
                throw new NbtFormatException($"NBT tag type out of range: {type} at position {(pos < 0 ? "unknown" : pos)}");
            }
            return (NbtTagType)type;
        }

		internal static string ReadSString(this BinaryReader reader)
		{
			short len = reader.Read<short>();
			return Encoding.UTF8.GetString(reader.ReadBytes(len));
		}

		internal static void WriteSString(this BinaryWriter writer, string value = "")
		{
			byte[] data = Encoding.UTF8.GetBytes(value);
			writer.Write((short)data.Length);
			writer.Write(data);
		}

		// ur bot has compress

		internal static NbtCompression DetectCompression(this BinaryReader reader)
		{
			NbtCompression compression = NbtCompression.None;
			int firstByte = reader.PeekChar();
			switch (firstByte)
			{
				case -1:
					throw new EndOfStreamException();

				case (byte)NbtTagType.Compound: // 0x0A
					compression = NbtCompression.None;
					break;

				case 0x1F: // GZip magic number
					compression = NbtCompression.GZip;
					break;

				case 0x78: // ZLib header
					compression = NbtCompression.ZLib;
					break;

				default:
					throw new InvalidDataException("Could not auto-detect compression format.");
			}
			return compression;
		}

		internal static DeflateStream GetZLibStreamForRead(this Stream stream)
		{
			if (stream.ReadByte() != 0x78)
			{
				throw new InvalidDataException("Unrecognized ZLib header. Expected 0x78");
			}
			stream.ReadByte();
			return new DeflateStream(stream, CompressionMode.Decompress, true);
		}

		internal static ZLibStream GetZLibStreamForWrite(this Stream stream)
		{
			stream.WriteByte(0x78);
			stream.WriteByte(0x01);
			return new ZLibStream(stream, CompressionMode.Compress, true);
		}
	}
}
