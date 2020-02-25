import React from 'react';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSearch } from '@fortawesome/free-solid-svg-icons'

import './styles.scss';

export default class SearchInput extends React.Component {
    
    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        const { inputHandler } = this.props;
        inputHandler(event.target.value);
    }

    render() {
        return (
            <div className="search-input-container">
                <FontAwesomeIcon className="search-icon" icon={faSearch} />
                <input
                    type="text"
                    onChange={this.handleChange}
                    placeholder="Search... "
                />
            </div>
        );
    }
};