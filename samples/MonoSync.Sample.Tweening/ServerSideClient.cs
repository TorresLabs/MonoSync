﻿using System;
using System.IO;
using System.Net.Sockets;
using Microsoft.Xna.Framework;

namespace MonoSync.Sample.Tweening
{
    internal class ServerSideClient
    {
        private readonly byte[] _buffer = new byte[512];
        private readonly Action<Vector2> _clickHandler;
        private readonly TcpClient _client;
        private readonly NetworkStream _networkStream;

        public ServerSideClient(TcpClient client, int playerId, Action<Vector2> clickHandler)
        {
            _client = client;
            _clickHandler = clickHandler;
            PlayerId = playerId;
            _networkStream = client.GetStream();
            Read();
        }

        public int PlayerId { get; }
        public int Tick { get; set; }

        public event EventHandler Disconnected;

        private void Read()
        {
            _networkStream.BeginRead(_buffer, 0, _buffer.Length, BeginReadCallback, null);
        }

        private void BeginReadCallback(IAsyncResult ar)
        {
            try
            {
                int length = _networkStream.EndRead(ar);
                if (length == 0)
                {
                    Disconnect();
                    return;
                }

                if (_buffer[0] == Commands.CLICK_COMMAND)
                {
                    var x = BitConverter.ToSingle(_buffer, 1);
                    var y = BitConverter.ToSingle(_buffer, 5);
                    _clickHandler(new Vector2(x, y));
                }
                else if (_buffer[0] == Commands.TICK_UPDATE)
                {
                    // Refine Tick
                    Tick = BitConverter.ToInt32(_buffer, 1);
                }

                Read();
            }
            catch
            {
                Disconnect();
            }
        }

        private void Disconnect()
        {
            _client.Close();
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        public void SendWorld(byte[] worldData)
        {
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write((byte) Commands.WORLD_DATA);
            binaryWriter.Write(worldData);
            _networkStream.Write(memoryStream.ToArray());
        }
    }
}