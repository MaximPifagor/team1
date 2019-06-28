import React from 'react';
import styles from './styles.css';
import Field from '../Field';

export default class App extends React.Component {
    constructor() {
        super();
        this.state = {
            score: 50,
            map: null,
            playerCoords: null,
            id: 0
        };
    }

    componentDidMount() {
        fetch('/api/game')
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                return Promise.reject();
            })
            .then(data => {
                /*const PLAYER_CELL_TYPE = 4;*/
                let playerCoords;
                const mapArr = data.map.split(' ').map((str, index) => {
                    const row = str.split(',');
                    /*if (row.indexOf(PLAYER_CELL_TYPE)) {
                        playerCoords = {y: index, x: row.indexOf(PLAYER_CELL_TYPE)};
                    }*/
                    return row;
                });
                this.setState({
                    map: mapArr,
                    playerCoords,
                    id: data.id
                });
                return;
            });

        document.addEventListener('keyup', this.keyUpEventHandler);
    }

    render() {
        return (
            <div className={styles.root}>
                <div className={styles.score}>
                    Ваш счет: {this.state.score}
                </div>
                <Field map={this.state.map}/>
            </div>
        );
    }

    notifyMovement(direction) {
        fetch('/api/game/' + this.state.id + '?movement=' + direction)
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                return Promise.reject();
            })
            .then(data => {
                let playerCoords;
                const mapArr = data.map.split(' ').map((str, index) => str.split(','));
                this.setState({
                    map: mapArr,
                    playerCoords
                });
            });
    }

    keyUpEventHandler = (event) => {
        console.dir(event);
        console.dir(event.keyCode);
        const DIRECTIONS = {
            37: 1,
            38: 2,
            39: 0,
            40: 3
        };
        const CODE = event.keyCode;
        console.dir(CODE);
        console.dir(DIRECTIONS.hasOwnProperty(CODE));
        if (!DIRECTIONS.hasOwnProperty(CODE)) {
            return;
        }

        this.notifyMovement(DIRECTIONS[CODE]);
    };
}
