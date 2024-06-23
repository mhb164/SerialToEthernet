using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SerialToEthernet
{
    public class TcpClientAid
    {
        public TcpClient Client { get; private set; }
        public string Name { get; private set; }
        NetworkStream stream;
        Action<string, byte[]> dataReadyAction;
        Action<TcpClientAid> disconnectedAction;

        public TcpClientAid(TcpClient client, Action<string, byte[]> dataReadyAction, Action<TcpClientAid> disconnectedAction)
        {
            this.Client = client;
            this.dataReadyAction = dataReadyAction;
            this.disconnectedAction = disconnectedAction;
            Name = client.Client.RemoteEndPoint.ToString();
            stream = client.GetStream();
            StartReader();
        }

        private void StartReader()
        {
            new Thread(() =>
            {
                try
                {
                    var bytes = new byte[1024];
                    var i = stream.Read(bytes, 0, bytes.Length);
                    while (i != 0)
                    {
                        Task.Run(() => dataReadyAction?.Invoke(Name, bytes.Take(i).ToArray()));

                        i = stream.Read(bytes, 0, bytes.Length);
                    }
                }
                catch { }
                Disconnected();
            })
            { Name = $"TcpClient{Name} Reader", IsBackground = true }.Start();
        }

        private void Disconnected()
        {
            try
            {
                disconnectedAction?.Invoke(this);
            }
            catch { }
        }

        internal void Send(byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
    }
}