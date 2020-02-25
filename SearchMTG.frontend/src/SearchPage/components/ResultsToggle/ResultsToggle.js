import React from 'react';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faListUl, faTh } from '@fortawesome/free-solid-svg-icons'

import './styles.scss';

export default class SearchPage extends React.PureComponent {
    render() {
        const { toggleHandler, isListResults } = this.props;

        const listToggleStyles = [ 'list' ];
        const tilesToggleStyles = [ 'tile' ];
        if (isListResults) {
            listToggleStyles.push('active');
        }
        else {
            tilesToggleStyles.push('active');
        }

        return (
            <div className="results-toggles">
                <button
                    disabled={!isListResults}
                    className={tilesToggleStyles.join(' ')}
                    onClick={toggleHandler}
                >
                    <FontAwesomeIcon icon={faTh} />
                </button>
                <button
                    disabled={isListResults}
                    className={listToggleStyles.join(' ')}
                    onClick={toggleHandler}
                >
                    <FontAwesomeIcon icon={faListUl} />
                </button>
            </div>
        );
    }
}