import React from 'react';

export default class SelectCheckBox extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            isChecked: false
        }
    }

    onClick = () => {
        const isChecked = !this.state.isChecked;
        this.props.onCheckHandler(this.props.item, isChecked);
        this.setState({ isChecked: isChecked });
    }

    render() {
        const { item, hasIcon } = this.props;
        const { isChecked } = this.state;

        const itemStyles = [ 'multi-item' ];
        if (isChecked)
            itemStyles.push('selected');

        return(
            <div
                className={itemStyles.join(' ')}
                onClick={this.onClick}
                title={item.Name}
            >
                <input
                    readOnly
                    type="checkbox"
                    checked={isChecked}
                />
                <span className="checkmark"/>
                {(hasIcon) ? (<img className="icon" src={item.IconUrl}/>) : null }
                <div className="text">
                    {item.Name}
                </div>
            </div>
        );
    }
}

 