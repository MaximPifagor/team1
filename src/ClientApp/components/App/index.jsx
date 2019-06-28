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
        fetch('/api/game/').then(response => {
            if (response.ok) {
                /*const PLAYER_CELL_TYPE = 4;*/
                const data = response.json();
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
            }
            console.error('GET /get request failed');
        });

        document.addEventListener('keyup', this.keyUpEventHandler);

        /*this.setState({
            map: [
                [0, 0, 1, 1, 1, 1, 1, 0],
                [1, 1, 1, 0, 0, 0, 1, 0],
                [1, 4, 0, 3, 0, 0, 1, 0],
                [1, 1, 1, 0, 3, 2, 1, 0],
                [1, 2, 1, 1, 3, 0, 1, 0],
                [1, 0, 1, 0, 2, 0, 1, 1],
                [1, 3, 0, 3, 3, 3, 2, 1],
                [1, 1, 1, 1, 1, 1, 1, 1],
            ],
            playerCoords: {x: 1, y: 2}
        });*/
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
        fetch('/api/game/' + this.state.id + '?movement=' + direction).then(response => {
            if (response.ok) {
                const data = response.json();
                let playerCoords;
                const mapArr = data.map.split(' ').map((str, index) => str.split(','));
                this.setState({
                    map: mapArr,
                    playerCoords
                });
                return;
            }
            console.error('GET /api/game/id?movement='+direction+' request failed');
        });
    }

    keyUpEventHandler = (event) => {
        const DIRECTIONS = {
            38: 0,
            39: 3,
            40: 1,
            37: 2
        };
        const CODE = event.keyCode;
        if (!DIRECTIONS.hasOwnProperty(CODE)) {
            return;
        }

        this.notifyMovement(DIRECTIONS[CODE]);
    };
}
