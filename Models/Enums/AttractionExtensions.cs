using System;

namespace Models.Enums;

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
            //Monuments History
            AttractionType.Castle => AttractionCategory.MonumentsAndHistory,
            AttractionType.Museum => AttractionCategory.MonumentsAndHistory,
            AttractionType.Monuments => AttractionCategory.MonumentsAndHistory,
            AttractionType.Church => AttractionCategory.MonumentsAndHistory,
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
            AttractionType.Cafe => AttractionCategory.FoodAndDrinks,
            AttractionType.Bakery => AttractionCategory.FoodAndDrinks,
            AttractionType.Brewery => AttractionCategory.FoodAndDrinks,
            AttractionType.Restaurant => AttractionCategory.FoodAndDrinks,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"No category mapped for {type}")
        };
    }
}
