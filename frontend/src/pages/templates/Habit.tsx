import habit from '../../store/HabitStore';

import { HabitModel } from '../../models/HabitModel';

import React, { useState } from 'react';

import { Card, Modal, Container, Button } from 'react-bootstrap';
import { CircularProgressbar, buildStyles } from 'react-circular-progressbar';

import { observer } from 'mobx-react-lite';

import { BsFillCheckCircleFill } from 'react-icons/bs';
import { useNavigate } from 'react-router';
import { Paths } from '../../routes';

const Habit : React.FC<HabitModel> = observer(props => {
  const [show, setShow] = useState(false);
  const [markShow, setMarkShow] = useState(false);

  const toggleShow = () => {
    setShow(!show);
  }

  const toggleMarkShow = () => {
    setMarkShow(!markShow);
    setShow(!show);
  }

  const navigate = useNavigate();

  const percentage = Math.round(props.HabitPerformance[0].NumOfExecs / props.Total * 100);

  return (
    <>
      {props.HabitPerformance[0].Executed === false
    ? <>
        <Card style={{width: '18rem', cursor: 'pointer'}} onClick={toggleShow} className={'border border-dark mt-3 px-0'} >
          <Card.Header as='h5' className='text-white' style={{backgroundColor: '#4d7d0f'}}>{props.HabitName}</Card.Header>
          <Card.Body>
            <Card.Text className='text-center fw-bold'>{`${props.HabitPerformance[0].NumOfExecs}/${props.Total}`}</Card.Text>
          </Card.Body>
        </Card>

        <Modal show={show} onHide={toggleShow}>
          <Modal.Header closeButton>
            <Modal.Title>{props.HabitName}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Container style={{ width: 300, height: 300 }}>
              <CircularProgressbar  value={percentage} text={`${percentage}%`} strokeWidth={5} styles={buildStyles({
                textColor: '#4d7d0f',
                pathColor: '#4d7d0f',
                trailColor: '#d6d6d6',
                textSize: '16px',
              })}/>
            </Container>
          </Modal.Body>
          <Modal.Footer>
            <Button className='w-full' variant='primary' onClick={() => {
              habit.mark(props.Id, habit.habits);
              toggleMarkShow();
            }}
            >
              Отметить
            </Button> 
          </Modal.Footer>
        </Modal>
        
        <Modal show={markShow} onHide={toggleMarkShow}
          size="lg"
          aria-labelledby="contained-modal-title-vcenter"
          centered
        >
          <Modal.Header>
            <Modal.Title id='contained-modal-title-vcenter'>Привычка отмечена!</Modal.Title>
          </Modal.Header>
          <Modal.Body>{habit.HabitPhrase.PhraseText}</Modal.Body>
          <Modal.Footer>
            <Button className='w-full' variant="primary" onClick={ () => {
              toggleMarkShow();
              habit.checkExecuted(props.Id);
              }}>
              OK
            </Button>
          </Modal.Footer>
        </Modal>
      </>
    : <>
        <Card bg='secondary' style={{width: '18rem', cursor: 'pointer'}} onClick={toggleShow} className={'border border-light mt-3 px-0'} >
          <Card.Header as='h5' className='text-white'>{props.HabitName}</Card.Header>
          <Card.Body>
            <Card.Text className='text-center fw-bold text-white'>Сформирована</Card.Text>
          </Card.Body>
        </Card>

        <Modal show={show} onHide={toggleShow}>
          <Modal.Header closeButton>
            <Modal.Title>{props.HabitName}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <span className='text-center'>
              <BsFillCheckCircleFill className='text-success'/>
              Привычка сформирована
            </span>
            <Button className='w-full' variant='primary' onClick={() => {
              // habit.loadCertificate(props.Id);
              navigate(Paths.Certificate.path);
            }}
            >
              Получить сертификат
            </Button> 
          </Modal.Body>
        </Modal>
      </>
      }
    </>
  );
}
);

export default Habit;