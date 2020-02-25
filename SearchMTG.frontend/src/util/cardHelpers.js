import { simpleFetch } from './simpleFetch';
import { getManaIconURL } from './cardIcons';

let hasInitialized = false;

const allFilterItems = {
    cardTypes: [],
    cardSubTypes: [],
    cardColors: [],
    cardSets: [],
    cardRarities: [],
    cardCMCRanges: {}
};

const mappedAllFiltersItems = {
    cardSets: {},
    cardRarities: {}
}

export async function fetchNewCards(query) {
    const response = await fetch('/home/get-cards', {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify({ query })
    });
    const cardsResponse = await response.json();
    const newCards = cardsResponse.Cards;
    for (let i = 0; i < newCards.length; i += 1) {
        const card = newCards[i];
        card.SetName = mappedAllFiltersItems.cardSets[card.SetName];
        card.Rarity = mappedAllFiltersItems.cardRarities[card.Rarity];
    }
    cardsResponse.CMCs = normalizeCMCRanges({
        Min: allFilterItems.cardCMCRanges.Min,
        Max: allFilterItems.cardCMCRanges.Max,
        Ranges: cardsResponse.CMCs.Ranges
    });
    return cardsResponse;
}

export async function getAllFilterItems() {
    if (!hasInitialized)
        await initFilterItems();
    return allFilterItems;
}

async function initFilterItems() {
    // Get card types
    const cardTypesPromise = simpleFetch('/home/get-all-types');
    // Get card sub-types
    const cardSubTypesPromise = simpleFetch('/home/get-all-sub-types');
    // Get card sets
    const cardSetsPromise = simpleFetch('/home/get-all-sets');
    // Get card colors
    const cardColorsPromise = simpleFetch('/home/get-all-colors');
    // Get card rarities
    const cardRaritiesPromise = simpleFetch('/home/get-all-rarity');
    // Get card converted mana costs
    const cardCMCRangesPromise = simpleFetch('/home/get-all-cmc');
    // Get promises
    allFilterItems.cardTypes = await cardTypesPromise;
    allFilterItems.cardSubTypes = await cardSubTypesPromise;
    allFilterItems.cardSets = await cardSetsPromise;
    allFilterItems.cardColors = initCardColors(await cardColorsPromise);
    allFilterItems.cardRarities = initCardRarities(await cardRaritiesPromise);
    allFilterItems.cardCMCRanges = normalizeCMCRanges(await cardCMCRangesPromise);
    // Setup mappedAllFiltersItems
    mappedAllFiltersItems.cardRarities = {};
    allFilterItems.cardRarities.forEach(rarity => 
        mappedAllFiltersItems.cardRarities[rarity.Id] = rarity.Name
    );
    mappedAllFiltersItems.cardSets = {};
    allFilterItems.cardSets.forEach(set => 
        mappedAllFiltersItems.cardSets[set.Id] = set.Name
    );
}

function initCardColors(colorItems) {
    // Add icon to card colors items
    const cardColors = [];
    for (let i = 0; i < colorItems.length; i += 1) {
        const color = colorItems[i];
        const colorChar = (color.Name == 'Blue') ? 'u' : color.Name[0];
        const IconUrl = getManaIconURL(colorChar);
        cardColors.push({ Id: color.Id, Name: color.Name, IconUrl: IconUrl });
    }
    return cardColors;
}

function initCardRarities(rarityItems) {
    // Capitalize names
    for (let i = 0; i < rarityItems.length; i += 1) {
        const name = rarityItems[i].Name;
        rarityItems[i].Name = name.charAt(0).toUpperCase() + name.slice(1);
    }
    return rarityItems;
}

function normalizeCMCRanges(cardCMCRanges) {
    const { Min, Max, Ranges } = cardCMCRanges;
    const ranges = [];
    let rangeIndex = 0;
    let costIndex = Min;
    while (costIndex <= Max) {
        if (rangeIndex >= Ranges.length)
            ranges.push({ Cost: costIndex, Count: 0 });
        else if (Ranges[rangeIndex].Cost == costIndex)
            ranges.push(Ranges[rangeIndex++]);
        else
            ranges.push({ Cost: costIndex, Count: 0 });
        costIndex += 1;
    }
    return { Min: Min, Max: Max, Ranges: ranges };
}