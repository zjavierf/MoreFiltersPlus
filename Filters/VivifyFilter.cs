using BetterSongList.FilterModels;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SongCore;

namespace MoreFiltersPlus.Filters
{
    public class VivifyFilter : IFilter
    {
        public bool isReady => true;

        public Task Prepare(CancellationToken cancelToken) => Task.CompletedTask;

        public bool GetValueFor(BeatmapLevel? level)
        {
            if (level == null) return false;
            
            if (Loader.CustomLevels != null)
            {
                var folderPath = Loader.CustomLevels.FirstOrDefault(x => x.Value == level).Key;
                if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
                {
                    string[] vivifyFiles = Directory.GetFiles(folderPath, "*.vivify", SearchOption.TopDirectoryOnly);
                    return vivifyFiles.Length > 0;
                }
            }

            return false;
        }
    }
}