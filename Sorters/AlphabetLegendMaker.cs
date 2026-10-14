using System.Collections.Generic;
using System.Linq;

namespace MoreFiltersPlus.Sorters {
    public static class AlphabetLegendMaker {
        public static List<(string Label, int Index)> GetLegend(IList<BeatmapLevel> levels) {
            var legend = new List<(string Label, int Index)>();
            char lastChar = '\0';

            for (int i = 0; i < levels.Count; i++) {
                var level = levels[i];
                string mapper = level != null && level.allMappers != null ? string.Join("", level.allMappers) : string.Empty;
                
                char firstChar = string.IsNullOrEmpty(mapper) ? '#' : char.ToUpperInvariant(mapper[0]);
                if (!char.IsLetter(firstChar)) {
                    firstChar = '#';
                }

                if (firstChar != lastChar) {
                    lastChar = firstChar;
                    legend.Add((lastChar.ToString(), i));
                }
            }

            return legend;
        }
    }
}