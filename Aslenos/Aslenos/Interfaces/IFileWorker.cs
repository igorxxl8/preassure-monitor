using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aslenos.Interfaces
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync(string filename);
        Task SaveTextAsync(string filename, string text);
        Task<string> LoadTextAsync(string filename);  
        Task<IEnumerable<string>> GetFilesAsync();  
        Task DeleteAsync(string filename);  
    }
}
