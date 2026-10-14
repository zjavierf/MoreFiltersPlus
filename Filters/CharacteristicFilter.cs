using BetterSongList.FilterModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoreFiltersPlus.Filters {
    public class CharacteristicFilter : IFilter {
        private readonly string _characteristicSerializedName;

        public bool isReady => true;

        public CharacteristicFilter(string characteristicSerializedName) {
            _characteristicSerializedName = characteristicSerializedName;
        }

        public Task Prepare(CancellationToken cancelToken) => Task.CompletedTask;

        public bool GetValueFor(BeatmapLevel? level) {
            if (level == null) return false;

            var characteristics = level.GetCharacteristics();
            return characteristics != null && characteristics.Any(c => c.serializedName == _characteristicSerializedName);
        }
    }
}