import React from 'react';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSyncAlt } from '@fortawesome/free-solid-svg-icons'
import { simpleFetch } from '../../../util/simpleFetch';

const mouseDown = 'mousedown';

async function getCardImage(multiverseId) {
    const url = `/home/get-card-image?id=${multiverseId}`;
    return await simpleFetch(url);
}

export default class ResultTile extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isPopupOpen: false,
            popupImage: props.NormalImage,
            currentRotation: 0
        }
        this.popupRef = this.popupRef.bind(this);
        this.handleClickOutside = this.handleClickOutside.bind(this);
    }

    onMouseEnter = () => {
        this.props.mouseEnterHandler(this.props.card);
    }

    handleClickOutside = (event) => {
        if (this.popup && !this.popup.contains(event.target)) {
            document.removeEventListener(mouseDown, this.handleClickOutside, false);
            this.setState({
                isPopupOpen: false,
                currentRotation: 0
            });
        }
    }

    handleTileClick = async () => {
        const { card } = this.props;
        // Check if high res image exists
        if (card.NormalImage == null)
            card.NormalImage = await getCardImage(card.Id);
        this.setState({
            isPopupOpen: true,
            popupImage: card.NormalImage
        });
    }

    rotatePopupClick = () => {
        const { currentRotation } = this.state;
        this.setState({
            currentRotation: (currentRotation + 90)
        });
    }

    popupRef = (popup) => {
        this.popup = popup;
    }

    render() {
        const { card } = this.props;
        const { popupImage, currentRotation } = this.state;

        let popup = null;
        if (this.state.isPopupOpen) {
            document.addEventListener(mouseDown, this.handleClickOutside, false);
            const style = { transform: `rotate(${currentRotation}deg)` };
            popup = (
                <div ref={this.popupRef} className="popup">
                    <div className="rotate" onClick={this.rotatePopupClick}>
                        <FontAwesomeIcon icon={faSyncAlt}/>
                    </div>
                    <div className="card">
                        <img src={popupImage} style={style}/>
                    </div>
                </div>
            );
        }

        return (
            <div
                className="tile"
                onMouseEnter={this.onMouseEnter}
                onClick={this.handleTileClick}
            >
                <img src={card.NormalImage}/>
                {popup}
            </div>
        );
    }
};