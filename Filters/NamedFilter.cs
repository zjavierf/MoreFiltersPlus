using BetterSongList.FilterModels;
using BetterSongList.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MoreFiltersPlus.Filters {
    public class NamedFilter : ITransformerPlugin, IFilter {
        public string name { get; }
        public bool visible => true;
        public bool isReady => _innerFilter.isReady;
        
        private readonly IFilter _innerFilter;

        public NamedFilter(string name, IFilter innerFilter) {
            this.name = name;
            _innerFilter = innerFilter;
        }

        public Task Prepare(CancellationToken cancelToken) => _innerFilter.Prepare(cancelToken);
        public bool GetValueFor(BeatmapLevel level) => _innerFilter.GetValueFor(level);
        
        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, BeatmapLevelPack? playlist) {
        }
    }
}