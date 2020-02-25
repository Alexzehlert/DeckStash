

export function getDefaultQuery() {
    return {
        // Number of results per page
        PageSize: 20,
        // Current page you are on 
        PageNumber: 0,
        // Search input
        InputSearch: '',
        // Types selected
        TypeIds: [],
        // Sub-Types selected
        SubTypeIds: [],
        // Colors selected
        ColorIds: [],
        // Sets selected
        SetIds: [],
        // Rarity selected
        rarityIds: [],
        // Converted mana cost range
        CmcRange: [ 0, 100 ],
    };
}