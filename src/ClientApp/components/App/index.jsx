import React from 'react';
import styles from './styles.css';
import Field from '../Field';

export default class App extends React.Component {
    constructor () {
        super();
        this.state = {
            score: 50,
            map: null
        };
    }

    componentDidMount() {
        /*fetch('/get').then(response => {
            if (response.ok) {
                this.setState({
                    map:response.json()
                });
            }
            console.error('GET /get request failed');
        });*/
        this.setState({
            map: [
                [0, 0, 1, 1, 1, 1, 1, 0],
                [1, 1, 1, 0, 0, 0, 1, 0],
                [1, 4, 0, 3, 0, 0, 1, 0],
                [1, 1, 1, 0, 3, 2, 1, 0],
                [1, 2, 1, 1, 3, 0, 1, 0],
                [1, 0, 1, 0, 2, 0, 1, 1],
                [1, 3, 0, 3, 3, 3, 2, 1],
                [1, 1, 1, 1, 1, 1, 1, 1],
            ]
        });
    }

    render () {
        return (
            <div className={ styles.root }>
                <div className={ styles.score }>
                    Ваш счет: { this.state.score }
                </div>
                <Field map={this.state.map} />
            </div>
        );
    }
}
