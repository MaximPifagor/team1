import React from 'react';
import styles from './styles.css';
import Field from '../Field';
import Modal from '@skbkontur/react-ui/Modal';
import Button from '@skbkontur/react-ui/Button';

export default class App extends React.Component {
    constructor() {
        super();
        this.state = {
            score: 50,
            map: null,
            playerCoords: null,
            id: 0,
            modalVisible: false,
            isFinished: false,
            level: 0
        };
        this.keyListener = null;
        this.wonMessage = {
            title: 'Поздравляем!',
            body: 'Вы успешно прошли уровень.'
        }
    }

    componentDidMount() {
        this.startLevel();
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
                const mapArr = data.map.split(' ').map((str, index) => str.split(','));
                this.setState({
                    map: mapArr,
                    modalVisible: data.isFinished
                });
                data.inFinished && this.endLevel();
            });
    }

    startLevel = () => {
        console.dir(this.state.level > 0);
        const URL = '/api/game' + (this.state.level === 0 ? '' : ('?level=' + this.state.level));
        fetch(URL)
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                return Promise.reject();
            })
            .then(data => {
                const mapArr = data.map.split(' ').map((str) => str.split(','));
                this.setState({
                    map: mapArr,
                    id: data.id,
                    isFinished: data.isFinished,
                    level: this.state.level + 1
                });
                return;
            });
        this.keyListener = document.addEventListener('keyup', this.keyUpEventHandler, false);
    }

    endLevel = () => {
        document.removeEventListener(this.keyListener);
    }

    render() {
        return (
            <div className={styles.root}>
                <div className={styles.score}>
                    Ваш счет: {this.state.score}
                </div>
                <Field map={this.state.map}/>
                {this.state.modalVisible && this.renderModal(this.wonMessage)}
            </div>
        );
    }

    renderModal = ({title, body}) => {
        const onClose = () => { this.switchModalVisiblility(); this.startLevel(); };
        return (
            <Modal onClose={onClose}>
                <Modal.Header>{title}</Modal.Header>
                <Modal.Body>
                    {body}
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={onClose}>Продолжить</Button>
                </Modal.Footer>
            </Modal>
        );
    };

    switchModalVisiblility = () => {
        this.setState({
            modalVisible: !this.state.modalVisible
        });
    };

    keyUpEventHandler = (event) => {
        if (this.state.modalVisible) {
            return;
        }
        const DIRECTIONS = {
            37: 1,
            38: 2,
            39: 0,
            40: 3
        };
        const CODE = event.keyCode;
        if (!DIRECTIONS.hasOwnProperty(CODE)) {
            return;
        }

        this.notifyMovement(DIRECTIONS[CODE]);
    };
}
