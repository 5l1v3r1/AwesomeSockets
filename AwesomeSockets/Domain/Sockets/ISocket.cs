﻿using AwesomeSockets.Domain.SocketModifiers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Buffer = AwesomeSockets.Buffers.Buffer;

namespace AwesomeSockets.Domain.Sockets
{
    class WithModifierWrapper<T> where T : ISocketModifier, new()
    {
        public ISocket ApplyModifier(ISocket socket, params string[] args)
        {
            return new T().Apply(socket, args);
        }
    }

    public interface ISocket
    {
        void SetGlobalConfiguration(Dictionary<SocketOptionName, object> opts);
        Socket GetSocket();

        ISocket Accept();
        void Connect(EndPoint remoteEndPoint);

        int SendMessage(Buffer buffer);
        int SendMessage(string ip, int port, Buffer buffer);

        Tuple<int, EndPoint> ReceiveMessage(Buffer buffer);
        Tuple<int, EndPoint> ReceiveMessage(string ip, int port, Buffer buffer);

        EndPoint GetRemoteEndPoint();
        ProtocolType GetProtocolType();

        int GetBytesAvailable();

        void Close(int timeout = 0);

        ISocket WithModifier<T>(params string[] args) where T : ISocketModifier, new();
    }
}
