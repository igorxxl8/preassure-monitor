using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using Aslenos.Interfaces;

[assembly: Dependency(typeof(Aslenos.UWP.Services.FileWorker))]
namespace Aslenos.UWP.Services
{
    public class FileWorker : IFileWorker
    {
        public async Task DeleteAsync(string filename)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var storageFile = await localFolder.GetFileAsync(filename);
            await storageFile.DeleteAsync();
        }

        public async Task<bool> ExistsAsync(string filename)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                await localFolder.GetFileAsync(filename);
            }
            catch { return false; }
            return true;
        }

        public async Task<IEnumerable<string>> GetFilesAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var filenames = from storageFile in await localFolder.GetFilesAsync()
                                            select storageFile.Name;
            return filenames;
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var helloFile = await localFolder.GetFileAsync(filename);
            var text = await FileIO.ReadTextAsync(helloFile);
            return text;
        }

        public async Task SaveTextAsync(string filename, string text)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var helloFile = await localFolder.CreateFileAsync(filename,
           CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(helloFile, text);
        }
    }
}