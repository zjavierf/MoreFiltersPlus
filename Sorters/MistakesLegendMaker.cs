using System.Collections.Generic;

namespace MoreFiltersPlus.Sorters {
    public static class MistakesLegendMaker {
        public static List<(string Label, int Index)> GetLegend(IList<BeatmapLevel> levels, Dictionary<string, int?> mistakeValues) {
            var legend = new List<(string Label, int Index)>();
            int lastBucket = -999;

            for (int i = 0; i < levels.Count; i++) {
                var level = levels[i];
                if (level == null) continue;

                if (mistakeValues.TryGetValue(level.levelID, out var val) && val.HasValue) {
                    int mistakes = val.Value;
                    int bucket = mistakes == 0 ? 0 : (mistakes / 5) * 5;
                    
                    if (bucket != lastBucket) {
                        lastBucket = bucket;
                        legend.Add((bucket.ToString(), i));
                    }
                } else {
                    // For songs with no play data / never played
                    if (lastBucket != -1) {
                        lastBucket = -1;
                        legend.Add(("None", i));
                    }
                }
            }

            return legend;
        }
    }
}