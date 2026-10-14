using BetterSongList.SortModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoreFiltersPlus.Sorters {
    public class MapperSorter : ISorterCustom, ISorterWithLegend, BetterSongList.Interfaces.ITransformerPlugin, IComparer<BeatmapLevel> {
        public string name => "Mapper Name";
        public bool visible => true;
        public bool isReady => true;

        public Task Prepare(CancellationToken cancelToken) => Task.CompletedTask;

        public void DoSort(ref IEnumerable<BeatmapLevel> levels, bool ascending) {
            var list = levels.ToList();
            list.Sort(ascending ? this : (IComparer<BeatmapLevel>)new DescendingMapperComparer(this));
            levels = list;
        }

        public IEnumerable<KeyValuePair<string, int>> BuildLegend(BeatmapLevel[] levels) {
            var legendData = AlphabetLegendMaker.GetLegend(levels.ToList());
            return legendData.Select(x => new KeyValuePair<string, int>(x.Label, x.Index));
        }

        public int Compare(BeatmapLevel? x, BeatmapLevel? y) {
            var mappersX = x?.allMappers ?? Enumerable.Empty<string>();
            var mappersY = y?.allMappers ?? Enumerable.Empty<string>();
            
            string mapperX = string.Join(" ", mappersX);
            string mapperY = string.Join(" ", mappersY);
            
            return string.Compare(mapperX, mapperY, StringComparison.Ordinal);
        }

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, BeatmapLevelPack? playlist) {
        }

        private class DescendingMapperComparer : IComparer<BeatmapLevel> {
            private readonly MapperSorter _sorter;
            public DescendingMapperComparer(MapperSorter sorter) => _sorter = sorter;
            public int Compare(BeatmapLevel? x, BeatmapLevel? y) => _sorter.Compare(y, x);
        }
    }
}