import React from 'react';

import CustomRange from '../RangeSlider/CustomRange';

import './styles.scss';
import 'rc-slider/assets/index.css';

export default class SimpleRangeSlider extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            currentRange: [ 0, 100 ]
        }
    }

    onChangeHandler = (range) => {
        this.setState({ currentRange: range });
    };

    onAfterChangeHandler = (range) => {
        this.setState({ currentRange: range });
        this.props.handler(range);
    }

    render() {
        const { label, min, max } = this.props;
        const { currentRange } = this.state;

        return (
            <React.Fragment>
                <div className="label-container">
                    <div className="label">{label}</div>
                </div>
                <div className="range-slider">
                    <CustomRange
                        allowCross={false}
                        min={min}
                        max={max}
                        onChange={this.onChangeHandler.bind(this)}
                        onAfterChange={this.onAfterChangeHandler.bind(this)}
                        defaultValue={[ min, max ]}
                        value={currentRange}
                    />
                </div>
            </React.Fragment>
        );
    }
}