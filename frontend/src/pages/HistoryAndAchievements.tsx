import React, {useState} from 'react';
import { Alert, Card, ListGroup } from 'react-bootstrap';
import AchievementItem from './templates/AchievementItem';
import Habit from './templates/Habit';
import HistoryItem from './templates/HistoryItem';

const HistoryAndAchievements : React.FC = () => {
  return(
    <>
      <ListGroup variant="flush">
        <ListGroup.Item className='p-0'>
          <AchievementItem username='Олег' date={new Date().toDateString()} achievement='Сделать "Историю и достижения"'/>
        </ListGroup.Item>
        <ListGroup.Item className='p-0'>
        <AchievementItem username='Снова Олег' date={new Date().toDateString()} achievement='Сделать template "Достижение"'/>
        </ListGroup.Item>
        <ListGroup.Item className='p-0'>
          <HistoryItem username='Олег' date={new Date('12.06.2022').toString()} action='сделал(а) "Историю и достижения"'/>
        </ListGroup.Item>
        <ListGroup.Item className='p-0'>
        <HistoryItem username='Снова Олег' date={new Date('12.06.2022').toDateString()} action='сделал(а) template "История"'/>
        </ListGroup.Item>
      </ListGroup>
    </>
  );
}

export default HistoryAndAchievements;