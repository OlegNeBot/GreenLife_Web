import React, {useEffect} from 'react';
import { Container, Row } from 'react-bootstrap';
import SearchFilters from './templates/SearchFilters';

import checklist from '../store/CheckListStore'
import { CheckListModel } from '../models/CheckListModel';
import CheckList from './templates/CheckList';
import { observer } from 'mobx-react-lite';


const CheckLists : React.FC = observer(() => {
  useEffect(() => { checklist.load()}, [])

    return(
        <>
          <Container >
            {/* <SearchFilters type='checklist' /> */}

            <Row className='px-0 justify-content-between'>
              {checklist.checklists.map((c: CheckListModel) => 
                <CheckList Id={c.Id} CheckListName={c.CheckListName} ExecutionStatus={c.ExecutionStatus} key={c.Id} />
                )}
            </Row>
          </Container>
        </>
    );
});

export default CheckLists;