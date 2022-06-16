import React, {useState} from 'react';
import { Col, Form, InputGroup, Row } from 'react-bootstrap';

import {HiOutlineSearch} from 'react-icons/hi'

export interface SearchProps {
  type: string,
};

const SearchFilters : React.FC<SearchProps> = props => {

  const [sort, setSort] = useState<string>('0');

  const sortHandler  = (e: React.ChangeEvent<HTMLSelectElement> ) => {
    setSort(e.target.value);
  }
  
  return(
    <>
      <Row className='justify-content-between align-items-center mt-3'>
        <Col className='ps-0'>
          <Form >
            <Form.Group>
              <InputGroup>
                <InputGroup.Text><HiOutlineSearch/></InputGroup.Text>
                <Form.Control type='text' placeholder='Поиск' />
              </InputGroup>
            </Form.Group>
          </Form>
        </Col>

        <Col className='pe-0'>
          <Form.Group controlId='count' >
            <Form.Select value={sort} onChange={sortHandler}>
              <option value='0' disabled>Сортировка:</option>
              {props.type !== 'memo' 
              ? <>
                  <option value='1'>По наибольшему кол-ву отметок</option>
                  <option value='2'>По наименьшему кол-ву отметок</option>
                </>
              : <></>
              }
              <option value='3'>По алфавиту: от А до Я</option>
              <option value='4'>По алфавиту: от Я до А</option>   
              
            </Form.Select>
          </Form.Group>
        </Col>
      </Row>
    </>
  );
}

export default SearchFilters;