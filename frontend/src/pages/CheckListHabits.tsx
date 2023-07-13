import React, {useEffect, useState} from 'react';
import { Container, Row } from 'react-bootstrap';
import { useParams } from 'react-router-dom';

import { HabitModel } from '../models/HabitModel';
import Habit from './templates/Habit';
import habit from '../store/HabitStore'

import SearchFilters from './templates/SearchFilters';
import { observer } from 'mobx-react-lite';

const CheckListHabits : React.FC = observer(() => {
  const params = useParams();
  const id = Number(params.id);

  useEffect(() => {
    habit.loadByCheckList(id);
  }, [habit, habit.loadByCheckList])

    return(
        <>
          <Container >
            <SearchFilters type='habit' />

            <Row className='px-0 justify-content-between'>
              {habit.chLHabits.map((h: HabitModel) => 
                <Habit Id={h.Id} HabitName={h.HabitName} Total={h.Total} HabitPerformance={h.HabitPerformance} key={h.Id}/>
                )}
            </Row>
          </Container>
        </>
    );
});

export default CheckListHabits;