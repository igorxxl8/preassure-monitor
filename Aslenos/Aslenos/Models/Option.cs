using System.Windows.Input;

namespace Aslenos.Models
{
    public class Option
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }
}