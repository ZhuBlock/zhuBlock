import React, { Component } from 'react';
import { injectNOS } from './lib/nOS/nOS';

class App extends Component {
  render() {
    return (
      <div className="App">
        <p>nOS info: {this.props.nos.exists ? 'nOS found': 'nOS was not found!'}</p>
      </div>
    );
  }
}

export default injectNOS(App);
