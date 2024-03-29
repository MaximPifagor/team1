import React from 'react';
import styles from './styles.css';

const cellTypes = {
    0: '',
    1: 'wall',
    2: 'target',
    3: 'box',
    4: 'player',
    5: 'player',
    6: 'boxOnTarget',
}

export default function Cell(props) {
    const size = props.size + '%';
    return (
        <div className={`${styles.cell} ${styles[`cell_${cellTypes[props.type]}`]}`} style={{ width: size, height: size }}></div>
    );
}
