import React, { useEffect } from 'react';
import { Button, Card } from 'react-bootstrap';

import habit from '../store/HabitStore';

export interface Certificate {
  Name?: string,
  hName?: string
}

const Certificate : React.FC<Certificate> = () => {
  useEffect(() => {
  }, [])
  return(
    <>
    <Card id='certificate'>
      <Card.Header className='text-white' style={{backgroundColor: '#4d7d0f'}}>Сертификат</Card.Header>
      <Card.Body>
        <Card.Text>
          {'Поздравляем! Ты сформировал(а) эту привычку! Продолжай в том же духе!'}
        </Card.Text>
      </Card.Body>
    </Card>
    <Button variant='primary w-full' onClick={() => {habit.createReport()}}>Скачать сертификат</Button>
    </>
  );
}

export default Certificate;