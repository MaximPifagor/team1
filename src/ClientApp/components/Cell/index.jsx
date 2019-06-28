import React from 'react';
import styles from './styles.css';

const cellTypes = {
    0: '',
    1: 'wall',
    2: 'target',
    3: 'box',
    4: 'player',
}

export default class Cell extends React.Component {
    render() {
        <div className={styles['cell cell_' + cellTypes[this.props.type]]}></div>
    }
}
