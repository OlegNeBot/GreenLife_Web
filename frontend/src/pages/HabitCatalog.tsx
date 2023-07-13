import React, {useEffect, useState} from 'react';
import { Container, Row } from 'react-bootstrap';

import SearchFilters from './templates/SearchFilters';
import Habit from './templates/Habit';

import habit from '../store/HabitStore';
import { HabitModel } from '../models/HabitModel';
import { observer } from 'mobx-react-lite';

const HabitCatalog : React.FC = observer(() => {
  useEffect(() => {
    habit.load();
  }, [habit, habit.load])
  
    return(
        <>
          <Container >
            <SearchFilters type='habit' />

            <Row className='px-0 justify-content-between'>
              {habit.sortedHabits.map((h: HabitModel) => 
                <Habit Id={h.Id} HabitName={h.HabitName} Total={h.Total} HabitPerformance={h.HabitPerformance} key={h.Id}/>
                )}
            </Row>
          </Container>
        </>
    );
});

export default HabitCatalog;