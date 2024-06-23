using System.IO.Ports;

namespace SerialToEthernet
{
    public class SerialPortConfig
    {
        public string Portname { get; set; }
        public int BaudRate { get; set; } = 9600;
        public int DataBits { get; set; } = 8;
        public Handshake Handshake { get; set; } = Handshake.None;
        public Parity Parity { get; set; } = Parity.None;
        public StopBits StopBits { get; set; } = StopBits.Two;
    }
}