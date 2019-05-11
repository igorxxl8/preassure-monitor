using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Aslenos.Interfaces;

[assembly: Dependency(typeof(Aslenos.Droid.Services.FileWorker))]
namespace Aslenos.Droid.Services
{
    public class FileWorker : IFileWorker
    {
        public Task DeleteAsync(string filename)
        {
            File.Delete(GetFilePath(filename));
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(string filename)
        {
            var filepath = GetFilePath(filename);
            var exists = File.Exists(filepath);
            return Task.FromResult(exists);
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            var filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task.FromResult(filenames);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            var filepath = GetFilePath(filename);
            using (var reader = File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task SaveTextAsync(string filename, string text)
        {
            var filepath = GetFilePath(filename);
            using (var writer = File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }
        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }
        string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}