using BetterSongList.Interfaces;
using BetterSongList.SortModels;
using BetterSongList.Util;
using SongDetailsCache.Structs;
using System.Collections.Generic;
using System.Linq;

namespace MoreFiltersPlus.Sorters {
    public class NoteDensitySorter : BasicSongDetailsSorterWithLegend, BetterSongList.Interfaces.ITransformerPlugin, IAvailabilityCheck {
        public string name => "Note Density (NPS)";
        public bool visible => true;

        public NoteDensitySorter() : base(
            songObj => {
                var song = (Song)songObj;
                if (song.songDurationSeconds <= 0 || song.difficulties == null || song.difficulties.Count == 0) return null;
                float maxNps = song.difficulties.Max(d => (float)d.notes / song.songDurationSeconds);
                return maxNps > 0f ? (float?)maxNps : null;
            },
            songObj => {
                var song = (Song)songObj;
                if (song.songDurationSeconds <= 0 || song.difficulties == null || song.difficulties.Count == 0) return null;
                float maxNps = song.difficulties.Max(d => (float)d.notes / song.songDurationSeconds);
                return maxNps > 0f ? $"{maxNps:0.0} NPS" : null;
            }
        ) { }

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, BeatmapLevelPack? playlist) {
        }
    }
}