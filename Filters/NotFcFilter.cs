using BetterSongList.FilterModels;
using BetterSongList.Util;
using System.Threading;
using System.Threading.Tasks;
using BetterSongList.Interfaces;

namespace MoreFiltersPlus.Filters {
    public class NotFcFilter : IFilter, IAvailabilityCheck {
        public bool isReady => LocalScoresUtil.hasScores;

        public Task Prepare(CancellationToken cancelToken) {
            var t = new TaskCompletionSource<bool>();
            IPA.Utilities.Async.UnityMainThreadTaskScheduler.Factory.StartNew(() => {
                try {
                    LocalScoresUtil.Load();
                } catch {
                    // Ignore if load fails
                }
                t.SetResult(true);
            });
            return t.Task;
        }

        public string GetUnavailabilityReason() => LocalScoresUtil.hasScores ? string.Empty : "Player scores data is not ready yet.";

        public bool GetValueFor(BeatmapLevel? level) {
            if (level == null) 
                return false;

            var statsData = LocalScoresUtil.playerDataModel.playerData.levelsStatsData;
            if (statsData == null) 
                return false;

            foreach (var kvp in statsData) {
                if (kvp.Key.levelId == level.levelID && kvp.Value.validScore) {
                    if (!kvp.Value.fullCombo) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}