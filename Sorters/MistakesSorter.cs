using BetterSongList.SortModels;
using BetterSongList.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoreFiltersPlus.Sorters {
    public class MistakesSorter : ISorterCustom, ISorterWithLegend, BetterSongList.Interfaces.ITransformerPlugin {
        public string name => "Mistakes";
        public bool visible => true;
        public bool isReady => LocalScoresUtil.hasScores;

        private Dictionary<string, int?> _lastMistakeValues = new();

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

        public void DoSort(ref IEnumerable<BeatmapLevel> levels, bool ascending) {
            var levelList = levels.ToList();
            _lastMistakeValues.Clear();

            foreach (var lvl in levelList) {
                _lastMistakeValues[lvl.levelID] = GetMistakesForLevel(lvl);
            }

            levelList.Sort((x, y) => {
                int? valX = x != null && _lastMistakeValues.TryGetValue(x.levelID, out var vx) ? vx : null;
                int? valY = y != null && _lastMistakeValues.TryGetValue(y.levelID, out var vy) ? vy : null;

                int result;
                if (valX.HasValue && valY.HasValue) {
                    result = valX.Value.CompareTo(valY.Value);
                } else if (valX.HasValue) {
                    result = -1;
                } else if (valY.HasValue) {
                    result = 1;
                } else {
                    result = 0;
                }
                return ascending ? result : -result;
            });

            levels = levelList;
        }

        public IEnumerable<KeyValuePair<string, int>> BuildLegend(BeatmapLevel[] levels) {
            var legendData = MistakesLegendMaker.GetLegend(levels.ToList(), _lastMistakeValues);
            return legendData.Select(x => new KeyValuePair<string, int>(x.Label, x.Index));
        }

        private int? GetMistakesForLevel(BeatmapLevel level) {
            var statsData = LocalScoresUtil.playerDataModel?.playerData?.levelsStatsData;
            if (statsData == null) return null;

            int totalMistakes = -1;

            foreach (var kvp in statsData) {
                if (kvp.Key.levelId == level.levelID && kvp.Value != null && kvp.Value.validScore) {
                    var stats = kvp.Value;
                    
                    int misses = GetIntFieldOrProperty(stats, "missedCount", "missedNotesCount", "numberOfMisses");
                    int badCuts = GetIntFieldOrProperty(stats, "badCutsCount", "badCuts");
                    int wallHits = GetIntFieldOrProperty(stats, "obstaclesHitCount", "wallsHitCount", "hitObstaclesCount");

                    int currentMistakes = misses + badCuts + wallHits;

                    if (totalMistakes == -1 || currentMistakes < totalMistakes) {
                        totalMistakes = currentMistakes;
                    }
                }
            }

            return totalMistakes == -1 ? null : totalMistakes;
        }

        private int GetIntFieldOrProperty(object obj, params string[] propertyNames) {
            var type = obj.GetType();
            foreach (var propName in propertyNames) {
                var field = type.GetField(propName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (field != null && field.GetValue(obj) is int fieldValue) return fieldValue;

                var prop = type.GetProperty(propName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (prop != null && prop.GetValue(obj) is int propValue) return propValue;
            }
            return 0;
        }

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, BeatmapLevelPack? playlist) {
        }
    }
}