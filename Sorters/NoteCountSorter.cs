using BetterSongList.Interfaces;
using BetterSongList.SortModels;
using BetterSongList.Util;
using SongDetailsCache.Structs;
using System.Collections.Generic;
using System.Linq;

namespace MoreFiltersPlus.Sorters {
    public class NoteCountSorter : BasicSongDetailsSorterWithLegend, BetterSongList.Interfaces.ITransformerPlugin, IAvailabilityCheck {
        public string name => "Note Count";
        public bool visible => true;

        public NoteCountSorter() : base(
            songObj => {
                var song = (Song)songObj;
                if (song.difficulties == null || song.difficulties.Count == 0) return null;
                return (float?)song.difficulties.Max(d => d.notes);
            },
            songObj => {
                var song = (Song)songObj;
                if (song.difficulties == null || song.difficulties.Count == 0) return null;
                return song.difficulties.Max(d => d.notes).ToString();
            }
        ) { }

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, BeatmapLevelPack? playlist) {
        }
    }
}