using System;

namespace Aslenos.Helpers
{
    public static class Guids
    {
        public static readonly Guid UART_SERVICE = Guid.Parse("6e400001-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid RX_DATA_CHARACTERISTIC = Guid.Parse("6e400003-b5a3-f393-e0a9-e50e24dcca9e");
        public static readonly Guid TX_DATA_CHARACTERISTIC = Guid.Parse("6e400002-b5a3-f393-e0a9-e50e24dcca9e");
    }
}
