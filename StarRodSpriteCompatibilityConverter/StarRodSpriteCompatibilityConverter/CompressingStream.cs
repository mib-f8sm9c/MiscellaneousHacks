using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace StarRodSpriteCompatibilityConverter
{
    public class CompressingStream : Stream
    {
        private readonly DeflateStream _deflateStream;
        private readonly MemoryStream _buffer;
        private Stream _inputStream;
        private readonly byte[] _fileBuffer = new byte[64 * 1024];

        public CompressingStream(Stream inputStream)
        {
            _inputStream = inputStream;
            _buffer = new MemoryStream();
            _deflateStream = new DeflateStream(_buffer, CompressionMode.Compress, true);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            while (true)
            {
                var read = _buffer.Read(buffer, offset, count);

                if (read > 0) return read;

                if (_inputStream == null) return 0;

                _buffer.Position = 0;
                read = _inputStream.Read(_fileBuffer, 0, _fileBuffer.Length);
                if (read == 0)
                {
                    _inputStream.Close();
                    _inputStream = null;
                    _deflateStream.Close();
                }
                else
                {
                    _deflateStream.Write(_fileBuffer, 0, read);
                }
                _buffer.SetLength(_buffer.Position);
                _buffer.Position = 0;
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return base.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return base.BeginWrite(buffer, offset, count, callback, state);
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanTimeout
        {
            get
            {
                return base.CanTimeout;
            }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { return _buffer.Length; }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
    }

}
