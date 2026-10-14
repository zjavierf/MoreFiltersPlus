using BetterSongList.FilterModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoreFiltersPlus.Filters {
    public class NoodleExtensionsFilter : IFilter {
        public bool isReady => SongCore.Loader.AreSongsLoaded;

        public Task Prepare(CancellationToken cancelToken) => Task.CompletedTask;

        public bool GetValueFor(BeatmapLevel level) {
            if (level == null) return false;
            
            var songData = SongCore.Collections.GetCustomLevelSongData(level.levelID);
            if (songData?._difficulties == null) return false;

            return songData._difficulties.Any(x => 
                x.additionalDifficultyData?._requirements != null && 
                x.additionalDifficultyData._requirements.Any(req => req.IndexOf("Noodle Extensions", StringComparison.OrdinalIgnoreCase) >= 0)
            );
        }
    }
}