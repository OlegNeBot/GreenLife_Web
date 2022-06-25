import { observer } from 'mobx-react-lite';
import React, { useState } from 'react';
import { Card, Modal, Image } from 'react-bootstrap';
import { MemoModel } from '../../models/MemoModel';

const Memo : React.FC<MemoModel> = observer(props => {
  const [show, setShow] = useState(false);

  const toggleShow = () => {
    setShow(!show);
  }

  return (
    <>
        <Card style={{width: '18rem', cursor: 'pointer'}} className={'border border-dark mt-3 px-0'} onClick={toggleShow}>
          <Card.Img src={props.MemoRef} />
          <Card.Body>
            <Card.Title>{props.MemoName}</Card.Title>
          </Card.Body>
        </Card>

        <Modal show={show} onHide={toggleShow}>
          <Modal.Header closeButton>
            <Modal.Title>{props.MemoName}</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Image src={props.MemoRef} />
          </Modal.Body>
        </Modal>
      </>
  );
})

export default Memo;