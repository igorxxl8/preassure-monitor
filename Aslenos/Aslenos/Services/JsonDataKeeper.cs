using System.Collections.Generic;
using System.Threading.Tasks;
using Aslenos.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Aslenos.Services
{
    public class JsonDataKeeper<T, TK>
    {
        public IFileWorker FileWorker { get; } = DependencyService.Get<IFileWorker>();
        public string Filename { get; set; }

        public async Task<T> Browse()
        {
            if (!await FileWorker.ExistsAsync(Filename)) return default;
            var serialized = await FileWorker.LoadTextAsync(Filename);
            return await Task.FromResult(JsonConvert.DeserializeObject<T>(serialized));
        }

        public async void Save(object @object)
        {
            await FileWorker.SaveTextAsync(Filename, JsonConvert.SerializeObject(@object));
        }

        public async void Save(TK @object)
        {
            var list = await Browse();
            if (!(list is List<TK> lk))
            {
                return;
            }

            lk.Add(@object);
            Save(lk);
        }
    }
}
