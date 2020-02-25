import React from 'react';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSearch, faTimes } from '@fortawesome/free-solid-svg-icons'

import SelectCheckBox from './SelectCheckBox';

import './styles.scss';

function descending(item1, item2) {
    const name1 = item1.Name.toLowerCase();
    const name2 = item2.Name.toLowerCase();
    if (name1 > name2)
        return 1;
    else
        return -1;
}

export default class MultiSelectCheckBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isRolledUp: (props.isRolledUp == null) ? false : props.isRolledUp,
            selected: [],
            searchValue: ''
        };
        this.handleChange = this.handleChange.bind(this);
    }
    
    onCheckHandler = (itemChecked, isChecked) => {
        const { selected } = this.state;
        if (isChecked)
            selected.push(itemChecked.Id);
        else {
            const index = selected.indexOf(itemChecked.Id);
            if (index > -1)
                selected.splice(index, 1);
        }
        this.setState({ selected });
        this.props.handler(selected);
    }

    onRollUpClick = () => {
        this.setState({ isRolledUp: !(this.state.isRolledUp) });
    }

    handleChange(event) {
        this.setState({ searchValue: event.target.value }); 
    }
    
    clearOnClick = () => {
        this.setState({ searchValue: '' }); 
    }

    render() {
        const { label, hasIcons, items, hasSearch } = this.props;
        const { searchValue, isRolledUp } = this.state;

        let searchInput = null;
        let filteredItems = null;
        if (hasSearch) {
            const clearStyles = [ 'clear' ];
            if (searchValue == '')
                clearStyles.push('disabled');
            searchInput = (
                <div className="search-box">
                    <FontAwesomeIcon className="search-icon" icon={faSearch} />
                    <input type="text" onChange={this.handleChange} value={searchValue}/>
                    <FontAwesomeIcon className={clearStyles.join(' ')} onClick={this.clearOnClick} icon={faTimes}/>
                </div>
            );

            if (searchValue == '') 
                filteredItems = items;
            else {
                filteredItems = [];
                for (let i = 0; i < items.length; i++) {
                    const item = items[i];
                    if (item.Name.toLowerCase().includes(searchValue.toLowerCase()))
                        filteredItems.push(item);
                }
            }
        }
        else {
            filteredItems = items;
        }


        let rollupStyles = [ 'roll-up' ];
        const multiContainerStyles = [ 'multi-container' ];
        if (isRolledUp) {
            rollupStyles.push('up');
            multiContainerStyles.push('collapsed');
        }
        else {
            rollupStyles.push('down');
        }

        filteredItems = filteredItems.sort(descending);

        const checkboxesJSX = filteredItems.map(item =>
            <SelectCheckBox
                key={item.Id}
                item={item}
                hasIcon={hasIcons}
                onCheckHandler={this.onCheckHandler}
            />
        );

        return (
            <React.Fragment>
                <div className="label-container">
                    <div className={rollupStyles.join(' ')} onClick={this.onRollUpClick}/>
                    <div className="label">{label}</div>
                    {searchInput}
                </div>
                <div className={multiContainerStyles.join(' ')}>
                    {checkboxesJSX}
                </div>
            </React.Fragment>
        );
    }
};
