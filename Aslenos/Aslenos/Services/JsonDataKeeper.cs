using System.Threading.Tasks;
using Aslenos.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Aslenos.Services
{
    public class JsonDataKeeper<T> where T:class
    {
        public IFileWorker FileWorker { get; } = DependencyService.Get<IFileWorker>();
        public string Filename { get; set; }

        public async Task<T> Browse()
        {
            if (!await FileWorker.ExistsAsync(Filename)) return default;
            var serialized = await FileWorker.LoadTextAsync(Filename);
            return await Task.FromResult(JsonConvert.DeserializeObject<T>(serialized));
        }

        public async void Save(T @object)
        {
            await FileWorker.SaveTextAsync(Filename, JsonConvert.SerializeObject(@object));
        }
    }
}
