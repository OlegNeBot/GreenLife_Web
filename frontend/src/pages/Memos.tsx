import { observer } from 'mobx-react-lite';
import React, {useEffect} from 'react';
import { Container, Row } from 'react-bootstrap';
import { MemoModel } from '../models/MemoModel';

import memo from '../store/MemoStore';
import SearchFilters from './templates/SearchFilters';
import Memo from './templates/Memo';

const Memos : React.FC = observer(() => {
  useEffect(() => {
    memo.load();
  }, [])
  
    return(
        <>
          <Container >
            {/* <SearchFilters type='memos' /> */}

            <Row className='px-0 justify-content-between'>
              {memo.memos.map((m: MemoModel) => 
                <Memo Id={m.Id} MemoName={m.MemoName} MemoRef={m.MemoRef} key={m.Id}/>
                )}
            </Row>
          </Container>
        </>
    );
})

export default Memos;