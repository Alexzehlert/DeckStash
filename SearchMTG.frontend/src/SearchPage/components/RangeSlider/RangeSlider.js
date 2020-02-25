import React from 'react';

import CustomRange from './CustomRange';

import './styles.scss';
import 'rc-slider/assets/index.css';


export default class RangeSlider extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            currentRange: [ props.bounds.min, props.bounds.max ]
        }
    }

    onChangeHandler = (range) => {
        this.setState({ currentRange: range });
        // this.props.handler(range);
    };

    onAfterChangeHandler = (range) => {
        this.setState({ currentRange: range });
        this.props.handler(range);
    }

    onBarClick = (cost) => {
        const range = [ cost, cost ];
        this.props.handler(range);
        this.setState({ currentRange: range });
    };

    render() {
        const { label, ranges, bounds } = this.props;
        const { currentRange } = this.state;
        const spacing = 2;

        // Setup bars
        let highestCount = 0;
        for (let i = 0; i < ranges.length; i += 1) {
            const range = ranges[i];
            if (range.Count > highestCount)
                highestCount = range.Count;
        }
        const bars = [];
        const numbers = [];
        const barWidth = (100 - (spacing * ranges.length)) / ranges.length;
        const costWidth = 100 / ranges.length;
        for (let i = 0; i < ranges.length; i += 1) {
            const barStyles = [ 'bar' ];
            const range = ranges[i];
            const height = Math.ceil((range.Count / highestCount) * 100);
            if (range.Cost >= currentRange[0] && range.Cost <= currentRange[1])
                barStyles.push('bound');
            bars.push(
                <div
                    key={range.Cost}
                    className={barStyles.join(' ')}
                    style={{ height: height + '%', width: barWidth + '%' }}
                    onClick={() => this.onBarClick(range.Cost)}
                />
            );
            numbers.push(
                <div
                    key={range.Cost}
                    className="cost"
                    style={{ width: costWidth + '%' }}
                >
                    {range.Cost}
                </div>
            );
        }

        return (
            <React.Fragment>
                <div className="label-container">
                    <div className="label">{label}</div>
                </div>
                <div className="range-slider">
                    <div className="bars">
                        {bars}
                    </div>
                    <div className="costs">
                        {numbers}
                    </div>
                    <CustomRange
                        allowCross={false}
                        min={bounds.min}
                        max={bounds.max}
                        onChange={this.onChangeHandler.bind(this)}
                        onAfterChange={this.onAfterChangeHandler.bind(this)}
                        defaultValue={[ bounds.min, bounds.max ]}
                        value={currentRange}
                    />
                </div>
            </React.Fragment>
        );
    }
}