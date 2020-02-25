import React from 'react';
import ReactDOM from 'react-dom';

import SearchPage from './SearchPage/searchPage';
import { simplePOST } from './util/simpleFetch';

require("babel-polyfill");
const uuidv1 = require('uuid/v1');

const logUserUrl = '/home/log-uuid';

class App extends React.Component {

    async componentDidMount() {
        let uuid = window.localStorage.getItem('uuid');
        if (!uuid) {
            uuid = uuidv1();
            window.localStorage.setItem('uuid', uuid);
        }
        await simplePOST(logUserUrl, { uuid: uuid });
    }

    render() {
        return (<SearchPage />);
    }
};
 
ReactDOM.render(<App />, document.getElementById('root'));