import React from 'react';

import { getManaIconURL, getSymbolIconURL, getSetIconURL } from '../../../util/cardIcons.js';

const mouseDown = 'mousedown';

function getManaCost(manaCost) {
    if (manaCost == null || manaCost == '')
        return null;
    manaCost = manaCost.substring(1, manaCost.length - 1);
    const manaCosts = manaCost.split(/\}\{/);
    const manaIcons = [];
    for (let i = 0; i < manaCosts.length; i += 1) {
        const url = getManaIconURL(manaCosts[i]);
        if (url == '')
            continue;
        manaIcons.push(<img key={i} className="mana-cost" src={url}/>);
    }
    return manaIcons;
}

function getCardText(text) {
    if (text == null)
        return null;
    const textBlocks = [];
    let beginIndex = 0;
    for (let i = 0; i < text.length;) {
        if (text[i] == '{') {
            textBlocks.push(text.slice(beginIndex, i));
            beginIndex = i = i + 1;
            while (text[i] != '}')
                i += 1;
            const symbolText = text.slice(beginIndex, i);
            const url = getSymbolIconURL(symbolText);
            textBlocks.push(<img key={i} className="symbol" src={url}/>);
            beginIndex = i = i + 1;
        }
        else
            i += 1;
    }
    if (textBlocks.length == 0)
        textBlocks.push(text);
    else {
        textBlocks.push(text.slice(beginIndex, text.length));
    }
    return textBlocks;
}

function getSetIcon(set, rarity) {
    if (set == null || rarity == null)
        return null;
    const url = getSetIconURL(set, rarity[0]);
    return (<img className="set" src={url}/>);
}

export default class ResultsItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isSelected: false
        }
        // this.handleClickOutside = this.handleClickOutside.bind(this);
    }

    onMouseEnter = () => {
        this.props.mouseEnterHandler(this.props.card);
    }

    onClick = () => {
        this.setState({ isSelected: !(this.state.isSelected) });
    }

    // handleClickOutside = (event) => {
    //     if (this.item && !this.item.contains(event.target)) {
    //         document.removeEventListener(mouseDown, this.handleClickOutside, false);
    //         this.setState({
    //             isSelected: false
    //         });
    //     }
    // }

    // itemRef = (item) => {
    //     this.item = item;
    // }

    render() {
        const { isSelected } = this.state;
        const { card } = this.props;

        let itemStyles = [ 'list-item' ];
        if (isSelected) {
            // document.addEventListener(mouseDown, this.handleClickOutside, false);
            itemStyles.push('selected');
        }

        return (
            <div
                className={itemStyles.join(' ')}
                // ref={this.itemRef}
                onClick={this.onClick}
                onMouseEnter={this.onMouseEnter}
            >
                <div className="image-crop">
                    <img src={card.NormalImage}/>
                </div>
                <div className="content">
                    <div className="title">
                        {card.Name}
                        {getManaCost(card.ManaCost)}
                    </div>
                    <hr className="line-break"/>
                    <div className="sub">
                        {card.Type}
                        {getSetIcon(card.Set, card.Rarity)}
                    </div>
                    <div className="description">
                        {getCardText(card.Text)}
                    </div>
                    <div className="flavor">
                        {card.Flavor}
                    </div>
                    <div className="footer">
                        <div className="artist">
                            {card.Artist}
                        </div>
                        <div className="strength">
                            {(card.Power == null) ? null : `${card.Power}/${card.Toughness}`}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};