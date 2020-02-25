import React from 'react';

import './styles.scss';

export default class Loader extends React.PureComponent {
    state = {
        mounted: false,
        loadingComplete: false
    };

    componentDidMount() {
        setTimeout(() => this.setState({ mounted: true }), 100);
    }

    render() {
        // Check if loading is done
        const loaderStyles = [ 'loader' ];
        if (this.props.loadingComplete) {
            loaderStyles.push('finished');
            setTimeout(() => this.setState({ loadingComplete: true }), 200);
        }

        // Check if component mounted to start animation
        const wrapperStyle = [ 'wrapper' ];
        if (this.state.mounted)
            wrapperStyle.push('mounted');

        if (this.state.loadingComplete)
            return null;

        return (
            <div className={loaderStyles.join(' ')}>
                <div className={wrapperStyle.join(' ')}>
                    <span className="font">DECK</span>
                    <span className="cards">
                        <div className="card1"/>
                        <div className="card2"/>
                        <div className="card3"/>
                        <div className="card4"/>
                        <div className="card5"/>
                    </span>
                    <span className="font">STASH</span>
                </div>
            </div>
        );
    }
}