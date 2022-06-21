import React, {useState} from 'react';
import { Button, Card, Container, Modal, Image } from 'react-bootstrap';

import { buildStyles, CircularProgressbar } from 'react-circular-progressbar';
import 'react-circular-progressbar/dist/styles.css';
import { useNavigate } from 'react-router';

export interface ComponentProps {
  type: string,
  name: string,
  total?: number,
  num?: number,
  phrase?: string,
  picRef?: string,
  checkListId?: number,
  className?: string
};

const Template : React.FC<ComponentProps> = props => {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  if (props.type === 'memo') {
    return(
      <>
        <Card style={{width: '18rem', cursor: 'pointer'}} className={`border border-dark ${props.className}`} onClick={handleShow}>
          <Card.Img src='https://downloader.disk.yandex.ru/preview/7949e5c1141fc046fbe614572356e3133d68bf55761517377593b31163d6e724/62b1c0bd/Y4KHBo_zV6VoQXCr3W5eDW3VJUU8UulssUUyudGM8RX9qMuptS5K3ZSweMxSxhYXhyPdttybGYam7iZiXmvjzw%3D%3D?uid=0&filename=1.jpg&disposition=inline&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=0&tknv=v2&size=2048x2048' />
          <Card.Body>
            <Card.Title>{props.name}</Card.Title>
          </Card.Body>
        </Card>

        <Modal show={show} onHide={handleClose}>
          {/* //TODO: Add an image tag */}
          <Modal.Header closeButton>
            <Modal.Title>{props.name}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Image src='https://downloader.disk.yandex.ru/preview/7949e5c1141fc046fbe614572356e3133d68bf55761517377593b31163d6e724/62b1c0bd/Y4KHBo_zV6VoQXCr3W5eDW3VJUU8UulssUUyudGM8RX9qMuptS5K3ZSweMxSxhYXhyPdttybGYam7iZiXmvjzw%3D%3D?uid=0&filename=1.jpg&disposition=inline&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=0&tknv=v2&size=2048x2048'/>
          </Modal.Body>
        </Modal>
      </>
    ); }
  else if (props.type !== 'habit_done') {
    const [markShow, setMarkShow] = useState(false);
  
    const navigate = useNavigate();

    const handleMarkClose = () => { setMarkShow(false), setShow(true) }
    const handleMarkShow = () => { setMarkShow(true), setShow(false) }

    const percentage = Math.round(props.num! / props.total! * 100);

    const goToChecklist = () => {
      const id = props.checkListId;
      navigate(`/home/checklists:${id}`, {replace: true});
    }

    return(
      <>
        <Card style={{width: '18rem', cursor: 'pointer'}} onClick={handleShow} className={`border border-dark ${props.className}`} >
          <Card.Header as='h5' className='text-white' style={{backgroundColor: '#4d7d0f'}}>{props.name}</Card.Header>
          <Card.Body>
            <Card.Text className='text-center'>{`${props.num}/${props.total}`}</Card.Text>
          </Card.Body>
        </Card>

        <Modal show={show} onHide={handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>{props.name}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Container style={{ width: 300, height: 300 }}>
              <CircularProgressbar  value={percentage} text={`${percentage}%`} strokeWidth={5} styles={buildStyles({
                textSize: '14px',

                // textColor: '#4d7d0f',
                // pathColor: '#4d7d0f',
              })}/>
            </Container>
          </Modal.Body>
          <Modal.Footer>

            {/* <Button variant='primary' style={{backgroundColor: '#4d7d0f', borderColor: '#4d7d0f'}}>Отметить</Button> */}
            {props.type === 'habit'
            ? <Button className='w-full' variant='primary' onClick={handleMarkShow}>Отметить</Button> 
            : <Button className='w-full' variant='primary' onClick={goToChecklist}>Обновить</Button>
            }
          </Modal.Footer>
        </Modal>
        
        <Modal show={markShow} onHide={handleMarkClose}
          size="lg"
          aria-labelledby="contained-modal-title-vcenter"
          centered
        >
          <Modal.Header>
            <Modal.Title id='contained-modal-title-vcenter'>Привычка отмечена!</Modal.Title>
          </Modal.Header>
          <Modal.Body>{props.phrase}</Modal.Body>
          <Modal.Footer>
            <Button className='w-full' variant="primary" onClick={handleMarkClose}>
              OK
            </Button>
          </Modal.Footer>
        </Modal>
      </>
    );}
    else return(
        <Card style={{width: '18rem'}} className={`border border-dark ${props.className}`} bg='light'>
          <Card.Header as='h5' className='text-white' >{props.name}</Card.Header>
          <Card.Body>
            <Card.Text className='text-center'>{`${props.num}/${props.total}`}</Card.Text>
          </Card.Body>
        </Card>
      );
    
}

export default Template;