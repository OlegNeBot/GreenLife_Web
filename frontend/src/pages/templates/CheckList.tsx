import { observer } from 'mobx-react-lite';
import React, { useState } from 'react';
import { Card, Modal, Container, Button } from 'react-bootstrap';
import { CircularProgressbar, buildStyles } from 'react-circular-progressbar';
import { useNavigate } from 'react-router';

import { CheckListModel } from '../../models/CheckListModel';
import checklist from '../../store/CheckListStore';

const CheckList : React.FC<CheckListModel> = observer(props => {
  const navigate = useNavigate();

  const redirectToChecklist = (id: number) => {
    navigate(`/home/checklists/${id}`, {replace: true});
  }
  
  return (
    <>
      {props.ExecutionStatus === false
      ?  <Card style={{width: '18rem', cursor: 'pointer'}} onClick={() => { redirectToChecklist(props.Id); }} className={'border border-dark mt-3 px-0'} >
        <Card.Header as='h5' className='text-white' style={{backgroundColor: '#4d7d0f'}}>{props.CheckListName.Name}</Card.Header>
      </Card>

       : <Card bg='secondary' style={{width: '18rem', cursor: 'pointer'}} onClick={() => { redirectToChecklist(props.Id); }} className={'border border-light mt-3 px-0'}>
        <Card.Header as='h5' className='text-white'>{props.CheckListName.Name}</Card.Header>
      </Card>
      }
    </>
  );
})

export default CheckList;