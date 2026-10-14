using BetterSongList;
using BetterSongList.SortModels;
using MoreFiltersPlus.Sorters;

namespace MoreFiltersPlus {
    public static class MoreSortersInitializer {
        public static void RegisterSorters() {
            SortMethods.RegisterCustomSorter(new MapperSorter());
            SortMethods.RegisterCustomSorter(new MistakesSorter());
            SortMethods.RegisterPrimitiveSorter(new NoteCountSorter());
            SortMethods.RegisterPrimitiveSorter(new NoteDensitySorter());
        }
    }
}