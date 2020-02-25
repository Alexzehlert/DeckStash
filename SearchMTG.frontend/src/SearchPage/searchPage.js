import React from 'react';

import DynamicBackground from './components/DynamicBackground/DynamicBackground';
import Loader from './components/Loader/Loader';
import SearchInput from './components/SearchInput/SearchInput';
import MultiSelectCheckBox from './components/MultiSelectCheckbox/MultiSelectCheckBox';
import RangeSlider from './components/RangeSlider/RangeSlider';
import ResultsList from './components/ResultsList/ResultsList';
import ResultsTiles from './components/ResultsTiles/ResultsTiles';
import ResultsToggle from './components/ResultsToggle/ResultsToggle';
import { getAllFilterItems, fetchNewCards } from '../util/cardHelpers';
import { getDefaultQuery } from '../util/query';
import logo from "../../img/favicon.ico";

import './styles.scss';

export default class SearchPage extends React.Component {
    state = {
        initialLoadFinished: false,
        isListResults: false,
        cards: [],
        cardTypes: [],
        cardSubTypes: [],
        cardColors: [],
        cardRarities: [],
        cardSets: [],
        initialCMCBounds: { min: 0, max: 16 },
        cardCMCRanges: [],
        hoveredCard: null,
        lastPageReached: false,
        resultsKey: 0,
    };

    constructor(props) {
        super(props);
        this.query = getDefaultQuery();
    }

    async componentDidMount() {
        // Initial card fetch
        const fetchCardsPromise = this.fetchCards();
        // Get default filter items
        const defaultsPromise = getAllFilterItems();
        // Await promises
        await fetchCardsPromise;
        const defaults = await defaultsPromise;
        this.setState({
            initialLoadFinished: true,
            cardTypes: defaults.cardTypes,
            cardSubTypes: defaults.cardSubTypes,
            cardColors: defaults.cardColors,
            cardSets: defaults.cardSets,
            cardRarities: defaults.cardRarities,
            initialCMCBounds: { min: defaults.cardCMCRanges.Min, max: defaults.cardCMCRanges.Max },
            cardCMCRanges: defaults.cardCMCRanges.Ranges
        });
    }

    searchInputHandler = async (input) => {
        this.query.InputSearch = input;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }

    searchTypeHandler = async (typesSelected) => {
        this.query.TypeIds = typesSelected;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }

    searchSubTypeHandler = async (subtypesSelected) => {
        this.query.SubTypeIds = subtypesSelected;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }

    searchColorHandler = async (colorsSelected) => {
        this.query.ColorIds = colorsSelected;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }

    searchSetHandler = async (setsSelected) => {
        this.query.SetIds = setsSelected;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }

    searchRarityHandler = async (raritySelected) => {
        this.query.rarityIds = raritySelected;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }

    cmcRangeHandler = async (cmcRange) => {
        this.query.CmcRange = cmcRange;
        this.query.PageNumber = 0;
        await this.fetchCards();
    }
    
    fetchCards = async () => {
        const cardsResponse = await fetchNewCards(this.query);
        const newCards = cardsResponse.Cards;
        const hasCards = (newCards.length > 0);
        // Check if first card grab of query
        let cards = null;
        let resultsKey = this.state.resultsKey;
        if (this.query.PageNumber == 0) {
            cards = newCards;
            resultsKey += 1;
        }
        else
            cards = this.state.cards.concat(newCards);
        this.setState({
            cards,
            hoveredCard: hasCards ? cards[0] : this.state.hoveredCard,
            lastPageReached: !hasCards,
            resultsKey,
            cardCMCRanges: cardsResponse.CMCs.Ranges
        });
    }

    mouseEnterHandler = (card) => {
        // console.log(card);
        this.setState({ hoveredCard: card });
    }

    handleLoadMore = async () => {
        if (this.state.lastPageReached)
            return;
        this.query.PageNumber += 1;
        await this.fetchCards();
    }

    handleResultsViewToggle = () => {
        this.setState({ isListResults: !(this.state.isListResults) });
    }

    render() {
        const { initialLoadFinished, cards, hoveredCard, resultsKey } = this.state;

        let background = null
        if (hoveredCard != null) {
            background = (
                <DynamicBackground
                    urlId={hoveredCard.Id}
                    src={hoveredCard.CroppedImage}
                />
            );
        }

        let results;
        if (this.state.isListResults) {
            results = (
                <ResultsList
                    key={resultsKey}
                    cards={cards}
                    mouseEnterHandler={this.mouseEnterHandler}
                    handleLoadMore={this.handleLoadMore}
                />
            );
        }
        else {
            results = (
                <ResultsTiles
                    key={resultsKey}
                    cards={cards}
                    mouseEnterHandler={this.mouseEnterHandler}
                    handleLoadMore={this.handleLoadMore}
                />
            );
        }

        return (
            <div className="search-page">
                <Loader loadingComplete={initialLoadFinished}/>
                {background}
                <div className="site-title">
                    <span>Deck</span>
                    <img className="logo" src={logo} />
                    <span>Stash</span>
                </div>
                <div className="filter-panel-container">
                    <div className="title">
                        <div>MAGIC</div>
                        <div>THE GATHERING</div>
                    </div>
                    <div className="scroll">
                        <div className="filter-container">
                            <MultiSelectCheckBox
                                label="Color"
                                items={this.state.cardColors}
                                hasIcons={true}
                                handler={this.searchColorHandler}
                            />
                        </div>
                        <div className="filter-container">
                            <MultiSelectCheckBox
                                label="Type"
                                hasSearch={true}
                                items={this.state.cardTypes}
                                hasIcons={false}
                                handler={this.searchTypeHandler}
                            />
                        </div>
                        <div className="filter-container">
                            <MultiSelectCheckBox
                                label="Rarity"
                                items={this.state.cardRarities}
                                hasIcons={false}
                                handler={this.searchRarityHandler}
                            />
                        </div>
                        <div className="filter-container">
                            <RangeSlider
                                label="Converted Mana Cost"
                                bounds={this.state.initialCMCBounds}
                                ranges={this.state.cardCMCRanges}
                                handler={this.cmcRangeHandler}
                            />
                        </div>
                        <div className="filter-container">
                            <MultiSelectCheckBox
                                label="Sub-Type"
                                isRolledUp={true}
                                hasSearch={true}
                                items={this.state.cardSubTypes}
                                hasIcons={false}
                                handler={this.searchSubTypeHandler}
                            />
                        </div>
                        <div className="filter-container">
                            <MultiSelectCheckBox
                                label="Set"
                                isRolledUp={true}
                                hasSearch={true}
                                items={this.state.cardSets}
                                hasIcons={false}
                                handler={this.searchSetHandler}
                            />
                        </div>
                    </div>
                </div>
                <div className="search-results-container">
                    <ResultsToggle
                        toggleHandler={this.handleResultsViewToggle}
                        isListResults={this.state.isListResults}
                    />
                    <SearchInput inputHandler={this.searchInputHandler}/>
                    {results}
                </div>
            </div>
        );
    }
}

