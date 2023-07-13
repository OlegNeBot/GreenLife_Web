import React, { useEffect } from 'react';
import { Alert, Container, Row } from 'react-bootstrap';
import { observer } from 'mobx-react-lite';
import account from '../store/AccountStore';

const MainPage : React.FC = observer(() => {
  useEffect(() => {
    account.load();
  }, []);
  
    return(
      <Container fluid>
        <Alert variant='success'>Ваши баллы: {account.Account.ScoreSum}</Alert>
        <Row className='px-0 justify-content-between'>
          {/* {habit.chLHabits.map((h: HabitModel) => 
            <Habit Id={h.Id} HabitName={h.HabitName} Total={h.Total} HabitPerformance={h.HabitPerformance} key={h.Id}/>
         )} */}
        </Row>
        <Row className='px-0 justify-content-between'>
          {/* {checklist.checklists.map((c: CheckListModel) => 
            <CheckList Id={c.Id} CheckListName={c.CheckListName} ExecutionStatus={c.ExecutionStatus} key={c.Id} />
          )} */}
        </Row>
      </Container>
    );
});

export default MainPage;