import React, {useState} from 'react';
import { Container, Row } from 'react-bootstrap';
import CheckList from '../templates/CheckList';
import Habit from '../templates/Habit';
import Memo from '../templates/Memo';
import SearchFilters from './SearchFilters';

interface ListProps {
  type: string
}

const ItemsList : React.FC<ListProps> = props => {
  if(props.type === 'habit') {
    return(
      <>
        <Habit className='mt-3 px-0' name='Имя 1' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 2' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 3' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 4' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 5' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 6' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 7' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 8' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 9' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 10' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 11' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 12' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 13' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 14' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 15' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 16' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 17' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 18' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 19' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 20' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 21' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 22' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 23' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 24' num={3} total={5} phrase='Фраза 1'/>
        <Habit className='mt-3 px-0' name='Имя 25' num={3} total={5} phrase='Фраза 1'/>
      </>
    ); }
  else if(props.type === 'checklist') {
    return (
    <>
      <CheckList className='mt-3 px-0' name='Имя 1' num={3} total={5} checkListId={1}/>
      <CheckList className='mt-3 px-0' name='Имя 2' num={3} total={5} checkListId={2}/>
      <CheckList className='mt-3 px-0' name='Имя 3' num={3} total={5} checkListId={3}/>
      <CheckList className='mt-3 px-0' name='Имя 4' num={3} total={5} checkListId={4}/>
      <CheckList className='mt-3 px-0' name='Имя 5' num={3} total={5} checkListId={5}/>
    </>
    ); }
  else if(props.type === 'memo') {
    return(
    <>
      <Memo className='mt-3 px-0' name='Эко-правонарушения' picRef='https://disk.yandex.ru/i/bFwhoWW5L0fdjw'/>
    </>
    );} 
  return <></>
}

const ListTemplate : React.FC<ListProps> = props => {
  return(
    <Container >
      <SearchFilters type={props.type} />

      <Row className='px-0 justify-content-between'>
        <ItemsList type={props.type}/>
      </Row>
    </Container>
  );
}

export default ListTemplate;