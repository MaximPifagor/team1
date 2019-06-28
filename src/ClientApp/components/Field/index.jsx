import React from 'react';
import styles from './styles.css'
import Cell from '../Cell'

export default class Field extends React.Component {
    render () {
        const map = this.props.map || [[]];
        const cells = [];
        map.forEach((types, yCoord) => {
            types.forEach((type, xCoord) => {
                cells.push(<Cell type={type} key={xCoord + ':' + yCoord} />);
            });
        });

        return (
            <div className={ styles.root }>
                {cells}
            </div>
        );
    }
}
