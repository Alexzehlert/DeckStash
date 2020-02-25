import React from 'react';

import ResultsItem from './ResultItem';

import './styles.scss';


export default class ResultsList extends React.PureComponent {
    constructor(props) {
        super(props)
        this.scrollRef = React.createRef();
        this.currentHeight = 0;
    }

    handleScroll = (event) => {
        const { scrollHeight, scrollTop, clientHeight } = event.target;
        if (scrollHeight - scrollTop < clientHeight + 200) {
            // Check if height has changed
            if (this.currentHeight != scrollHeight) {
                this.currentHeight = scrollHeight;
                this.props.handleLoadMore();
            }
        }
    }

    render() {
        const { cards, mouseEnterHandler } = this.props;

        const resultListJSX = cards.map(card =>
            <ResultsItem
                key={card.Id}
                card={card}
                mouseEnterHandler={mouseEnterHandler}
            />
        );

        return (
            <div
                className="results-container"
                ref={this.scrollRef}
                onScroll={this.handleScroll}
            >
                {resultListJSX}
            </div>
        );
    }
};