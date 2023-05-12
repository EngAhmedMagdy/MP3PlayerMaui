using MauiApp3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Service
{
    internal interface IAudioDataService
    {
        public Task<List<AudioFile>> Get();
        public Task<AudioFile> Get(int id);
        public Task<bool> Post(AudioFile value);
        public Task<bool> Put(int id, AudioFile value);
        public Task<bool> Delete(int id);
        public Task<bool> Get(AudioFile values);
    }
}
