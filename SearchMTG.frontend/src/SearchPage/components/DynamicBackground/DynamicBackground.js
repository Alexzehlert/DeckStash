import React from 'react';
import PropTypes from "prop-types";

import './styles.scss';

export default class DynamicBackground extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            topSrc: props.src,
            bottomOpacity: 0,
            bottomSrc: props.src,
            loadingImage: false
        };
    }

    componentWillReceiveProps(newProps) {
        if (this.props.urlId !== newProps.urlId && !this.state.loadingImage) {
            const oldSrc = this.state.topSrc;
            const newSrc = newProps.src;
            // Reset the component everytime we receive new prop, to
            // cancel out any animation that's still going on
            this.setState({ bottomSrc: false, topSrc: false, loadingImage: true }, () =>
                this.setState(
                    // Opacity less than 1 takes precendence in stacking order
                    { bottomSrc: oldSrc, topSrc: newSrc, bottomOpacity: 0.99 }
                )
            );
        }
    }

    handleImageLoaded = () => {
        // Give small amount of time to display in browser
        setTimeout(this.imageFadeStart, 20);
    }

    imageFadeStart = () => {
        // Start fading the image transition is 500 ms
        this.setState({ bottomOpacity: 0 });
        // Let fade finish at least halfway (full transition 500 ms)
        setTimeout(this.imageFadeComplete, 250);
    }

    imageFadeComplete = () => {
        this.setState({ loadingImage: false });
    }

    render() {
        const { alt } = this.props;
        const { topSrc, bottomOpacity, bottomSrc } = this.state;

        return (
            <div className="dynamic-background">
                {
                    topSrc &&
                    <img
                        src={topSrc}
                        style={{ position: "absolute" }}
                        alt={alt}
                        onLoad={this.handleImageLoaded}
                    />
                }
                {
                    bottomSrc &&
                    <img style={{ opacity: bottomOpacity }} src={bottomSrc} />
                }
            </div>
        );
    }
}

DynamicBackground.propTypes = {
    urlId: PropTypes.number.isRequired,
    src: PropTypes.string.isRequired,
    alt: PropTypes.string,
    duration: PropTypes.number,
    timingFunction: PropTypes.string
};