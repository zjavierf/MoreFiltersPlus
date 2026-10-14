using BetterSongList;
using MoreFiltersPlus.Filters;

namespace MoreFiltersPlus {
    public static class MoreFiltersInitializer {
        public static void RegisterFilters() {
            FilterMethods.Register(new NamedFilter("Standard", new CharacteristicFilter("Standard")));
            FilterMethods.Register(new NamedFilter("One Saber", new CharacteristicFilter("OneSaber")));
            FilterMethods.Register(new NamedFilter("No Arrows", new CharacteristicFilter("NoArrows")));
            FilterMethods.Register(new NamedFilter("90 Degrees", new CharacteristicFilter("90Degree")));
            FilterMethods.Register(new NamedFilter("360 Degrees", new CharacteristicFilter("360Degree")));
            FilterMethods.Register(new NamedFilter("Not FC'd", new NotFcFilter()));
            
            FilterMethods.Register(new NamedFilter("Vivify", new VivifyFilter()));
            FilterMethods.Register(new NamedFilter("Chroma", new ChromaFilter()));
            FilterMethods.Register(new NamedFilter("Noodle Extensions", new NoodleExtensionsFilter()));
            FilterMethods.Register(new NamedFilter("Mapping Extensions", new MappingExtensionsFilter()));
        }
    }
}