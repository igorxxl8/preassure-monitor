using System.Threading.Tasks;
using Aslenos.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Aslenos.Services
{
    public class JsonDataKeeper<T> where T:class
    {
        private readonly IFileWorker _fileWorker;
        private readonly string _filename;

        public JsonDataKeeper(string filename)
        {
            _fileWorker = DependencyService.Get<IFileWorker>();
            _filename = filename;
        }

        public async Task<T> Browse()
        {
            if (!await _fileWorker.ExistsAsync(_filename)) return default;
            var serialized = await _fileWorker.LoadTextAsync(_filename);
            return await Task.FromResult(JsonConvert.DeserializeObject<T>(serialized));
        }

        public async void Save(T @object)
        {
            await _fileWorker.SaveTextAsync(_filename, JsonConvert.SerializeObject(@object));
        }
    }
}
