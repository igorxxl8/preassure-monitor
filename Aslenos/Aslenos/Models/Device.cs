using System;

namespace Aslenos.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Device((Guid, string) data)
        {
            var (guid, name) = data;
            Id = guid;
            Name = name;
        }
    }
}