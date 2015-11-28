﻿using System;
using System.IO;
using Dia;

namespace DiaAdapter
{

    class DiaMemoryStream : StubMemoryStream
    {
        private Stream PdbFile { get; }

        internal DiaMemoryStream(Stream pdbFile)
        {
            this.PdbFile = pdbFile;
        }

        unsafe public override void RemoteRead(out byte buffer, uint bufferSize, out uint bytesRead)
        {
            fixed (byte* addressOfBuffer = &buffer)
            {
                for (bytesRead = 0; bytesRead < bufferSize; bytesRead++)
                {
                    var nextByte = PdbFile.ReadByte();
                    if (nextByte == -1)
                        return;
                    addressOfBuffer[bytesRead] = (byte)nextByte;
                }
            }
        }

        unsafe public override void RemoteSeek(_LARGE_INTEGER offset, uint seekOrigin, out _ULARGE_INTEGER newPosition)
        {
            newPosition.QuadPart = (ulong)PdbFile.Seek(offset.QuadPart, (SeekOrigin)seekOrigin);
        }

        unsafe public override void Stat(out tagSTATSTG pstatstg, uint grfStatFlag)
        {
            pstatstg = new tagSTATSTG
            {
                cbSize = new _ULARGE_INTEGER
                {
                    QuadPart = (ulong)PdbFile.Length
                }
            };
        }

    }


    abstract class StubMemoryStream : IStream
    {
        unsafe public abstract void RemoteRead(out byte pv, uint cb, out uint pcbRead);
        unsafe public abstract void RemoteSeek(_LARGE_INTEGER dlibMove, uint dwOrigin, out _ULARGE_INTEGER plibNewPosition);
        unsafe public abstract void Stat(out tagSTATSTG pstatstg, uint grfStatFlag);

        void IStream.Clone(out IStream ppstm)
        {
            throw new NotImplementedException();
        }

        void IStream.Commit(uint grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        void IStream.LockRegion(_ULARGE_INTEGER libOffset, _ULARGE_INTEGER cb, uint dwLockType)
        {
            throw new NotImplementedException();
        }

        void IStream.RemoteCopyTo(IStream pstm, _ULARGE_INTEGER cb, out _ULARGE_INTEGER pcbRead, out _ULARGE_INTEGER pcbWritten)
        {
            throw new NotImplementedException();
        }

        void ISequentialStream.RemoteRead(out byte pv, uint cb, out uint pcbRead)
        {
            throw new NotImplementedException();
        }

        void ISequentialStream.RemoteWrite(ref byte pv, uint cb, out uint pcbWritten)
        {
            throw new NotImplementedException();
        }

        void IStream.RemoteWrite(ref byte pv, uint cb, out uint pcbWritten)
        {
            throw new NotImplementedException();
        }

        void IStream.Revert()
        {
            throw new NotImplementedException();
        }

        void IStream.SetSize(_ULARGE_INTEGER libNewSize)
        {
            throw new NotImplementedException();
        }

        void IStream.UnlockRegion(_ULARGE_INTEGER libOffset, _ULARGE_INTEGER cb, uint dwLockType)
        {
            throw new NotImplementedException();
        }

    }

}