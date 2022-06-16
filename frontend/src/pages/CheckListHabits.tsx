import React, {useState} from 'react';
import { useParams } from 'react-router-dom';
import ListTemplate from './listsTemplates/ListTemplate';

const CheckListHabits : React.FC = () => {
  // При рендеринге компонента отправляется запрос на сервер
  // Возвращаются привычки чек-листа с checkListId === props.checkListId
  const params = useParams();
  const id = params.id;
    return(
        <>
          <ListTemplate type='habit' />
        </>
    );
}

export default CheckListHabits;