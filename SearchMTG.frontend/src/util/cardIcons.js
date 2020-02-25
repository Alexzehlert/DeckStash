const MANA_URL = 'http://eakett.ca/mtgimage/symbol/mana/';
const SYMBOL_URL = 'http://eakett.ca/mtgimage/symbol/other/';
const SET_URL = 'http://eakett.ca/mtgimage/symbol/set/';

export function getManaIconURL(mana) {
    return `${MANA_URL}${mana.toLowerCase()}.svg`
}

export function getSymbolIconURL(symbol) {
    let url;
    symbol = symbol.toLowerCase();
    switch (symbol) {
        case 't':
        case 'chaos':
            url = SYMBOL_URL;
            break;
        case 'c':
            url = MANA_URL;
            symbol = '1'
            break;
        default:
            url = MANA_URL;
            break;
    }
    return `${url}${symbol}.svg`
}

export function getSetIconURL(set, rarity) {
    return `${SET_URL}${set.toLowerCase()}/${rarity.toLowerCase()}.svg`
}