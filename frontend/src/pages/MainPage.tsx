import React, {useState} from 'react';
import { Alert, Card, Carousel, Container, Row } from 'react-bootstrap';
import CheckList from './templates/CheckList';
import CheckListProgress from './templates/CheckListProgress';
import Habit from './templates/Habit';
import HabitProgress from './templates/HabitProgress';

const MainPage : React.FC = () => {
    return(
        <Container fluid>
            <Row>
              <HabitProgress num={5} total={20}/>
            </Row>
            <Row>
              <CheckListProgress num={3} total={5}/>
            </Row>
            <Row>
              <Alert variant='success'>У Вас: 10 баллов</Alert>
            </Row>
        </Container>
    );
}

export default MainPage;