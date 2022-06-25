import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { Card } from 'react-bootstrap';

import { ActionModel } from '../../models/ActionModel';

const Action : React.FC<ActionModel> = observer(props => {
  let text = '';

  if (props.Action.ActionType.TypeName === 'Достижение') {
    text = `${props.Account.Name} получил(а) достижение: ${props.Action.ActionName}!`;
  }
  else if (props.Action.ActionType.TypeName === 'Действие') {
    text = `${props.Account.Name} ${props.Action.ActionName}!`
  } 

  return(
    <>
      <Card>
        <Card.Body>
          <Card.Subtitle className='text-muted mb-2'>{props.ActionDate}</Card.Subtitle>
          <Card.Text>{text}</Card.Text>
        </Card.Body>
      </Card>
    </>
  );
});

export default Action;