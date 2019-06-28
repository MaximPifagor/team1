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
            level: 0,
            levelId: 0,
            modalVisible: false
        };
        this.keyListener = null;
        this.movementDirections = {
            37: 1,
            38: 2,
            39: 0,
            40: 3
        };
        this.wonMessage = {
            title: 'Поздравляем!',
            body: 'Вы успешно прошли уровень.'
        }
    }

    componentDidMount() {
        this.startLevel();
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

    /**
     * Выполняет запрос на сервер
     * @param {String} url - адрес запроса
     * @param {Function|undefined} callback - коллбэк
     */
    sendRequest = (url, callback) => {
        fetch(url)
            .then(response => response.ok ? response.json() : Promise.reject())
            .then(data => { typeof callback === 'function' && callback(data) });
    }

    /**
     * Начинает игру
     * @returns {undefined}
     */
    startLevel = () => {
        const URL = '/api/game' + (this.state.level === 0 ? '' : ('?level=' + this.state.level));
        this.sendRequest(URL, (data) => {
            const mapArr = data.map.split(' ').map((str, index) => str.split(','));
            this.setState({
                map: mapArr,
                levelId: data.id,
                modalVisible: data.isFinished,
                level: this.state.level + 1
            });
            this.keyListener = document.addEventListener('keyup', this.keyboardListener, false);
        });
    }

    /**
     * Уведомляет сервер о переходе в указанном направлении
     * @param {Number} direction - код направления из this.movementDirections
     * @returns {undefined}
     */
    notifyMovement(direction) {
        const URL = '/api/game/' + this.state.levelId + '?movement=' + direction;
        this.sendRequest(URL, (data) => {
            const mapArr = data.map.split(' ').map((str, index) => str.split(','));
            data.inFinished && document.removeEventListener(this.keyListener);
            this.setState({
                map: mapArr,
                modalVisible: data.isFinished
            });
        });
    }

    /**
     * Выводит модальное окно с сообщением
     * @param {string} title - текст заголовка
     * @param {string|*} body - сообщение
     * @returns {*}
     */
    renderModal = ({title, body}) => {
        const onClose = () => {
            this.switchModalVisiblility();
            this.startLevel();
        };
        return (
            <Modal onClose={onClose}>
                <Modal.Header>{title}</Modal.Header>
                <Modal.Body>{body}</Modal.Body>
                <Modal.Footer>
                    <Button onClick={onClose} use="primary">Следующий уровень</Button>
                </Modal.Footer>
            </Modal>
        );
    }

    /**
     * Переключает отображение модального окна
     * @returns {undefined}
     */
    switchModalVisiblility = () => {
        this.setState({
            modalVisible: !this.state.modalVisible
        });
    }

    /**
     * Прослушивает нажатие на клавиатуру
     * @param {Object} event - событие
     * @returns {undefined}
     */
    keyboardListener = (event) => {
        if (this.state.modalVisible) {
            return;
        }
        const CODE = event.keyCode;
        if (! this.movementDirections.hasOwnProperty(CODE)) {
            return;
        }
        this.notifyMovement(this.movementDirections[CODE]);
    }

}