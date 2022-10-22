using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netherite.Nbt.Binary
{
    internal class BinaryWriterBigEndian : BinaryWriter
    {
        public BinaryWriterBigEndian(Stream stream) : base(stream)
        {
        }

        public override void Write(int i)
        {
            byte[] data = BitConverter.GetBytes(i);
            Array.Reverse(data);
            Write(data);
        }

        public override void Write(short s)
        {
            byte[] data = BitConverter.GetBytes(s);
            Array.Reverse(data);
            Write(data);
        }

        public override void Write(long l)
        {
            byte[] data = BitConverter.GetBytes(l);
            Array.Reverse(data);
            Write(data);
        }

        public override void Write(uint i)
        {
            byte[] data = BitConverter.GetBytes(i);
            Array.Reverse(data);
            Write(data);
        }

        public override void Write(ushort s)
        {
            byte[] data = BitConverter.GetBytes(s);
            Array.Reverse(data);
            Write(data);
        }

        public override void Write(float f)
        {
            byte[] data = BitConverter.GetBytes(f);
            Array.Reverse(data);
            Write(data);
        }
    }
}
