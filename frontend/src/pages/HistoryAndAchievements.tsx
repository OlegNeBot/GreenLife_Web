import { observer } from 'mobx-react-lite';
import React, {useEffect} from 'react';
import { ListGroup } from 'react-bootstrap';
import { ActionModel } from '../models/ActionModel';

import action from '../store/ActionStore';
import Action from './templates/Action';

const HistoryAndAchievements : React.FC = () => {
  useEffect(() => {
    action.load();
  });

  return(
    <>
      <ListGroup variant="flush">
        {action.actions.map((a: ActionModel) => 
          <ListGroup.Item className='p-0' key={a.Id}>
            <Action Id={a.Id} ActionDate={a.ActionDate} Action={a.Action} Account={a.Account} key={a.Id}/>
          </ListGroup.Item>
        )}
      </ListGroup>
    </>
  );
}

export default HistoryAndAchievements;