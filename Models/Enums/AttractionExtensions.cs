using System;

namespace Models.Enums;
// Experiment med extension method, kopplar type till category med en switch
public static class AttractionTypeExtensions
{
    public static AttractionCategory GetCategory(this AttractionType type)
    {
        return type switch
        {   // Nature
            AttractionType.Nationalpark => AttractionCategory.Nature,
            AttractionType.Waterfall => AttractionCategory.Nature,
            AttractionType.Beach => AttractionCategory.Nature,
            AttractionType.Hiking => AttractionCategory.Nature,
            AttractionType.Camping => AttractionCategory.Nature,
            //Monuments
            AttractionType.Castle => AttractionCategory.Monuments,
            AttractionType.Monuments => AttractionCategory.Monuments,
            //History
            AttractionType.Museum => AttractionCategory.History,
            AttractionType.Church => AttractionCategory.History,
            // City
            AttractionType.Buildings => AttractionCategory.City,
            AttractionType.Bridge => AttractionCategory.City,
            AttractionType.Market => AttractionCategory.City,
            // Entertainment 
            AttractionType.Amusementpark => AttractionCategory.Entertainment,
            AttractionType.Zoo => AttractionCategory.Entertainment,
            AttractionType.Theather => AttractionCategory.Entertainment,
            AttractionType.Stadium => AttractionCategory.Entertainment,
            // Food and drinks
            AttractionType.Cafe => AttractionCategory.Food,
            AttractionType.Bakery => AttractionCategory.Food,
            AttractionType.Brewery => AttractionCategory.Food,
            AttractionType.Restaurant => AttractionCategory.Food,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"No category mapped for {type}")
        };
    }
}
