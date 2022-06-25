import { observer } from 'mobx-react-lite';
import React from 'react';
import { Card, ProgressBar } from 'react-bootstrap';

interface IProgressProps {
  type: string,
  num: number,
  total: number
}

const ProgressTemplate : React.FC<IProgressProps> = observer(props => {
  const now = Math.round(props.num / props.total * 100);
  let text = '';
  if (now >= 0 && now < 25){
    text = 'Хорошее начало! Продолжай в том же духе!';
  }
  else if (now >= 25 && now < 50) {
    text='Ты молодец! Не останавливайся, ты на правильном пути!';
  }
  else if (now >= 50 && now < 75) {
    text='Полпути уже пройдено! Продолжай осваивать новые привычки!';
  }
  else if (now >= 75 && now < 100) {
    text='Ты практически у цели! Ты справишься!';
  }
  else {
    text='Поздравляем! Все привычки освоены!';
  }

  return (
    <Card className='px-0'>
      {props.type === 'habit'
        ? <Card.Header className='fw-bold'>Прогресс по привычкам</Card.Header>
        : <Card.Header className='fw-bold'>Заполненность чек-листов</Card.Header>
      }
      <Card.Body>
        <ProgressBar animated now={now} label={`${props.num}/${props.total}`}/>
        <Card.Text>{text}</Card.Text>
      </Card.Body>
    </Card>
  );
});

export default ProgressTemplate;
